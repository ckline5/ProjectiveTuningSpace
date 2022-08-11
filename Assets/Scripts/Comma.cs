using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static XenObjects;
using static XenMath;

public class Comma : MonoBehaviour, PTSObject
{
    public Interval TemperedInterval { get; set; }
    public Tuple<float,float,float> wedgie;

    TuningSpace ts;
    LineRenderer line;
    UIHandler ui;

    List<Val> mappings = new List<Val>(3);

    Mapping topMapping;
    
    public double top_damage;
    public double Complexity
    {
        get
        {
            return GetComplexity(TemperedInterval.Monzos, TemperedInterval.PrimeBasis);
        }
    }

    public string XenWikiURL
    {
        get
        {
            string url = XenConstants.XEN_WIKI_URL_BASE;
            if (!title.StartsWith("[") && !title.Contains("/") && !title.Contains("&"))
            {
                //this temperament has a friendly name
                url += title;
            }
            else
            {
                //edo
                if (TemperedInterval.Numerator.ToString().Contains("E") || TemperedInterval.Denominator.ToString().Contains("E"))
                    url += TemperedInterval.NumeratorBI + "/" + TemperedInterval.DenominatorBI;
                else
                    url += TemperedInterval.Numerator + "/" + TemperedInterval.Denominator;
            }
            return url;
        }
    }

    public string X31EQURL
    {
        get
        {
            string url = XenConstants.X31EQ_URL_BASE;
            float limit = Mathf.Max(ts.primes.AsArray);
            if (TemperedInterval.Numerator.ToString().Contains("E") || TemperedInterval.Denominator.ToString().Contains("E"))
                url += $"uv.cgi?uvs={TemperedInterval.NumeratorBI}%3A{TemperedInterval.DenominatorBI}&limit={limit}";
            else
                url += $"uv.cgi?uvs={TemperedInterval.Numerator}%3A{TemperedInterval.Denominator}&limit={limit}";
            return url;
        }
    }

    public string ScaleWorkshopURL
    {
        get
        {
            string url = XenConstants.SCALE_WORKSHOP_URL_BASE;
            string name = $"Rank 2 scale ({period}, {generator})";
            List<float> pitches = XenMath.getScalePitches(this, ts.primes, 15);
            string data = string.Join("%0A", pitches.ToArray());
            url += $"name={name}&data={data}";
            return url;
        }
    }

    public string XenCalcURL
    {
        get
        {
            string url = XenConstants.XEN_CALC_URL_BASE;
            if (TemperedInterval.Numerator.ToString().Contains("E") || TemperedInterval.Denominator.ToString().Contains("E"))
                url += TemperedInterval.NumeratorBI + "/" + TemperedInterval.DenominatorBI;
            else
                url += TemperedInterval.Numerator + "/" + TemperedInterval.Denominator;
            return url;
        }
    }

    public Interval period;
    public Interval generator;

    public string title;

    private Vector3 lastTransform;

    private Color? defaultTextColor = null;
    private Color? defaultLineColor = null;
    public TextMesh text;

    public void Initialize(float x, float y, float z, string name, PrimeBasis primes)
    {
        title = name;
        this.gameObject.name = name;
        TemperedInterval = new Interval(new Monzo(x, y, z), primes);
        if (z >= 0)
            wedgie = Tuple.Create(z, -y, x); //simple for rank-2, more complex math needed for higher ranks
        else
            wedgie = Tuple.Create(-z, y, -x);
        mappings = Get3Mappings(TemperedInterval.Monzos);
        GetProjection(mappings);
        AddCollider();
        Tuple<Interval, Interval> pergen = GetPerGen(wedgie, TemperedInterval.PrimeBasis);
        period = pergen.Item1;
        generator = pergen.Item2;
        if (TemperedInterval.Numerator < TemperedInterval.Denominator)
        {
            TemperedInterval.Monzos *= -1;
        }
        SetText();
        Val top = getTenneyOptimalTuning(TemperedInterval.NumeratorBI, TemperedInterval.DenominatorBI, TemperedInterval.PrimeBasis);
        Tuple<float,float,float> w_top = GetWeightedVals(top, TemperedInterval.PrimeBasis);
        Vector3 pos = (Vector3)ProjectionTools.Project(w_top);
        if (!float.IsNaN(pos.x) && !float.IsNaN(pos.y) && !float.IsNaN(pos.z))
            this.transform.position = pos;
        SetRotation();
        topMapping = ts.MakeTOPMapping(top.X, top.Y, top.Z, "TOP " + name);
        top_damage = getTenneyOptimalDamage(TemperedInterval.NumeratorBI, TemperedInterval.DenominatorBI);
    }

    public void Initialize(float x, float y, float z, PrimeBasis primes)
    {
        Initialize(x, y, z, "[ " + x + " " + y + " " + z + " >", primes);
    }

    public void Initialize(Mapping m1, Mapping m2, string name, PrimeBasis primes)
    {
        TemperedInterval = new Interval(new Monzo(AntiNullSpaceBasis(m1.vals.Vals, m2.vals.Vals)), primes);
        if (TemperedInterval.Monzos != Monzo.Zero)
        {
            name = NamedTemperaments.WhatTemperament(TemperedInterval.PrimeBasis, TemperedInterval.Monzos) ?? name;
            title = name;
            this.gameObject.name = name;
            Initialize(TemperedInterval.Monzos.X, TemperedInterval.Monzos.Y, TemperedInterval.Monzos.Z, name, primes);
        }
        else
        {
            throw new System.Exception($"Rank-2 cannot be formed by {m1} and {m2}");
        }
    }

    public void Initialize(Mapping m1, Mapping m2, PrimeBasis primes)
    {
        Initialize(m1, m2, (m1.vals.X + m1.warts + "&" + m2.vals.X + m2.warts).Replace("p",""), primes);
    }
    

    
    public void SetText()
    {
        SetTextFromStyle();
        SetCharacterSize();
    }

    public void SetCharacterSize()
    {
        //System.Numerics.BigInteger sz = (numerator / denominator) / ((System.Numerics.BigInteger) complexity * (System.Numerics.BigInteger)ts.scaling / 2) * (System.Numerics.BigInteger)ts.Zoom;
        //text.characterSize = (float)sz;
        System.Numerics.BigInteger lerp = 1 - (TemperedInterval.NumeratorBI / TemperedInterval.DenominatorBI);
        //text.characterSize = ((float)numerator / (float)denominator);
        text.characterSize = Mathf.Lerp(1.5f, 2, (float)lerp);
        text.characterSize /= (float)Complexity * (ts.scaling / ts.MetaZoom) / 2;
        text.characterSize = Mathf.Clamp(text.characterSize, .002f, .05f);
        text.characterSize *= ts.Zoom/1.5f;
        text.characterSize /= 5;
    }

    void Awake()
    {
        ts = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        ui = GameObject.Find("UI").GetComponent<UIHandler>();
        line = GetComponent<LineRenderer>();
        line.startWidth = .003f * ts.Zoom;
        line.endWidth = line.startWidth;
        text = GetComponent<TextMesh>();
        mappings = new List<Val>();
    }

    void Start()
    {
        ts.OnZoomChange += ZoomChangeHandler;
    } 

    // Update is called once per frame
    void Update()
    {
        //handle scaling somehow?
        //if (transform.position != lastTransform)
        //    GetProjection(monzos);
        //lastTransform = transform.position;
    }

    public List<Val> Get3Mappings(Monzo monzos)
    {
        List<Val> result = new List<Val>();

        float biggest = Mathf.Max(Mathf.Max(Mathf.Abs(monzos.X), Mathf.Abs(monzos.Y)), Mathf.Abs(monzos.Z));

        ///*if (monzos.x == 0)
        //{
        //    result.Add(new Vector3(-monzos.y, -monzos.z, monzos.y));
        //    result.Add(new Vector3(1, 0, 0));
        //}
        //else*/ if (Mathf.Abs(monzos.x) == biggest)
        //{
        //    result.Add(new Vector3(-monzos.z, 0, monzos.x));
        //    result.Add(new Vector3(-monzos.y, monzos.x, 0));
        //}
        //else if (Mathf.Abs(monzos.y) == biggest)
        //{
        //    result.Add(new Vector3(monzos.y, -monzos.x, 0));
        //    result.Add(new Vector3(0, -monzos.z, monzos.y));
        //}
        //else if (Mathf.Abs(monzos.z) == biggest)
        //{
        //    result.Add(new Vector3(monzos.z, 0, -monzos.x));
        //    result.Add(new Vector3(0, monzos.z, -monzos.y));
        //}

        Val mapping1 = new Val(-monzos.Z, 0, monzos.X);
        if (mapping1.X < 0)
            mapping1 *= -1;
        Val mapping2 = new Val(-monzos.Y, monzos.X, 0);
        if (mapping2.X < 0)
            mapping2 *= -1;
        Val mapping3 = new Val(0, -monzos.Z, monzos.Y);
        if (mapping3.X < 0)
            mapping3 *= -1;

        //order to ensure most important mappings (usually) go in front
        if (Mathf.Abs(monzos.X) == biggest)
        {
            result.Add(mapping1);
            result.Add(mapping2);
            result.Add(mapping3);
        }
        else if(Mathf.Abs(monzos.Y) == biggest)
        {
            result.Add(mapping2);
            result.Add(mapping3);
            result.Add(mapping1);
        }
        else if (Mathf.Abs(monzos.Z) == biggest)
        {
            result.Add(mapping1);
            result.Add(mapping3);
            result.Add(mapping2);
        }

        return result;
    }

    public void GetProjection(Monzo monzos)
    {
        List<Val> mappings = Get3Mappings(monzos);
        GetProjection(mappings);
    }
     
    public void GetProjection(List<Val> mappings)
    {
        Vector3 m1 = ProjectionTools.Project(GetWeightedVals(mappings[0], TemperedInterval.PrimeBasis));
        Vector3 m2 = ProjectionTools.Project(GetWeightedVals(mappings[1], TemperedInterval.PrimeBasis));
        Vector3 m3 = ProjectionTools.Project(GetWeightedVals(mappings[2], TemperedInterval.PrimeBasis));

        List<Vector3> positions = new List<Vector3>();
        if (!float.IsNaN(m1.x) && !float.IsNaN(m1.y) && !float.IsNaN(m1.z))
            positions.Add(m1);
        if (!float.IsNaN(m2.x) && !float.IsNaN(m2.y) && !float.IsNaN(m2.z))
            positions.Add(m2);
        if (!float.IsNaN(m3.x) && !float.IsNaN(m3.y) && !float.IsNaN(m3.z))
            positions.Add(m3);

        line.positionCount = positions.Count;
        line.SetPositions(positions.ToArray());

        float d1 = Vector3.Distance(m2, m3);
        float d2 = Vector3.Distance(m1, m3);
        float d3 = Vector3.Distance(m1, m2);
        float maxd = Mathf.Max(Mathf.Max(d1, d2), d3);

        if (maxd == d1)
        {
            transform.position = Vector3.Lerp(m2, m3, 0.5f);
        }
        else if (maxd == d2)
        {
            transform.position = Vector3.Lerp(m1, m3, 0.5f);
        }
        else if (maxd == d3)
        {
            transform.position = Vector3.Lerp(m1, m2, 0.5f);
        }

        //Vector3 v = m1 - m2;
        //this.transform.rotation = Quaternion.identity;
        //this.transform.Rotate(new Vector3(0, 0, Mathf.Atan(v.y / v.x) * Mathf.Rad2Deg));
        //if (ts.JIP() != null)
        //    if (this.transform.position.y > ts.JIP().transform.position.y || (this.transform.position.y == ts.JIP().transform.position.y && this.transform.position.x > ts.JIP().transform.position.x))
        //        this.transform.Rotate(new Vector3(0, 0, 180));

        SetRotation();
    }

    void SetRotation()
    {
        Vector3 m1 = ProjectionTools.Project(GetWeightedVals(mappings[0], TemperedInterval.PrimeBasis));
        Vector3 m2 = ProjectionTools.Project(GetWeightedVals(mappings[1], TemperedInterval.PrimeBasis));
        Vector3 m3 = ProjectionTools.Project(GetWeightedVals(mappings[2], TemperedInterval.PrimeBasis));

        Vector3 v = m1 - m2;
        this.transform.rotation = Quaternion.identity;
        this.transform.Rotate(new Vector3(0, 0, Mathf.Atan(v.y / v.x) * Mathf.Rad2Deg));
        if (ts.JIP() != null)
            if (this.transform.position.y > ts.JIP().transform.position.y || (this.transform.position.y == ts.JIP().transform.position.y 
                                                                         && ((this.transform.position.x > ts.JIP().transform.position.x && this.transform.rotation.eulerAngles.z < 0) 
                                                                          || (this.transform.position.x < ts.JIP().transform.position.x && this.transform.rotation.eulerAngles.z > 0))))
                this.transform.Rotate(new Vector3(0, 0, 180));
    }

    string SetTextFromStyle()
    {
        string txt;
        switch (ts.commaTextStyle)
        {
            case TuningSpace.CommaTextStyle.NAME:
                txt = name;
                break;
            case TuningSpace.CommaTextStyle.RATIO:
                txt = TemperedInterval.NumeratorBI + " / " + TemperedInterval.DenominatorBI;
                break;
            case TuningSpace.CommaTextStyle.FULL_MONZOS:
                txt = TemperedInterval.Monzos.ToString();
                break;
            default:
                txt = name;
                break;
        }
        text.text = txt;
        if (defaultTextColor == null)
            defaultTextColor = text.color;
        if (defaultLineColor == null)
            defaultLineColor = line.startColor;
        return txt;
    }

    public override string ToString()
    {
        return TemperedInterval.Monzos.ToString();
    }

    BoxCollider col; 
    private void AddCollider()
    {
        col = GetComponent<BoxCollider>();
        if (col == null)
            col = gameObject.AddComponent<BoxCollider>();
        Vector3 m1 = ProjectionTools.Project(GetWeightedVals(mappings[0], TemperedInterval.PrimeBasis));
        Vector3 m2 = ProjectionTools.Project(GetWeightedVals(mappings[1], TemperedInterval.PrimeBasis));
        Vector3 m3 = ProjectionTools.Project(GetWeightedVals(mappings[2], TemperedInterval.PrimeBasis));

        float d1 = Vector3.Distance(m2, m3);
        float d2 = Vector3.Distance(m1, m3);
        float d3 = Vector3.Distance(m1, m2);
        float maxd = Mathf.Max(Mathf.Max(d1, d2), d3);

        float lineLength = maxd; // length of line
   
        col.size = new Vector3(lineLength * 2, .002f * ts.Zoom, .002f * ts.Zoom); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
    }

    private void ZoomChangeHandler(float newVal)
    {
        //resize stuff
        line.startWidth = .003f * newVal/1.5f;
        line.endWidth = line.startWidth;
        SetCharacterSize();
    }

    private void OnDestroy()
    {
        // Unsubscribe from event(s)
        ts.OnZoomChange -= ZoomChangeHandler;

        // And stop all coroutines
        StopAllCoroutines();

        ts.Remove(this);
        if (topMapping != null && topMapping.gameObject != null)
            Destroy(topMapping.gameObject);
    }

    public bool Equals(Comma c)
    {
        return (this.TemperedInterval.Monzos == c.TemperedInterval.Monzos);
    }

    
    public void OnSelect()
    {
        ui.UpdatePTSObjectInfo(this);
        text.color = Color.yellow;
        line.startColor = Color.yellow;
        line.endColor = line.startColor;
    }

    public void OnJoinInit()
    {
        text.color = Color.blue;
        line.startColor = Color.blue;
        line.endColor = line.startColor;
    }

    public void OnDeselect()
    {
        if (this)
        {
            text.color = defaultTextColor ?? Color.black;
            line.startColor = defaultLineColor ?? Color.magenta;
            line.endColor = line.startColor;
        }
    }

    public void OnMouseEnter()
    {
        if (!ui.mouseInUI)
        {
            if (this != (object)ts.SelectedObject)
            {
                text.color = Color.cyan;
                line.startColor = Color.cyan;
                line.endColor = line.startColor;
            }
        }
    }

    public void OnMouseOver()
    {
        if (ui.mouseInUI)
        {
            if (this != (object)ts.SelectedObject)
            {
                text.color = defaultTextColor ?? Color.black;
                line.startColor = defaultLineColor ?? Color.magenta;
                line.endColor = line.startColor;
            }
        }
    }

    private void OnMouseExit()
    {
        if (this != (object)ts.SelectedObject)
        {
            text.color = defaultTextColor ?? Color.black;
            line.startColor = defaultLineColor ?? Color.magenta;
            line.endColor = line.startColor;
        }
    }
}
