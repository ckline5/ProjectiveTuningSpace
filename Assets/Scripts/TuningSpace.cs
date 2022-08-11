using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static XenObjects;
using static XenMath;

public class TuningSpace : MonoBehaviour
{
    private UIHandler ui;
    FPSFlyer player;

    public PrimeBasis primes;
    public float max_val = 100;
    public int max_offset = 2;
    public float scaling = 100;
    public bool displayEncipheredVals;
    private bool prevDisplayEncipheredvals;
    [SerializeField]
    private float topRotationValue = 6 * Mathf.Deg2Rad;
    public float TopRotationValue
    {
        get { return topRotationValue; }
        set
        {
            if (topRotationValue == value) return;
            topRotationValue = value;
            OnTopRotationValueChange?.Invoke(topRotationValue);
        }
    }
    public delegate void OnTopRotationValueChangeDelegate(float newVal);
    public event OnTopRotationValueChangeDelegate OnTopRotationValueChange;

    public float topRotationSpeed = 0;

    public Mapping mapping_prefab;
    public Comma comma_prefab;
    public DamageHexagon hexagon_prefab;
    public MOS mos_prefab;

    //public static readonly int[] prime_numbers = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

    [SerializeField]
    private float zoom = 1;
    public float Zoom
    {
        get { return zoom; }
        set
        {
            if (zoom == value) return;
            zoom = value;
            OnZoomChange?.Invoke(zoom);
        }
    }
    public delegate void OnZoomChangeDelegate(float newVal);
    public event OnZoomChangeDelegate OnZoomChange;

    private float metaZoom = 1;
    public float MetaZoom
    {
        get { return metaZoom; }
        set
        {
            if (metaZoom == value) return;
            metaZoom = value;
            OnZoomChange?.Invoke(zoom);
        }
    }

    Mapping jip;
    List<Mapping> mappings = new List<Mapping>();
    List<Comma> commas = new List<Comma>();
    List<DamageHexagon> hexagons = new List<DamageHexagon>();

    public enum JoinMode
    {
        NONE,
        MAPPING,
        COMMA
    };
    public JoinMode joinMode;

    private PTSObject selectedObject = null;
    public PTSObject joinObject = null;
    public PTSObject SelectedObject
    {
        get { return selectedObject; }
        set
        {
            if (selectedObject == value)
                return;

            var oldVal = selectedObject;
            selectedObject = value;
            if (oldVal != null)
                oldVal.OnDeselect();
            if (selectedObject != null)
                selectedObject.OnSelect();
            else
                ui.HidePTSObjectInfo();
            //OnSelectedObjectChange?.Invoke(oldVal, false);
            //OnSelectedObjectChange?.Invoke(selectedObject, true);
            if (oldVal != null && selectedObject != null && oldVal.GetType() == SelectedObject.GetType())
            {
                switch (joinMode)
                {
                    case JoinMode.MAPPING:
                        Comma c = MakeComma((Mapping)oldVal, (Mapping)SelectedObject);
                        joinMode = JoinMode.NONE;
                        player.SetPosition(c.transform.position);
                        SelectedObject = c;
                        break;
                    case JoinMode.COMMA:
                        Mapping m = MakeMapping((Comma)oldVal, (Comma)SelectedObject);
                        joinMode = JoinMode.NONE;
                        player.SetPosition(m.transform.position + new Vector3(0, 0, -1));
                        SelectedObject = m;
                        break;
                    case JoinMode.NONE:
                    default:
                        break;
                }
            }
        }
    }
    //public delegate void OnSelectedObjectChangeDelegate(PTSObject obj, bool select);
    //public event OnSelectedObjectChangeDelegate OnSelectedObjectChange;

    public enum MappingTextStyle
    {
        EDO,
        EDO_WITH_WARTS,
        X_VAL,
        Y_VAL,
        Z_VAL,
        FULL_VALS,
        DOTS
    };
    public MappingTextStyle mappingTextStyle;
    private MappingTextStyle prevMappingTextStyle;

    public enum CommaTextStyle
    {
        NAME,
        RATIO,
        FULL_MONZOS
    }
    public CommaTextStyle commaTextStyle;
    private CommaTextStyle prevCommaTextStyle;

    public enum MappingColorStyle
    {
        BLACK,
        RAINBOW_TOP,
        RAINBOW_INTERVAL
    }
    public MappingColorStyle mappingColorStyle;
    private MappingColorStyle prevMappingColorStyle;
    public Monzo rainbowIntervalSelection = new Monzo(0,0,0);
    private Monzo prevRainbowIntervalSelection = new Monzo(0,0,0);

    Coroutine populateMappings;

    private void Awake()
    {
        ui = GameObject.Find("UI").GetComponent<UIHandler>();
        player = GameObject.Find("Player").GetComponent<FPSFlyer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //OnSelectedObjectChange += OnSelectedObjectChangeHandler;
        //StartCoroutine(ProceduralPTS(5, 7, .01f));
    }

    IEnumerator ProceduralPTS(float from, float to, float by)
    {
        MakeJIP();
        MakeDamageHexagons(10);
        ui.ShowHideMenu();
        yield return new WaitForSeconds(3f);
        int count = 0;
        for (float z = from; z <= to + by / 2; z += by)
        {
            primes = new PrimeBasis(2, 3, z);
            for (int i = 0; i < max_val; i++)
            {
                //first get the patent val
                Val patentVal = GetPatentVal(i, primes);
                for (int j = (int)(patentVal.Y - max_offset >= 0 ? patentVal.Y - max_offset : 0); j <= patentVal.Y + max_offset; j++)
                {
                    for (int k = (int)(patentVal.Z - max_offset >= 0 ? patentVal.Z - max_offset : 0); k <= patentVal.Z + max_offset; k++)
                    {
                        Mapping m = MakeMapping(i, j, k);
                    }
                }
            }
            ScreenCapture.CaptureScreenshot($@"C:\PTS\{count}.png");
            yield return new WaitForSeconds(1f);
            DeleteAllMappings();
            count++;
        }
    }

    private void OnDestroy()
    {
        //OnSelectedObjectChange -= OnSelectedObjectChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        TopRotationValue += topRotationSpeed / 100;

        if (prevDisplayEncipheredvals != displayEncipheredVals)
        {
            foreach (Mapping m in mappings)
            {
                m.SetText();
            }
            prevDisplayEncipheredvals = displayEncipheredVals;
        }
        if (prevMappingTextStyle != mappingTextStyle)
        {
            foreach (Mapping m in mappings)
            {
                m.SetText();
            }
            prevMappingTextStyle = mappingTextStyle;
        }
        if (prevMappingColorStyle != mappingColorStyle)
        {
            foreach (Mapping m in mappings)
            {
                m.SetText();
            }
            prevMappingColorStyle = mappingColorStyle;
        }
        if (prevRainbowIntervalSelection != rainbowIntervalSelection)
        {
            foreach (Mapping m in mappings)
            {
                m.SetTextFromStyle();
            }
            prevRainbowIntervalSelection = rainbowIntervalSelection;
        }
        if (prevCommaTextStyle != commaTextStyle)
        {
            foreach (Comma c in commas)
            {
                c.SetText();
            }
            prevCommaTextStyle = commaTextStyle;
        }
        /*if (prevTopRotationValue != topRotationValue)
        {
            foreach (Mapping m in mappings)
            {
                m.SetTopRotation();
            }
            prevTopRotationValue = topRotationValue;
        }*/
    }

    /// <summary>
    /// Create tuning space
    /// </summary>
    public void MakeMappings()
    {
        DeleteAllMappings();
        DeleteAllCommas();
        populateMappings = StartCoroutine(PopulateMappings());
    }

    /// <summary>
    /// draw default 5-limit commas from Paul's paper
    /// </summary>
    public void MakeDefaultCommas()
    {
        MakeComma(4, -1, -1, "Father"); //Father
        MakeComma(0, 3, -2, "Bug"); //Bug
        MakeComma(-3, -1, 2, "Dicot"); //Dicot
        MakeComma(-4, 4, -1, "Meantone"); //Meantone
        MakeComma(7, 0, -3, "Augmented"); //Augmented
        MakeComma(-7, 3, 1, "Mavila"); //Mavila
        MakeComma(1, -5, 3, "Porcupine"); //Porcupine
        MakeComma(8, -5, 0, "Blackwood"); //Blackwood
        MakeComma(3, 4, -4, "Dimipent"); //Dimipent
        MakeComma(11, -4, -2, "Srutal"); //Srutal
        MakeComma(-10, -1, 5, "Magic"); //Magic
        MakeComma(-1, 8, -5, "Ripple"); //Ripple
        MakeComma(-6, -5, 6, "Hanson"); //Hanson
        MakeComma(-14, 3, 4, "Negripent"); //Negripent
        MakeComma(5, -9, 4, "Tetracot"); //Tetracot
        MakeComma(12, -9, 1, "Superpyth"); //Superpyth
        MakeComma(-15, 8, 1, "Helmholtz"); //Helmholtz
        MakeComma(2, 9, -7, "Sensipent"); //Sensipent
        MakeComma(18, -4, -5, "Passion"); //Passion
        MakeComma(17, 1, -8, "Würschmidt"); //Würschmidt
        MakeComma(-19, 12, 0, "Compton"); //Compton
        MakeComma(9, -13, 5, "Amity"); //Amity
        MakeComma(-21, 3, 7, "Orson"); //Orson
        MakeComma(23, 6, -14, "Vishnu"); //Vishnu
        MakeComma(38, -2, -15, "Luna"); //Luna
    }

    public void DeleteAll()
    {
        DeleteAllMappings();
        DeleteAllCommas();
        foreach (DamageHexagon h in hexagons)
        {
            Delete(h);
        }
        hexagons.RemoveAll(h => h);
        if (jip != null)
        {
            Delete(jip);
        }
    }

    public void DeleteAllMappings()
    {
        if (populateMappings != null)
        {
            StopCoroutine(populateMappings);
        }
        foreach (Mapping m in mappings)
        {
            Delete(m);
        }
        mappings.RemoveAll(m => m);
    }

    public void DeleteAllCommas()
    {
        foreach (Comma c in commas)
        {
            Delete(c);
        }
        commas.RemoveAll(c => c);
    }

    public void Delete(Mapping m)
    {
        //mappings.Remove(m);
        Destroy(m.gameObject);
    }

    public void Delete(Comma c)
    {
        //commas.Remove(c);
        Destroy(c.gameObject);
    }
    public void Delete(DamageHexagon h)
    {
        Destroy(h.gameObject);
    }
    public void Remove(Mapping m)
    {
        mappings.Remove(m);
    }
    public void Remove(Comma c)
    {
        commas.Remove(c);
    }
    public void Remove(DamageHexagon h)
    {
        hexagons.Remove(h);
    }

    public List<Mapping> GetAllMappings()
    {
        return mappings;
    }

    public List<string> GetAllMappingNames()
    {
        List<string> result = new List<string>();
        mappings.ForEach(m => result.Add(m.ToString()));
        return result;
    }

    public List<Comma> GetAllCommas()
    {
        return commas;
    }

    public List<string> GetAllCommaNames()
    {
        List<string> result = new List<string>();
        commas.ForEach(c => result.Add(c.ToString()));
        return result;
    }

    /// <summary>
    /// Create mappings up to max_val-et
    /// </summary>
    IEnumerator PopulateMappings()
    {
        for (int i = 0; i < max_val; i++)
        {
            //first get the patent val
            Val patentVal = GetPatentVal(i, primes);
            for (int j = (int)(patentVal.Y - max_offset >= 0 ? patentVal.Y - max_offset : 0); j <= patentVal.Y + max_offset; j++)
            {
                for (int k = (int)(patentVal.Z - max_offset >= 0 ? patentVal.Z - max_offset : 0); k <= patentVal.Z + max_offset; k++)
                {
                    if (!(i == 0 && j == 0 && k == 0)) // avoid < 0 0 0 ]
                    {
                        Mapping m = MakeMapping(i, j, k);
                    }
                    yield return null;
                }
            }
        }
    }

    public Mapping MakeMapping(float x, float y, float z)
    {

        Mapping m = Instantiate(mapping_prefab);
        m.Initialize(x, y, z);
        m.transform.parent = this.transform;
        if (mappings.Where(a => m.Equals(a)).Count() == 0)
        {
            mappings.Add(m);
            return m;
        }
        else
        {
            Mapping result = mappings.Where(a => m.Equals(a)).FirstOrDefault();
            Delete(m);
            return result;
        }
    }

    public Mapping MakeTOPMapping(float x, float y, float z, string name)
    {
        Mapping m = Instantiate(mapping_prefab);
        m.MakeTOPTuning(x, y, z, name);
        m.transform.parent = this.transform;
        if (mappings.Where(a => m.Equals(a)).Count() == 0)
        {
            mappings.Add(m);
            return m;
        }
        else
        {
            Mapping result = mappings.Where(a => m.Equals(a)).FirstOrDefault();
            Delete(m);
            return result;
        }
    }

    public Mapping MakeMapping(Comma c1, Comma c2)
    {
        Mapping m = Instantiate(mapping_prefab);
        m.Initialize(c1, c2);
        m.transform.parent = this.transform;
        if (mappings.Where(a => m.Equals(a)).Count() == 0)
        {
            mappings.Add(m);
            return m;
        }
        else
        {
            Mapping result = mappings.Where(a => m.Equals(a)).FirstOrDefault();
            Delete(m);
            return result;
        }
    }

    public Mapping MakeJIP()
    {
        Mapping jip = Instantiate(mapping_prefab);
        jip.MakeJIP();
        jip.transform.parent = this.transform;
        this.jip = jip;
        return jip;
    }

    public Mapping JIP()
    {
        return jip;
    }

    public Comma MakeComma(float x, float y, float z)
    {
        Comma c = MakeComma(x, y, z, NamedTemperaments.WhatTemperament(primes, new Monzo(x, y, z)));
        return c;
    }

    public Comma MakeComma(float x, float y, float z, string name)
    {
        Comma c = Instantiate(comma_prefab);
        if (string.IsNullOrEmpty(name))
            c.Initialize(x, y, z, "", primes);
        else
            c.Initialize(x, y, z, name, primes);
        c.transform.parent = this.transform;
        if (commas.Where(a => a.Equals(c)).Count() == 0)
        {
            commas.Add(c);
            return c;
        }
        else
        {
            Comma result = commas.Where(a => a.Equals(c)).FirstOrDefault();
            Delete(c);
            return result;
        }

    }

    public Comma MakeComma(Mapping m1, Mapping m2)
    {
        Comma c = Instantiate(comma_prefab);
        c.Initialize(m1, m2, primes);
        c.transform.parent = this.transform;
        if (commas.Where(a => a.Equals(c)).Count() == 0)
        {
            commas.Add(c);
            return c;
        }
        else
        {
            Comma result = commas.Where(a => a.Equals(c)).FirstOrDefault();
            Delete(c);
            return result;
        }
    }

    public DamageHexagon MakeDamageHexagon(float d)
    {
        DamageHexagon h = Instantiate(hexagon_prefab);
        h.Initialize(d);
        return h;
    }

    public List<DamageHexagon> MakeDamageHexagons(int num)
    {
        for (int i = 1; i <= num; i++)
        {
            DamageHexagon hex = MakeDamageHexagon(i);
            if (!hexagons.Contains(hex))
                hexagons.Add(hex);
        }
        return hexagons;
    }

    public void SelectObject(PTSObject selected)
    {
        SelectedObject = selected;
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////                XEN MATH STUFF STARTS HERE              ////////////////////////
/////////////                [  moved to XenMath.cs  ]               ////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/*
/// <summary>
/// Gets the patent val for num-et
/// </summary>
/// <param name="num">The number of steps to primes.x</param>
/// <returns>Patent val for all primes</returns>
public Vector3 GetPatentVal(float num, float? b = null)
{
    if (b == null)
        b = primes.x;
    int x = Mathf.RoundToInt(num * Mathf.Log(primes.x, (float)b));
    int y = Mathf.RoundToInt(num * Mathf.Log(primes.y, (float)b));
    int z = Mathf.RoundToInt(num * Mathf.Log(primes.z, (float)b));
    return new Vector3(x, y, z);
}

public bool IsPatentVal(Vector3 inVal)
{
    Vector3 pat = GetPatentVal(inVal.x);
    if (inVal.x == pat.x && inVal.y == pat.y && inVal.z == pat.z)
        return true;
    else
        return false;
}

public Vector3 GetOffsetFromPatentVal(Vector3 inVal)
{
    Vector3 offset = new Vector3();
    if (IsPatentVal(inVal))
    {
        offset.Set(0, 0, 0);
    }
    else
    {
        Vector3 pat = GetPatentVal(inVal.x);
        for (int i = 0; i < 3; i++)
        {
            if (pat[i] == inVal[i])
            {
                //val is patent
                offset[i] = 0;
            }
            else if (getEtCents(pat[i], pat.x) < getCents(primes[i], 1))
            {
                //patent val is flat
                //therefore 1st offset is above patent val, 2nd is below, 3rd above, 4th below etc
                int off = ((int)(inVal[i] - pat[i]));
                if (off < 0)
                    off = off * -2;
                else
                    off = off * 2 - 1;
                offset[i] = off;
            }
            else
            {
                //patent val is sharp
                //therefore 1st offset is below patent val, 2nd is above, 3rd below, 4th above etc
                int off = ((int)(inVal[i] - pat[i]));
                if (off < 0)
                    off = off * -2 - 1;
                else
                    off = off * 2;
                offset[i] = off;
            }
        }
    }
    return offset;
}

/// <summary>
/// Gets weighted vals for a give set of vals
/// </summary>
/// <param name="vals">A set of vals</param>
/// <returns>Weighted vals according to the primes</returns>
public Vector3 GetWeightedVals(Vector3 vals)
{
    return new Vector3(vals.x / Mathf.Log(primes.x, primes.x), vals.y / Mathf.Log(primes.y, primes.x), vals.z / Mathf.Log(primes.z, primes.x));
}

public Vector3 GetMonzosFromRatio(float n, float d)
{
    if ((n % primes.x == 0 || n % primes.y == 0 || n % primes.z == 0)
     && (d % primes.x == 0 || d % primes.y == 0 || d % primes.z == 0))
    {
        //valid numbers
        Vector3 monzos = new Vector3(GetPrimeCount(n, primes.x), GetPrimeCount(n, primes.y), GetPrimeCount(n, primes.z))
                       - new Vector3(GetPrimeCount(d, primes.x), GetPrimeCount(d, primes.y), GetPrimeCount(d, primes.z));
        return monzos;
    }
    else
        throw new System.FormatException($"Invalid ratio! {n}/{d} is not within the prime basis {primes.x}.{primes.y}.{primes.z}.");
}

public int GetPrimeCount(float num, float prime)
{
    int count = 0;
    while (num % prime == 0)
    {
        num /= prime;
        count++;
    }
    return count;
}

/// <summary>
/// Get cents for a given ratio n/d
/// </summary>
/// <param name="n">Numerator</param>
/// <param name="d">Denominator</param>
/// <returns>Cent value for n/d</returns>
public float getCents(float n, float d = 1)
{
    if (d > 0 && n > d)
    {
        return 1200 * (Mathf.Log(n / d) / Mathf.Log(2));
    }
    else if (n > 0 && n < d)
    {
        return getCents(d, n);
    }
    else
    {
        return Mathf.NegativeInfinity; //error
    }
}

public double getCents(System.Numerics.BigInteger n, System.Numerics.BigInteger d)
{
    if (d > 0 && n > d)
    {
        return 1200 * (System.Numerics.BigInteger.Log(n / d) / System.Numerics.BigInteger.Log(2));
    }
    else if (n > 0 && n < d)
    {
        return getCents(d, n);
    }
    else
    {
        return Mathf.NegativeInfinity; //error
    }
}

/// <summary>
/// Gets cents for a number of steps in a given et
/// </summary>
/// <param name="s">Number of steps per primes.x</param>
/// <param name="e">Equal temperament</param>
/// <returns></returns>
public float getEtCents(float s, float e)
{
    if (e > 0)
    {
        return 1200 * s / e;
    }
    else
    {
        return Mathf.NegativeInfinity; //error
    }
}

/// <summary>
/// Gets Tenney-Optimal value in cents for prime p when comma n/d is tempered out
/// </summary>
/// <param name="n"></param>
/// <param name="d"></param>
/// <param name="p"></param>
/// <returns></returns>
public float getTenneyOptimalTuning(System.Numerics.BigInteger n, System.Numerics.BigInteger d, float pr)
{
    System.Numerics.BigInteger p = (System.Numerics.BigInteger)pr;
    if (n % p != 0 && d % p != 0) //prime does not occur in comma
    {
        return 0;
    }
    float top = getCents((float)n, (float)d) * (float)(System.Numerics.BigInteger.Log10(p) / System.Numerics.BigInteger.Log10(n * d));
    if (n % p == 0) top *= -1;
    return top;
}

//for Rank-2
public Vector3 getTenneyOptimalTuning(System.Numerics.BigInteger n, System.Numerics.BigInteger d)
{
    float top_x = getTenneyOptimalTuning(n, d, primes.x) + getCents(primes.x, 1);
    float top_y = getTenneyOptimalTuning(n, d, primes.y) + getCents(primes.y, 1);
    float top_z = getTenneyOptimalTuning(n, d, primes.z) + getCents(primes.z, 1);
    return new Vector3(top_x, top_y, top_z);
}

//for Rank-1
public Vector3 getTenneyOptimalTuning(Vector3 mapping)
{
    Vector3 weighted = GetWeightedVals(mapping);
    float avg = (weighted.x + weighted.y + weighted.z) / 3;
    float offset = mapping.x / avg;
    Vector3 result = mapping * offset;
    return result;
}

/// <summary>
/// Gets tenney optimal damage in cents when comma n/d is tempered out
/// </summary>
/// <param name="n">Numerator of tempered comma</param>
/// <param name="d">Denominator of tempered comma</param>
/// <returns>Tenney-Optimal damage in cents when comma n/d is tempered out</returns>
public double getTenneyOptimalDamage(System.Numerics.BigInteger n, System.Numerics.BigInteger d)
{
    return 1200 * (System.Numerics.BigInteger.Log10(n / d) / System.Numerics.BigInteger.Log10(n * d));
}

/// <summary>
/// Get number of et-steps to traverse Comma c in Mapping m
/// </summary>
/// <param name="m">The mapping</param>
/// <param name="c">The comma being measured</param>
/// <returns>Number of steps to traverse Comma c in Mapping m</returns>
public float getSteps(Mapping m, Comma c)
{
    return m.vals.x * c.monzos.x + m.vals.y * c.monzos.y + m.vals.z * c.monzos.z;
}

public float getSteps(Vector3 m, Vector3 c)
{
    return m[0] * c[0] + m[1] * c[1] + m[2] * c[2];
}

public int gcd(int a, int b)
{
    while (b != 0)
    {
        int r = a % b;
        a = b;
        b = r;
        //Debug.Log(a);
    }
    //Debug.Log(a + " " + b);
    return a;
}

// Recursive function to demonstrate the extended Euclidean algorithm.
// It returns multiple values using Tuple in C++.
public Tuple<int, int, int> extendedGcd(int a, int b)
{
    if (a == 0)
    {
        return new Tuple<int,int,int>(b, 0, 1);
    }

    int g, x, y;

    // unpack Tuple returned by function into variables
    Tuple<int,int,int> t = extendedGcd(b % a, a);
    g = t.Item1;
    x = t.Item2;
    y = t.Item3;

    return new Tuple<int,int,int>(g, (y - (b / a) * x), x);
}

public Vector3 AntiNullSpaceBasis(Vector3 v1, Vector3 v2)
{
    Vector3 result = Vector3.zero;
    int tries = 0;
    float a = v1.x;
    float b = v1.y;
    float c = v1.z;
    float d = v2.x;
    float e = v2.y;
    float f = v2.z;
    do
    {
        tries++;
        float bd_ae = b * d - a * e;
        float cd_af = c * d - a * f;
        result = new Vector3(-f * bd_ae + e * cd_af, -d * cd_af, d * bd_ae);
        if (result == Vector3.zero)
        {
            switch (tries)
            {
                case 1:
                    a = v1.y;
                    b = v1.z;
                    c = v1.x;
                    d = v2.y;
                    e = v2.z;
                    f = v2.x;
                    break;
                case 2:
                    a = v1.z;
                    b = v1.x;
                    c = v1.y;
                    d = v2.z;
                    e = v2.x;
                    f = v2.y;
                    break;
                default:
                    break;
            }
        }
    } while (result == Vector3.zero && tries < 3);
    float g = gcd(gcd((int)result.x, (int)result.y), (int)result.z);
    result /= g;
    return result;
}

public int GetNumberOfPrime(int p)
{
    return System.Array.IndexOf(prime_numbers, p);
}

public float GetComplexity(Vector3 monzos)
{
    //is this the correct method?
    return Mathf.Log10(Mathf.Pow(primes.x, Mathf.Abs(monzos.x)) * Mathf.Pow(primes.y, Mathf.Abs(monzos.y)) * Mathf.Pow(primes.z, Mathf.Abs(monzos.z)));
}

public double GetComplexity(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
{
    //is this the correct method?
    return System.Numerics.BigInteger.Log10(numerator * denominator);
}

//private void OnSelectedObjectChangeHandler(PTSObject obj, bool select)
//{
//    //if (obj.GetType() == typeof(Mapping))
//    //    ui.UpdatePTSObjectInfo((Mapping)obj);
//    //else if (obj.GetType() == typeof(Comma))
//    //    ui.UpdatePTSObjectInfo((Comma)obj);
//    if (select)
//        obj.OnSelect();
//    else
//        obj.OnDeselect();
//}
}
*/