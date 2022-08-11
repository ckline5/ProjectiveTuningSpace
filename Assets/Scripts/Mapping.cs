using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static XenObjects;
using static XenMath;
using System;
using static ProjectionTools;

public class Mapping : MonoBehaviour, PTSObject
{
    public Val vals;
    public Tuple<float,float,float> w_vals;
    Vector2 projection;

    TuningSpace ts;
    UIHandler ui;

    public string warts;

    public float topOffset;

    public float stepSize;
    
    public Val parentVal;

    private Color? defaultColor = null;
    public TextMesh text;

    BoxCollider col;

    public string XenWikiURL
    {
        get
        {
            string url = XenConstants.XEN_WIKI_URL_BASE;
            if (ts.primes.X == 2)
            {
                //edo
                url += vals.X + "edo";
            }
            else if (ts.primes.X == 3)
            {
                //edt
                url += vals.X + "edt";
            }
            else
            {
                url += vals.X + "ed" + ts.primes.X;
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
            url += $"rt.cgi?ets={vals.X}{warts}&limit={limit}";
            return url;
        }
    }

    public string ScaleWorkshopURL
    {
        get
        {
            string url = XenConstants.SCALE_WORKSHOP_URL_BASE;
            string name = $"{vals.X} Equal Divisions of {ts.primes.X}";
            if (ts.primes.X % 1 == 0)
                name += "/1";
            List<float> pitches = XenMath.getScalePitches(this, ts.primes);
            List<string> pitchStrings = new List<string>();
            pitches.ForEach(p => pitchStrings.Add(p % 1 == 0 ? p.ToString() + "." : p.ToString()));
            string data = string.Join("%0A", pitchStrings.ToArray());
            url += $"name={name}&data={data}";
            return url;
        }
    }

    public string XenCalcURL
    {
        get
        {
            string url = XenConstants.XEN_CALC_URL_BASE;
            if (ts.primes.X == 2)
            {
                //edo
                url += vals.X + "EDO";
            }
            else
            {
                url += vals.X + "ED" + ts.primes.X;
            }
            return url;
        }
    }

    public enum MappingType
    {
        ET,
        ET_ENCIPHERED,
        TOP,
        JIP
    };
    public MappingType mappingType;

    public void Initialize(float x, float y, float z)
    {
        vals = new Val(x, y, z);
        mappingType = MappingType.ET;
        SetEnciphering();
        SetStepSize();
        w_vals = GetWeightedVals(vals, ts.primes);
        GetProjection();
        GetTop();
        SetTopRotation();
        SetText();
        warts = GetWart();
        this.gameObject.name = this.ToString();
    }

    public void MakeTOPTuning(float x, float y, float z, string name)
    {
        vals = new Val(x, y, z);
        mappingType = MappingType.TOP;
        w_vals = GetWeightedVals(vals, ts.primes);
        GetProjection();
        this.gameObject.name = name;
        SetText();
    }

    public void Initialize(Comma c1, Comma c2)
    {
        vals = new Val(AntiNullSpaceBasis(c1.TemperedInterval.Monzos.Monzos, c2.TemperedInterval.Monzos.Monzos));
        if (vals != Val.Zero)
        {
            Initialize(vals.X, vals.Y, vals.Z);
        }
        else
        {
            throw new System.Exception($"Rank-1 cannot be formed by {c1} and {c2}");
        }
    }

    public void MakeJIP()
    {
        w_vals = Tuple.Create(1f, 1f, 1f);
        mappingType = MappingType.JIP;
        GetProjection();
        this.gameObject.name = "< J I P ]";
        SetText();
    }

    void SetEnciphering()
    {
        if ((int)vals.X == vals.X
            && (int)vals.Y == vals.Y
            && (int)vals.Z == vals.Z)
        {
            bool isEnciphered = gcd(gcd((int)vals.X, (int)vals.Y), (int)vals.Z) == 1 ? false : true;
            if (isEnciphered)
            {
                parentVal = vals / gcd(gcd((int)vals.X, (int)vals.Y), (int)vals.Z);
                mappingType = MappingType.ET_ENCIPHERED;
            }
        }
    }

    void GetProjection()
    {
        //3-point perspective
        //projection = pt.ThreePointPerspective(w_vals);
        projection = ProjectionTools.Project(w_vals);
        //projection = pt.GnomonicProjection(w_vals);
        //projection = pt.MyProjection(w_vals);
        transform.position += (Vector3)projection;
    }

    public void GetTop()
    {
        Val top = getTenneyOptimalTuning(vals, ts.primes);
        topOffset = getEtCents(top.X - vals.X, vals.X);
    }

    public void SetTopRotation()
    {
        transform.localRotation = Quaternion.identity;
        transform.Rotate(Vector3.back, (topOffset * ts.TopRotationValue));
    }

    public void SetStepSize()
    {
        stepSize = getEtCents(1, vals.X);
    }

    public void SetText()
    {
        SetTextFromStyle();
        AddCollider();
    }
    
    void Awake()
    {
        ts = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        ui = GameObject.Find("UI").GetComponent<UIHandler>();
        text = GetComponent<TextMesh>();
    }

    private void Start()
    {
        ts.OnTopRotationValueChange += TopRotationValueChangeHandler;
        ts.OnZoomChange += ZoomChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public string SetTextFromStyle()
    {
        string txt;
        if (!ts.displayEncipheredVals && this.mappingType == MappingType.ET_ENCIPHERED)
        {
            txt = "";
        }
        else
        {
            if (mappingType == MappingType.ET || mappingType == MappingType.ET_ENCIPHERED)
            {
                switch (ts.mappingTextStyle)
                {
                    case TuningSpace.MappingTextStyle.EDO:
                        txt = (vals.X / Mathf.Log(ts.primes.X, 2)).ToString();
                        break;
                    case TuningSpace.MappingTextStyle.EDO_WITH_WARTS:
                        txt = (vals.X / Mathf.Log(ts.primes.X, 2)).ToString();
                        txt += warts;
                        break;
                    case TuningSpace.MappingTextStyle.X_VAL:
                        txt = vals.X.ToString();
                        break;
                    case TuningSpace.MappingTextStyle.Y_VAL:
                        txt = vals.Y.ToString();
                        break;
                    case TuningSpace.MappingTextStyle.Z_VAL:
                        txt = vals.Z.ToString();
                        break;
                    case TuningSpace.MappingTextStyle.FULL_VALS:
                        txt = vals.ToString();
                        break;
                    case TuningSpace.MappingTextStyle.DOTS:
                        txt = "•";
                        break;
                    default:
                        txt = (vals.X / Mathf.Log(ts.primes.X, 2)).ToString();
                        break;
                }
                text.characterSize = GetSize();
                switch (ts.mappingColorStyle)
                {
                    case TuningSpace.MappingColorStyle.BLACK:
                        text.color = Color.black;
                        break;
                    case TuningSpace.MappingColorStyle.RAINBOW_TOP:
                        text.color = Color.HSVToRGB(((256 * 256 + 100 + topOffset * ts.TopRotationValue) % 256) / 256, 1, 1);
                        break;
                    case TuningSpace.MappingColorStyle.RAINBOW_INTERVAL:
                        if (ts.rainbowIntervalSelection.Monzos != null)
                        {
                            float cents = getEtCents(getSteps(this.vals, new Interval(ts.rainbowIntervalSelection, ts.primes)), this.vals.X);
                            if (cents == 0)
                                text.color = Color.black;
                            else
                            {
                                float l;
                                l = cents * 256/1200 % 256;
                                while (l < 0)
                                    l += 256;
                                text.color = Color.HSVToRGB(Mathf.Lerp(0,1,l/256), 1, 1);
                            }
                        }
                        break;
                    default:
                        text.color = Color.black;
                        break;
                }
                defaultColor = text.color;
            }
            else
            {
                txt = "•";
                if (mappingType == MappingType.TOP)
                {
                    text.characterSize = GetSize();
                    text.color = Color.cyan;
                }
                else if (mappingType == MappingType.JIP)
                {
                    text.characterSize = GetSize();
                    text.color = Color.red;
                }
            }
        }
        text.text = txt;
        if (defaultColor == null)
            defaultColor = text.color;
        return txt;
    }
          
    public float GetSize()
    {
        float size = 0;
        switch(mappingType)
        {
            case MappingType.ET:
            case MappingType.ET_ENCIPHERED:
                size = vals.X != 0 ? 1 / vals.X : 1;
                size /= Mathf.Sqrt(ts.scaling / ts.MetaZoom);
                size *= (ts.Zoom + 1) / 2f;
                break;
            case MappingType.JIP:
                size = (1 / ts.scaling) * ts.Zoom/1.5f;
                break;
            case MappingType.TOP:
                size = (.8f / ts.scaling) * ts.Zoom/1.5f;
                break;
            default:
                break;
        }
        return size/5;
    }

    string GetWart()
    {
        string txt = "";
        if (IsPatentVal(vals, ts.primes))
        {
            txt += "p";
        }
        else
        {
            Val pat = GetPatentVal(vals.X, ts.primes);
            Tuple<int,int,int> offset = GetOffsetFromPatentVal(vals, ts.primes);

            if (offset.Item1 > 0)
            {
                char ch = (char)('a' + GetNumberOfPrime((int)ts.primes.X));
                for (int i = 0; i < offset.Item1; i++)
                    txt += ch;
            }
            if (offset.Item2 > 0)
            {
                char ch = (char)('a' + GetNumberOfPrime((int)ts.primes.Y));
                for (int i = 0; i < offset.Item2; i++)
                    txt += ch;
            }
            if (offset.Item3 > 0)
            {
                char ch = (char)('a' + GetNumberOfPrime((int)ts.primes.Z));
                for (int i = 0; i < offset.Item3; i++)
                    txt += ch;
            }
        }
        return txt;
    }

    public override string ToString()
    {
        return vals.ToString();
    }

    private void AddCollider()
    {
        col = gameObject.GetComponent<BoxCollider>();
        if (col == null)
            col = this.gameObject.AddComponent<BoxCollider>();
        if (!string.IsNullOrWhiteSpace(text.text))
        {
            Renderer renderer = GetComponent<Renderer>();
            col.center = Vector3.zero;
            col.size = new Vector3(renderer.bounds.size.x, renderer.bounds.size.y, .01f);
        }
        else
        {
            col.size = Vector3.zero;
        }
    }

    private void TopRotationValueChangeHandler(float newVal)
    {
        SetTopRotation();
        if (ts.mappingColorStyle == TuningSpace.MappingColorStyle.RAINBOW_TOP)
        {
            SetTextFromStyle();
        }
    }

    private void ZoomChangeHandler(float newVal)
    {
        //resize stuff
        text.characterSize = GetSize();
        AddCollider();
    }

    private void OnDestroy()
    {
        // Unsubscribe from event(s)
        ts.OnTopRotationValueChange -= TopRotationValueChangeHandler;
        ts.OnZoomChange -= ZoomChangeHandler;

        // And stop all coroutines
        StopAllCoroutines();

        ts.Remove(this);
    }

    public bool Equals(Mapping m)
    {
        return (this.vals.Equals(m.vals));
    }


    public void OnSelect()
    {
        ui.UpdatePTSObjectInfo(this);
        text.color = Color.yellow;
    }

    public void OnJoinInit()
    {
        text.color = Color.blue;
    }

    public void OnDeselect()
    {
        if (this)
            text.color = defaultColor ?? Color.black;
    }

    public void OnMouseEnter()
    {
        if (!ui.mouseInUI)
            if (this != (object)ts.SelectedObject && this.mappingType != MappingType.JIP)
                text.color = Color.cyan;
    }

    public void OnMouseOver()
    {
        if (ui.mouseInUI)
            if (this != (object)ts.SelectedObject && this.mappingType != MappingType.JIP)
                text.color = defaultColor ?? Color.black;
    }

    private void OnMouseExit()
    {
        if (this != (object)ts.SelectedObject && this.mappingType != MappingType.JIP)
            text.color = defaultColor ?? Color.black;
    }

}
