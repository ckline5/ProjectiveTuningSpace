using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MOS : MonoBehaviour
{
    LineRenderer line;
    public int segments = 64; //circle length in straight lines
    public int iterations = 8; //how many MOS circles
    public float scaling = 100f;
    public decimal period = 1200M;
    public decimal generator = 833M;
    public decimal equivalenceInterval = 1200M;
    Dictionary<int, List<decimal>> pitches;
    List<MOSLine> lines;
    List<MOSLine> circleLines;

    public float periodAsFloat = 1200f;
    public float generatorAsFloat = 833f;
    public float EquivalenceIntervalAsFloat = 1200f;

    public MOSLine MOSLine_prefab;
    private Transform MOSText;
    private TextMesh MOSTextMesh;

    public float initialLineWidth = .005f;

    public void Awake()
    {
        pitches = new Dictionary<int, List<decimal>>();
        lines = new List<MOSLine>();
        circleLines = new List<MOSLine>();
        MOSText = transform.GetChild(0);
        MOSTextMesh = MOSText.GetComponent<TextMesh>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize(transform.position, 833M);
        //StartCoroutine(ProceduralMOS());
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {
        Initialize(transform.position, generator, period, equivalenceInterval);
    }

    public void Initialize(Vector3 pos, float g = 0f, float p = 1200f, float e = 1200f)
    {
        Initialize(pos, (decimal)g, (decimal)p, (decimal)e);
    }

    /// <summary>
    /// Initialize a temperament MOS diagram with a given period and generator (in cents)
    /// </summary>
    /// <param name="p">Period (in cents)</param>
    /// <param name="g">Generator (in cents)</param>
    /// <param name="pos">Position of center</param>
    public void Initialize(Vector3 pos, decimal g = 0M, decimal p = 1200M, decimal e = 1200M)
    {
        period = p;
        generator = g;
        equivalenceInterval = e;
        transform.position = pos;
        DrawCircles();
        DrawMOS();
        //DrawPitchCounts();
    }

    public void DeleteAll()
    {
        DeleteLines();
        DeleteCircles();
    }

    public void DeleteCircles()
    {
        foreach (MOSLine c in circleLines)
        {
            Destroy(c.gameObject);
        }
        circleLines.RemoveAll(c => c);
    }

    public void DeleteLines()
    {
        foreach(MOSLine l in lines)
        {
            Destroy(l.gameObject);
        }
        lines.RemoveAll(l => l);
        pitches.Clear();
        MOSTextMesh.text = "";
    }

    IEnumerator ProceduralMOS()
    {
        decimal generator = 0.0M;
        Vector3 pos = new Vector3(117.1573f, 100, 0);
        while (generator <= 1200M)
        {
            //Destroy(mos.gameObject);
            Initialize(pos, generator, 1200, 1200);
            ScreenCapture.CaptureScreenshot(@"C:\MOS\" + (int)(generator * 10) + ".png");
            //Debug.Log("Captured screenshot " + (generator * 10));
            generator += .1M;
            yield return new WaitForSeconds(3f);
        }
    }

    public void DrawCircles()
    {
        DeleteCircles();
        for (int c = 0; c <= iterations; c++)
        {
            MOSLine circle = Instantiate(MOSLine_prefab);
            circle.lineType = MOSLine.LineType.CIRCLE;
            circle.SetWidth(initialLineWidth);
            circle.transform.position = transform.position;
            circle.transform.rotation = transform.rotation;
            circle.transform.parent = transform;
            circle.name = "Circle " + c;
            circle.line.positionCount = segments + 1;
            circle.RemoveCollider(); 
            DrawCircle(circle, transform.position, c / Mathf.Sqrt(scaling) );
        }
    }

    private void DrawMOS()
    {
        DeleteLines();
        //place text in lower right
        MOSText.position = transform.position + new Vector3(iterations, -iterations, 0) / Mathf.Sqrt(scaling);
        MOSTextMesh.text = "g = " + new string(' ', 7 - generator.ToString("0.00").Length) + generator.ToString("0.00") + "¢";
        
        //next draw the lines. they will start at 0 and extend out to iterations / Mathf.Sqrt(scaling)
        //once MOS has been achieved (no more than 2 distinct step sizes + distributionally even, start at next circle out: n+1 / Mathf.Sqrt(scaling)
        decimal currentStep = 0M;
        int currentIteration = 0;
        decimal large = period;
        decimal small = 0;
        while (currentIteration <= iterations)
        {
            //draw line
            //Debug.Log("NEW MOS LINE");
            for (int i = 0; i < equivalenceInterval / period; i++)
            {
                MOSLine line = Instantiate(MOSLine_prefab);
                line.lineType = MOSLine.LineType.LINE;
                line.SetWidth(initialLineWidth);
                line.transform.position = transform.position;
                line.transform.rotation = transform.rotation;
                line.transform.parent = transform;
                line.name = "Line " + currentIteration + "-" + currentStep;

                //calculate angle of new line
                float angle = (((float)currentStep + i * (float)period) / (float)equivalenceInterval * 360) % 360 * Mathf.Deg2Rad;
                line.cents = ((float)currentStep + i * (float)period);

                //calculate points of new line and place line
                float x0 = Mathf.Sin(angle) * currentIteration / Mathf.Sqrt(scaling);
                float y0 = Mathf.Cos(angle) * currentIteration / Mathf.Sqrt(scaling);
                float x1 = Mathf.Sin(angle) * iterations / Mathf.Sqrt(scaling);
                float y1 = Mathf.Cos(angle) * iterations / Mathf.Sqrt(scaling);
                line.line.SetPosition(0, new Vector3(x0, 0, y0));
                line.line.SetPosition(1, new Vector3(x1, 0, y1));

                if (!lines.Exists(x => x.cents == line.cents))
                {
                    //only add a collider if it's a new cents value
                    line.AddCollider();
                }
                else
                {
                    line.RemoveCollider();
                }

                //line.SetText(line.cents.ToString());
                //line.SetTextPosition();

                lines.Add(line);
            }

            //add line to memory
            if (!pitches.ContainsKey(currentIteration))
            {
                if (currentIteration == 0)
                    pitches[currentIteration] = new List<decimal>();
                else
                    pitches[currentIteration] = pitches[currentIteration - 1];
            }
            pitches[currentIteration].Add(currentStep);
            pitches[currentIteration].Sort((x,y) => x.CompareTo(y));

            //see how many step sizes we have
            decimal prevPitch = pitches[currentIteration][0];
            List<decimal> stepSizes = new List<decimal>();
            for (int i = 1; i < pitches[currentIteration].Count; i++) 
            {
                decimal pitch = pitches[currentIteration][i];
                stepSizes.Add(pitch - prevPitch);
                prevPitch = pitch;
            }
            stepSizes.Add(period - prevPitch);
            int distinctSteps = (stepSizes.Select(p => p)).Distinct().Count();
            //Debug.Log("Distinct steps: " + distinctSteps);
            //foreach (float size in stepSizes.Select(p => p).Distinct()) {
                //Debug.Log(size);
            //}
            if (distinctSteps == 2 && large != small)
            {
                //2 step sizes: MOS!
                //set new large and small
                List<decimal> orderedSteps = (stepSizes.Select(p => p)).Distinct().OrderBy(p => p).ToList();
                large = orderedSteps.Last();
                small = orderedSteps.First();
                ////Debug.Log("Large: " + large);
                ////Debug.Log("Small: " + small);
                //increase current Iteration
                //but only if Myhill's property is met. 
                //Only if there are no more than 2 distinct interval sizes within the scale
                if (Myhill(pitches[currentIteration]))
                {
                    currentIteration++;
                    //Debug.Log("MOS RING COMPLETE");
                }
            }
            else if (distinctSteps == 1 && pitches[currentIteration].Count > 1 && large == small)
            {
                //fully-saturated ET
                break;
            }
            else if (currentIteration == 0 && pitches[currentIteration].Count == 1)
            {
                currentIteration++;
                //Debug.Log("MOS RING COMPLETE");
            }

            //Debug.Log(currentIteration + " - " + currentStep + ": " + angle * Mathf.Rad2Deg);
            currentStep += generator;
            currentStep %= period;
        }

    }

    /// <summary>
    /// Does this scale meet Myhill's property? 
    /// Are there no more than 2 distinct interval sizes for each interval class?
    /// </summary>
    /// <param name="steps">a scale, with absolute cents of each step</param>
    /// <returns>False if more than 2 distinct interval sizes are found within the scale</returns>
    private bool Myhill(List<decimal> steps)
    {
        int num = steps.Count;
        steps.Sort((x,y) => x.CompareTo(y));
        //foreach (float step in steps) { //Debug.Log(step); }
        bool myhill = true;
        //starting at each position of the scale, find interval sizes for each number
        Dictionary<int, List<decimal>> sizeCatalog = new Dictionary<int, List<decimal>>();
        for (int i = 0; i < num; i++)
        {
            for (int j = i + 1; j < num + i; j++)
            {
                int truj = (j >= num ? j % num : j);
                decimal interval = steps[truj] + (truj < j ? period : 0) - steps[i];
                //Debug.Log("truj: " + truj + ", i: " + i);
                //Debug.Log(steps[truj] + (truj < j ? period : 0) + " - " + steps[i] + " = " + interval);
                if (!sizeCatalog.ContainsKey(j - i))
                {
                    sizeCatalog[j - i] = new List<decimal>();
                }
                if (!sizeCatalog[j-i].Contains(interval))
                {
                    if (interval != 0)
                    {
                        sizeCatalog[j - i].Add(interval);
                    }
                    if (sizeCatalog[j-i].Count > 2)
                    {
                        myhill = false;
                        break;
                    }
                }
            }
        }
        for (int i = 1; i < num; i++)
        {
            //Debug.Log(" --- Interval class " + i + " ---   --[ " + sizeCatalog[i].Count + " interval sizes found ]--");
            foreach (float size in sizeCatalog[i])
            {
                //Debug.Log(size);
            }
            //Debug.Log("===================");
        }
        return myhill;
    }

    private void DrawCircle(MOSLine line, Vector3 center, float radius)
    {
        float x, y;
        float change = 2 * (float)Mathf.PI / segments;
        float angle = change;
        LineRenderer l = line.GetComponent<LineRenderer>();
        l.useWorldSpace = false;
        l.startWidth = initialLineWidth;
        l.endWidth = l.startWidth;

        //x = Mathf.Sin(angle) * radius;
        //line.SetPosition(0, center + new Vector3(x,0, 0));

        for (int i = 0; i <= segments; i++)
        {
            x = Mathf.Sin(angle) * radius;
            y = Mathf.Cos(angle) * radius;
            l.SetPosition(i, new Vector3(x, 0, y));

            angle += change;
        }
        circleLines.Add(line);
    }

    private void DrawPitchCounts()
    {
        foreach (KeyValuePair<int, List<decimal>> kvp in pitches)
        {
            int ringNo = kvp.Key;
            int pitchCount = kvp.Value.Count;
            MOSLine circle = circleLines[ringNo];
            //circle.SetText(pitchCount.ToString());
            //circle.SetTextPosition();
        }
    }
}
