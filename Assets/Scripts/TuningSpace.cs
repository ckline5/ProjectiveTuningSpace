using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static XenObjects;
using static XenMath;

public class TuningSpace : MonoBehaviour
{
    private static TuningSpace _instance;
    public static TuningSpace Instance { get { return _instance; } }

    [TextArea(3,10)]
    public string introductionMessage;

    //private UIHandler UIHandler.Instance;
    //FPSFlyer FPSFlyer.Instance;

    public PrimeBasis primes;
    public float min_val = 0;
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
    public Comma Comma_prefab;
    public DamageHexagon hexagon_prefab;
    public MOS mos_prefab;
    
    public MOS mos;
    public MOS miniMos;
    public MeshRenderer miniMosMesh;

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

    public bool CenterNewTemperament { get; set; }

    Mapping jip;
    List<Mapping> mappings = new List<Mapping>();
    List<Comma> Commas = new List<Comma>();
    List<DamageHexagon> hexagons = new List<DamageHexagon>();

    public int MappingsCount { get { return mappings.Count; } }

    public enum JoinMode
    {
        NONE,
        MAPPING,
        COMMA,
        MAPPING_MERGE_PLUS,
        MAPPING_MERGE_MINUS,
        COMMA_MERGE_PLUS,
        COMMA_MERGE_MINUS
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

            var oldValue = selectedObject;
            selectedObject = value;
            if (oldValue != null)
                oldValue.OnDeselect();
            if (selectedObject != null)
                selectedObject.OnSelect();
            else
                UIHandler.Instance.HidePTSObjectInfo();
            //OnSelectedObjectChange?.Invoke(oldValue, false);
            //OnSelectedObjectChange?.Invoke(selectedObject, true);
            if (oldValue != null && selectedObject != null && oldValue.GetType() == SelectedObject.GetType())
            {
                switch (joinMode)
                {
                    case JoinMode.MAPPING:
                        Comma c = MakeComma((Mapping)oldValue, (Mapping)SelectedObject);
                        joinMode = JoinMode.NONE;
                        if (CenterNewTemperament)
                            FPSFlyer.Instance.SetPosition(c.transform.position);
                        SelectedObject = c;
                        break;
                    case JoinMode.COMMA:
                        Mapping m = MakeMapping((Comma)oldValue, (Comma)SelectedObject);
                        joinMode = JoinMode.NONE;
                        if (CenterNewTemperament)
                            FPSFlyer.Instance.SetPosition(m.transform.position + new Vector3(0, 0, -1));
                        SelectedObject = m;
                        break;
                    case JoinMode.MAPPING_MERGE_PLUS:
                        Val merged_mp = ((Mapping)oldValue).vals + ((Mapping)SelectedObject).vals;
                        Mapping mp = MakeMapping(merged_mp.X, merged_mp.Y, merged_mp.Z);
                        joinMode = JoinMode.NONE;
                        if (CenterNewTemperament)
                            FPSFlyer.Instance.SetPosition(mp.transform.position + new Vector3(0, 0, -1));
                        SelectedObject = mp;
                        break;
                    case JoinMode.MAPPING_MERGE_MINUS:
                        Val merged_mm = ((Mapping)oldValue).vals - ((Mapping)SelectedObject).vals;
                        if (merged_mm.X < 0)
                            merged_mm *= -1;
                        Mapping mm = MakeMapping(merged_mm.X, merged_mm.Y, merged_mm.Z);
                        joinMode = JoinMode.NONE;
                        if (CenterNewTemperament)
                            FPSFlyer.Instance.SetPosition(mm.transform.position + new Vector3(0, 0, -1));
                        SelectedObject = mm;
                        break;
                    case JoinMode.COMMA_MERGE_PLUS:
                        Monzo merged_cp = ((Comma)oldValue).TemperedInterval.Monzos + ((Comma)SelectedObject).TemperedInterval.Monzos;
                        Comma cp = MakeComma(merged_cp.X, merged_cp.Y, merged_cp.Z);
                        joinMode = JoinMode.NONE;
                        if (CenterNewTemperament)
                            FPSFlyer.Instance.SetPosition(cp.transform.position);
                        SelectedObject = cp;
                        break;
                    case JoinMode.COMMA_MERGE_MINUS:
                        Monzo merged_cm = ((Comma)oldValue).TemperedInterval.Monzos - ((Comma)SelectedObject).TemperedInterval.Monzos;
                        Comma cm = MakeComma(merged_cm.X, merged_cm.Y, merged_cm.Z);
                        joinMode = JoinMode.NONE;
                        if (CenterNewTemperament)
                            FPSFlyer.Instance.SetPosition(cm.transform.position);
                        SelectedObject = cm;
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
        FULL_MONZOS,
        BLANK
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
    Coroutine populateMappings2;
    Coroutine populateMappings3;
    Coroutine populateMappings4;

    private void Awake()
    {
        //UIHandler.Instance = GameObject.Find("UI").GetComponent<UIHandler>();
        //FPSFlyer.Instance = GameObject.Find("FPSFlyer.Instance").GetComponent<FPSFlyer>();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        CenterNewTemperament = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIHandler.Instance.ShowInfo(introductionMessage);
        miniMosMesh.gameObject.SetActive(false);

        //OnSelectedObjectChange += OnSelectedObjectChangeHandler;
        //StartCoroutine(ProceduralPTS(5, 7, .01f));
    }

    IEnumerator ProceduralPTS(float from, float to, float by)
    {
        MakeJIP();
        MakeDamageHexagons(10);
        UIHandler.Instance.ShowHideMenu();
        yield return new WaitForSeconds(3f);
        int count = 0;
        for (float z = from; z <= to + by / 2; z += by)
        {
            primes = new PrimeBasis(2, 3, z);
            for (int i = (int)min_val; i <= max_val; i++)
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
            foreach (Comma c in Commas)
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
        MakeBoundaryMappingsAndCommas();
        populateMappings = StartCoroutine(PopulateMappings());
    }

    /// <summary>
    /// draw default 5-limit Commas from Paul's paper
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
            StartCoroutine(Delete(h));
        }
        hexagons.RemoveAll(h => h);
        if (jip != null)
        {
            StartCoroutine(Delete(jip));
        }
    }

    public void DeleteAllMappings()
    {
        if (populateMappings != null)
        {
            StopCoroutine(populateMappings);
            StopCoroutine(populateMappings2);
            StopCoroutine(populateMappings3);
            StopCoroutine(populateMappings4);
        }
        foreach (Mapping m in mappings)
        {
            StartCoroutine(Delete(m));
        }
        mappings.RemoveAll(m => m);
    }

    public void DeleteAllCommas()
    {
        foreach (Comma c in Commas)
        {
            StartCoroutine(Delete(c));
        }
        Commas.RemoveAll(c => c);
    }

    public IEnumerator Delete(Mapping m)
    {
        //mappings.Remove(m);
        Destroy(m.gameObject);
        yield return null;
    }

    public IEnumerator Delete(Comma c)
    {
        //Commas.Remove(c);
        Destroy(c.gameObject);
        yield return null;
    }
    public IEnumerator Delete(DamageHexagon h)
    {
        Destroy(h.gameObject);
        yield return null;
    }
    public void Remove(Mapping m)
    {
        mappings.Remove(m);
    }
    public void Remove(Comma c)
    {
        Commas.Remove(c);
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
        return Commas;
    }

    public List<string> GetAllCommaNames()
    {
        List<string> result = new List<string>();
        Commas.ForEach(c => result.Add(c.ToString()));
        return result;
    }

    ///// <summary>
    ///// Create mappings up to max_val-et
    ///// </summary>
    //IEnumerator PopulateMappings()
    //{
    //    for (int i = (int)min_val; i <= max_val; i++)
    //    {
    //        //first get the patent val
    //        Val patentVal = GetPatentVal(i, primes);
    //        for (int j = (int)(patentVal.Y - max_offset >= 0 ? patentVal.Y - max_offset : 0); j <= patentVal.Y + max_offset; j++)
    //        {
    //            for (int k = (int)(patentVal.Z - max_offset >= 0 ? patentVal.Z - max_offset : 0); k <= patentVal.Z + max_offset; k++)
    //            {
    //                if (!(i == 0 && j == 0 && k == 0)) // avoid < 0 0 0 ]
    //                {
    //                    Mapping m = MakeMapping(i, j, k);
    //                }
    //                yield return null;
    //            }
    //        }
    //    }
    //}
    
    /// <summary>
    /// Create mappings up to max_val-et
    /// </summary>
    IEnumerator PopulateMappings()
    {
        for (int i = (int)min_val; i <= max_val; i++)
        {
            //first get the patent val
            Val patentVal = GetPatentVal(i, primes);
            populateMappings2 = StartCoroutine(PopulateMappings2(patentVal, i));
            yield return null;
        }
    }

    IEnumerator PopulateMappings2(Val patentVal, float i)
    {
        for (int j = (int)(patentVal.Y - max_offset >= 0 ? patentVal.Y - max_offset : 0); j <= patentVal.Y + max_offset; j++)
        {
            populateMappings3 = StartCoroutine(PopulateMappings3(patentVal, i, j));
            yield return null;
        }
    }

    IEnumerator PopulateMappings3(Val patentVal, float i, float j)
    {
        for (int k = (int)(patentVal.Z - max_offset >= 0 ? patentVal.Z - max_offset : 0); k <= patentVal.Z + max_offset; k++)
        {
            populateMappings4 = StartCoroutine(PopulateMappings4(patentVal, i, j, k));
            yield return null;
        }
    }

    IEnumerator PopulateMappings4(Val patentVal, float i, float j, float k)
    {
        if (!(i == 0 && j == 0 && k == 0)) // avoid < 0 0 0 ]
        {
            Mapping m = MakeMapping(i, j, k);
        }
        yield return null;
    }

    public Mapping MakeMapping(float x, float y, float z)
    {

        Mapping m = Instantiate(mapping_prefab);
        m.Initialize(x, y, z);
        m.transform.parent = this.transform;
        if (mappings.Where(a => m.vals == a.vals).Count() == 0)
        {
            mappings.Add(m);
            return m;
        }
        else
        {
            Mapping result = mappings.Where(a => m.vals == a.vals).FirstOrDefault();
            StartCoroutine(Delete(m));
            return result;
        }
    }

    public Mapping MakeTOPMapping(float x, float y, float z, string name)
    {
        Mapping m = Instantiate(mapping_prefab);
        m.MakeTOPTuning(x, y, z, name);
        m.transform.parent = this.transform;
        if (mappings.Where(a => m.vals == a.vals).Count() == 0)
        {
            mappings.Add(m);
            return m;
        }
        else
        {
            Mapping result = mappings.Where(a => m.vals == a.vals).FirstOrDefault();
            StartCoroutine(Delete(m));
            return result;
        }
    }

    public Mapping MakeMapping(Comma c1, Comma c2)
    {
        Mapping m = Instantiate(mapping_prefab);
        m.Initialize(c1, c2);
        m.transform.parent = this.transform;
        if (mappings.Where(a => m.vals == a.vals).Count() == 0)
        {
            mappings.Add(m);
            return m;
        }
        else
        {
            Mapping result = mappings.Where(a => m.vals == a.vals).FirstOrDefault();
            StartCoroutine(Delete(m));
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
        Comma c = Instantiate(Comma_prefab);
        if (string.IsNullOrEmpty(name))
            c.Initialize(x, y, z, "", primes);
        else
            c.Initialize(x, y, z, name, primes);
        c.transform.parent = this.transform;
        if (Commas.Where(a => c.TemperedInterval.Monzos == a.TemperedInterval.Monzos).Count() == 0)
        {
            Commas.Add(c);
            return c;
        }
        else
        {
            Comma result = Commas.Where(a => c.TemperedInterval.Monzos == a.TemperedInterval.Monzos).FirstOrDefault();
            StartCoroutine(Delete(c));
            return result;
        }

    }

    public Comma MakeComma(Mapping m1, Mapping m2)
    {
        Comma c = Instantiate(Comma_prefab);
        c.Initialize(m1, m2, primes);
        c.transform.parent = this.transform;
        if (Commas.Where(a => c.TemperedInterval.Monzos == a.TemperedInterval.Monzos).Count() == 0)
        {
            Commas.Add(c);
            return c;
        }
        else
        {
            Comma result = Commas.Where(a => c.TemperedInterval.Monzos == a.TemperedInterval.Monzos).FirstOrDefault();
            StartCoroutine(Delete(c));
            return result;
        }
    }

    public DamageHexagon MakeDamageHexagon(float d)
    {
        DamageHexagon h = Instantiate(hexagon_prefab);
        h.Initialize(d);
        h.transform.parent = this.transform;
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

    public void MakeBoundaryMappingsAndCommas()
    {
        MakeMapping(1, 0, 0);
        MakeMapping(0, 1, 0);
        MakeMapping(0, 0, 1);
        MakeComma(1, 0, 0);
        MakeComma(0, 1, 0);
        MakeComma(0, 0, 1);
    }

    public void SelectObject(PTSObject selected)
    {
        SelectedObject = selected;
    }

    public void ViewMainMos(decimal generator, decimal period, decimal equivalenceInterval)
    {
        mos.Initialize(mos.transform.position, generator, period, equivalenceInterval);
        UIHandler.Instance.UpdateMOSInfo(period.ToString(), generator.ToString(), equivalenceInterval.ToString());
    }

    public void ViewMiniMos(decimal generator, decimal period, decimal equivalenceInterval)
    {
        miniMos.Initialize(miniMos.transform.position, generator, period, equivalenceInterval);
        UIHandler.Instance.UpdateMOSInfo(period.ToString(), generator.ToString(), equivalenceInterval.ToString());
    }

    public void ViewMainMos(PTSObject x)
    {
        if (x is Mapping)
        {
            mos.Initialize(mos.transform.position, 0, ((Mapping)x).stepSize, XenMath.getCents(primes.X));
            UIHandler.Instance.UpdateMOSInfo((x as Mapping).stepSize.ToString(), "0", XenMath.getCents(primes.X).ToString());
        }
        else if (x is Comma)
        {
            mos.Initialize(mos.transform.position, ((Comma)x).generator.Cents, ((Comma)x).period.Cents, XenMath.getCents(primes.X));
            UIHandler.Instance.UpdateMOSInfo((x as Comma).period.Cents.ToString(), (x as Comma).generator.Cents.ToString(), XenMath.getCents(primes.X).ToString());
        }
        UIHandler.Instance.ShowMOSMenu();
        UIHandler.Instance.ShowMOSOptions();
    }

    public void ViewMiniMos(PTSObject x)
    {
        miniMosMesh.gameObject.SetActive(true);
        if (x is Mapping)
        {
            miniMos.Initialize(miniMos.transform.parent.position, 0, ((Mapping)x).stepSize, XenMath.getCents(primes.X));
            miniMos.transform.localPosition -= new Vector3(0, 2, 0);
            UIHandler.Instance.UpdateMOSInfo((x as Mapping).stepSize.ToString(), "N/A", XenMath.getCents(primes.X).ToString());
        }
        else if (x is Comma)
        {
            miniMos.Initialize(miniMos.transform.parent.position, ((Comma)x).generator.Cents, ((Comma)x).period.Cents, XenMath.getCents(primes.X));
            Debug.Log(((Comma)x).generator.Numerator + "/" + ((Comma)x).generator.Denominator + " \\ " + ((Comma)x).generator.Divisions + ": " + ((Comma)x).generator.Cents + "; " + ((Comma)x).period.Numerator + "/" + ((Comma)x).period.Denominator + " \\ " + ((Comma)x).period.Divisions + ": " + ((Comma)x).period.Cents);
            miniMos.transform.localPosition -= new Vector3(0, 2, 0);
            UIHandler.Instance.UpdateMOSInfo((x as Comma).period.Cents.ToString(), (x as Comma).generator.Cents.ToString(), XenMath.getCents(primes.X).ToString());
        }
        UIHandler.Instance.ShowMOSMenu();
        UIHandler.Instance.ShowMiniMOSOptions();
    }
}