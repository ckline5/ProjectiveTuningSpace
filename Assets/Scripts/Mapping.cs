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

    //TuningSpace TuningSpace.Instance;
    //UIHandler UIHandler.Instance;

    public string warts;

    public float topOffset;

    public float stepSize;
    
    public Val parentVal;

    public float colliderThickness = .001f;

    private Color? defaultColor = null;
    public TextMesh text;

    BoxCollider col;
    Renderer renderr;

    public string XenWikiURL
    {
        get
        {
            string url = XenConstants.XEN_WIKI_URL_BASE;
            if (TuningSpace.Instance.primes.X == 2)
            {
                //edo
                url += vals.X + "edo";
            }
            else if (TuningSpace.Instance.primes.X == 3)
            {
                //edt
                url += vals.X + "edt";
            }
            else
            {
                url += vals.X + "ed" + TuningSpace.Instance.primes.X;
            }
            return url;
        }
    }

    public string X31EQURL
    {
        get
        {
            string url = XenConstants.X31EQ_URL_BASE;
            float limit = Mathf.Max(TuningSpace.Instance.primes.AsArray);
            url += $"rt.cgi?ets={vals.X}{warts}&limit={limit}";
            return url;
        }
    }

    public string ScaleWorkshopURL
    {
        get
        {
            string url = XenConstants.SCALE_WORKSHOP_URL_BASE;
            string name = $"{vals.X} Equal Divisions of {TuningSpace.Instance.primes.X}";
            if (TuningSpace.Instance.primes.X % 1 == 0)
                name += "/1";
            List<float> pitches = XenMath.getScalePitches(this, TuningSpace.Instance.primes);
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
            if (TuningSpace.Instance.primes.X == 2)
            {
                //edo
                url += vals.X + "EDO";
            }
            else
            {
                url += vals.X + "ED" + TuningSpace.Instance.primes.X;
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
        w_vals = GetWeightedVals(vals, TuningSpace.Instance.primes);
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
        w_vals = GetWeightedVals(vals, TuningSpace.Instance.primes);
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
        Val top = getTenneyOptimalTuning(vals, TuningSpace.Instance.primes);
        topOffset = getEtCents(top.X - vals.X, vals.X);
    }

    public void SetTopRotation()
    {
        transform.localRotation = Quaternion.identity;
        transform.Rotate(Vector3.back, (topOffset * TuningSpace.Instance.TopRotationValue));
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
        //TuningSpace.Instance = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        //UIHandler.Instance = GameObject.Find("UI").GetComponent<UIHandler>();
        text = GetComponent<TextMesh>();
        renderr = GetComponent<Renderer>();
    }

    private void Start()
    {
        TuningSpace.Instance.OnTopRotationValueChange += TopRotationValueChangeHandler;
        TuningSpace.Instance.OnZoomChange += ZoomChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public string SetTextFromStyle()
    {
        string txt;
        if (!TuningSpace.Instance.displayEncipheredVals && this.mappingType == MappingType.ET_ENCIPHERED)
        {
            txt = "";
        }
        else
        {
            if (mappingType == MappingType.ET || mappingType == MappingType.ET_ENCIPHERED)
            {
                switch (TuningSpace.Instance.mappingTextStyle)
                {
                    case TuningSpace.MappingTextStyle.EDO:
                        txt = (vals.X / Mathf.Log(TuningSpace.Instance.primes.X, 2)).ToString();
                        break;
                    case TuningSpace.MappingTextStyle.EDO_WITH_WARTS:
                        txt = (vals.X / Mathf.Log(TuningSpace.Instance.primes.X, 2)).ToString();
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
                        txt = (vals.X / Mathf.Log(TuningSpace.Instance.primes.X, 2)).ToString();
                        break;
                }
                text.characterSize = GetSize();
                switch (TuningSpace.Instance.mappingColorStyle)
                {
                    case TuningSpace.MappingColorStyle.BLACK:
                        text.color = Color.black;
                        break;
                    case TuningSpace.MappingColorStyle.RAINBOW_TOP:
                        text.color = Color.HSVToRGB(((256 * 256 + 100 + topOffset * TuningSpace.Instance.TopRotationValue) % 256) / 256, 1, 1);
                        break;
                    case TuningSpace.MappingColorStyle.RAINBOW_INTERVAL:
                        if (TuningSpace.Instance.rainbowIntervalSelection.Monzos != null)
                        {
                            float cents = getEtCents(getSteps(this.vals, new Interval(TuningSpace.Instance.rainbowIntervalSelection, TuningSpace.Instance.primes)), this.vals.X);
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
                //size /= Mathf.Log(TuningSpace.Instance.scaling / TuningSpace.Instance.MetaZoom * .5f);
                size /= 3.69897f;
                //size *= (TuningSpace.Instance.Zoom + 1) / 2f;
                size *= Mathf.Log(((TuningSpace.Instance.Zoom + .1f) *.5f + 1)) * TuningSpace.Instance.MetaZoom;
                break;
            case MappingType.JIP:
                size = (1 / TuningSpace.Instance.scaling) * TuningSpace.Instance.Zoom/1.5f;
                break;
            case MappingType.TOP:
                size = (.6f / TuningSpace.Instance.scaling) * TuningSpace.Instance.Zoom/1.5f;
                break;
            default:
                break;
        }
        return size/6;
    }

    string GetWart()
    {
        string txt = "";
        if (IsPatentVal(vals, TuningSpace.Instance.primes))
        {
            txt += "p";
        }
        else
        {
            Val pat = GetPatentVal(vals.X, TuningSpace.Instance.primes);
            Tuple<int,int,int> offset = GetOffsetFromPatentVal(vals, TuningSpace.Instance.primes);

            if (offset.Item1 > 0)
            {
                char ch = (char)('a' + GetNumberOfPrime((int)TuningSpace.Instance.primes.X));
                for (int i = 0; i < offset.Item1; i++)
                    txt += ch;
            }
            if (offset.Item2 > 0)
            {
                char ch = (char)('a' + GetNumberOfPrime((int)TuningSpace.Instance.primes.Y));
                for (int i = 0; i < offset.Item2; i++)
                    txt += ch;
            }
            if (offset.Item3 > 0)
            {
                char ch = (char)('a' + GetNumberOfPrime((int)TuningSpace.Instance.primes.Z));
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
            col.center = Vector3.zero;
            col.size = new Vector3(renderr.bounds.size.x, renderr.bounds.size.y, colliderThickness);
        }
        else
        {
            col.size = Vector3.zero;
        }
    }

    private void TopRotationValueChangeHandler(float newVal)
    {
        SetTopRotation();
        if (TuningSpace.Instance.mappingColorStyle == TuningSpace.MappingColorStyle.RAINBOW_TOP)
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
        TuningSpace.Instance.OnTopRotationValueChange -= TopRotationValueChangeHandler;
        TuningSpace.Instance.OnZoomChange -= ZoomChangeHandler;

        // And stop all coroutines
        StopAllCoroutines();

        TuningSpace.Instance.Remove(this);
    }

    public bool Equals(Mapping m)
    {
        return (this.vals.Equals(m.vals));
    }


    public void OnSelect()
    {
        UIHandler.Instance.UpdatePTSObjectInfo(this);
        text.color = Color.yellow;
    }

    public void OnJoinInit()
    {
        text.color = Color.blue;
    }

    public void OnJoinCancel()
    {
        text.color = Color.yellow;
    }

    public void OnDeselect()
    {
        if (this)
            text.color = defaultColor ?? Color.black;
    }

    public void OnMouseEnter()
    {
        if (!UIHandler.Instance.mouseInUI)
            if (this != (object)TuningSpace.Instance.SelectedObject && this.mappingType != MappingType.JIP)
                text.color = Color.cyan;
    }

    public void OnMouseOver()
    {
        if (UIHandler.Instance.mouseInUI)
            if (this != (object)TuningSpace.Instance.SelectedObject && this.mappingType != MappingType.JIP)
                text.color = defaultColor ?? Color.black;
    }

    private void OnMouseExit()
    {
        if (this != (object)TuningSpace.Instance.SelectedObject && this.mappingType != MappingType.JIP)
            text.color = defaultColor ?? Color.black;
    }

}
