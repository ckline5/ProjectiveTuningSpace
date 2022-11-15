using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static XenObjects;
using static XenMath;
using System.Linq;

public class UIHandler : MonoBehaviour
{
    private static UIHandler _instance;
    public static UIHandler Instance { get { return _instance; } }

    //TuningSpace TuningSpace.Instance;
    //FPSFlyer FPSFlyer.Instance;

    public bool mouseInUI;

    VisualElement root;

    VisualElement m_PrimeBasis;
    VisualElement m_PTSObjectCreator;
    VisualElement m_Options;
    VisualElement m_PTSObjectInfo;
    VisualElement m_MappingInfo;
    VisualElement m_CommaInfo;
    VisualElement m_ErrorsWindow;
    VisualElement m_InfoWindow;
    VisualElement m_QuestionWindow;
    VisualElement m_QuestionResponses;
    VisualElement m_MOSInfo;
    VisualElement m_MiniMOSOptions;
    VisualElement m_MOSOptions;
    VisualElement m_SoundOptions;


    enum CreateMappingType
    {
        VALS,
        ED,
        CENTS
    }
    CreateMappingType createMappingType = CreateMappingType.VALS;

    enum CreateCommaType
    {
        MONZOS,
        RATIO,
        NAME
    }
    CreateCommaType createCommaType = CreateCommaType.MONZOS;

    public enum QuestionType
    {
        NONE,
        YESNO,
        OPTIMIZATION,
        MOSSIZE
    }
    QuestionType questionType = QuestionType.NONE;
    private string _questionResponse;
    public string QuestionResponse {
        get { return _questionResponse; }
        set
        {
            if (value == _questionResponse) return;
            _questionResponse = value;
            if (!string.IsNullOrWhiteSpace(_questionResponse))
            {
                switch (questionType)
                {
                    case QuestionType.NONE:
                        _questionResponse = "";
                        break;
                    case QuestionType.YESNO:
                        break;
                    case QuestionType.OPTIMIZATION:
                        break;
                    case QuestionType.MOSSIZE:
                        break;
                }
            }
        }
    }



    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        //TuningSpace.Instance = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        //FPSFlyer.Instance = GameObject.Find("FPSFlyer.Instance").GetComponent<FPSFlyer>();
    }

    private void SetupUIElements()
    {
        //setup root
        root = GetComponent<UIDocument>().rootVisualElement;
        screenshotCamera.enabled = false;

        //setup visualelements
        m_PrimeBasis = root.Q<VisualElement>("PrimeBasis");
        m_PTSObjectCreator = root.Q<VisualElement>("PTSObjectCreator");
        m_Options = root.Q<VisualElement>("Options");
        m_PTSObjectInfo = root.Q<VisualElement>("PTSObjectInfo");
        m_MappingInfo = m_PTSObjectInfo.Q<VisualElement>("MappingInfo");
        m_CommaInfo = m_PTSObjectInfo.Q<VisualElement>("CommaInfo");
        m_ErrorsWindow = root.Q<VisualElement>("ErrorsWindow");
        m_ErrorsWindow.style.display = DisplayStyle.None;
        m_InfoWindow = root.Q<VisualElement>("InfoWindow");
        m_InfoWindow.style.display = DisplayStyle.None;
        m_QuestionWindow = root.Q<VisualElement>("QuestionWindow");
        m_QuestionWindow.style.display = DisplayStyle.None;
        m_QuestionResponses = m_QuestionWindow.Q<VisualElement>("QuestionResponses");
        m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector").style.display = DisplayStyle.None;
        m_MOSInfo = root.Q<VisualElement>("MOSInfo");
        m_MOSInfo.style.display = DisplayStyle.None;
        m_MiniMOSOptions = m_MOSInfo.Q<VisualElement>("MiniMOSOptions");
        m_MOSOptions = m_MOSInfo.Q<VisualElement>("MOSOptions");
        m_SoundOptions = root.Q<VisualElement>("SoundOptions");
        m_SoundOptions.style.display = DisplayStyle.None;

        //setup buttons
        m_PrimeBasis.Q<Button>("PrimeBasisInit").clicked += PrimeBasisInit_clicked;
        m_PrimeBasis.Q<Button>("PrimeBasisCreate").clicked += PrimeBasisCreate_clicked;
        m_PrimeBasis.Q<Button>("PrimeBasisDestroyAll").clicked += PrimeBasisDestroyAll_clicked;
        m_PTSObjectCreator.Q<Button>("DestroyMappings").clicked += DestroyMappings_clicked;
        m_PTSObjectCreator.Q<Button>("DestroyCommas").clicked += DestroyCommas_clicked;
        m_PTSObjectCreator.Q<Button>("DestroyMappingsAndCommas").clicked += DestroyMappingsAndCommas_clicked;
        m_PTSObjectCreator.Q<Button>("CreateMappingButton").clicked += CreateMappingButton_clicked;
        m_PTSObjectCreator.Q<Button>("CreateCommaButton").clicked += CreateCommaButton_clicked;
        m_Options.Q<Button>("ScreenshotButton").clicked += ScreenshotButton_clicked;
        m_Options.Q<Button>("ResetPositionButton").clicked += ResetPositionButton_clicked;
        m_Options.Q<Button>("CreditsButton").clicked += CreditsButton_clicked;
        m_MappingInfo.Q<Button>("MappingJoin").clicked += MappingJoin_clicked;
        m_MappingInfo.Q<Button>("MappingMergePlus").clicked += MappingMergePlus_clicked;
        m_MappingInfo.Q<Button>("MappingMergeMinus").clicked += MappingMergeMinus_clicked;
        m_MappingInfo.Q<Button>("MappingSetPosition").clicked += MappingSetPosition_clicked;
        m_MappingInfo.Q<Button>("MappingViewHoragram").clicked += MappingViewHoragram_clicked;
        m_MappingInfo.Q<Button>("MappingDelete").clicked += MappingDelete_clicked;
        m_CommaInfo.Q<Button>("CommaJoin").clicked += CommaJoin_clicked;
        m_CommaInfo.Q<Button>("CommaMergePlus").clicked += CommaMergePlus_clicked;
        m_CommaInfo.Q<Button>("CommaMergeMinus").clicked += CommaMergeMinus_clicked;
        m_CommaInfo.Q<Button>("CommaSetPosition").clicked += CommaSetPosition_clicked;
        m_CommaInfo.Q<Button>("CommaViewHoragram").clicked += CommaViewHoragram_clicked;
        m_CommaInfo.Q<Button>("CommaDelete").clicked += CommaDelete_clicked;
        m_MappingInfo.Q<Button>("LinkXenWiki").clicked += LinkXenWiki_clicked;
        m_MappingInfo.Q<Button>("LinkX31EQ").clicked += LinkX31EQ_clicked;
        m_MappingInfo.Q<Button>("LinkScaleWorkshop").clicked += LinkScaleWorkshopMapping_clicked;
        m_MappingInfo.Q<Button>("LinkXenCalc").clicked += LinkXenCalc_clicked;
        m_CommaInfo.Q<Button>("LinkXenWiki").clicked += LinkXenWiki_clicked;
        m_CommaInfo.Q<Button>("LinkX31EQ").clicked += LinkX31EQ_clicked;
        m_CommaInfo.Q<Button>("LinkScaleWorkshop").clicked += LinkScaleWorkshopComma_clicked;
        m_CommaInfo.Q<Button>("LinkXenCalc").clicked += LinkXenCalc_clicked;
        m_ErrorsWindow.Q<Button>("ErrorsOK").clicked += ErrorsOK_clicked;
        m_InfoWindow.Q<Button>("InfoOK").clicked += InfoOK_clicked;
        m_QuestionWindow.Q<Button>("QuestionCancel").clicked += QuestionCancel_clicked;
        m_MiniMOSOptions.Q<Button>("MiniMOSHide").clicked += MiniMOSHide_clicked;
        m_MiniMOSOptions.Q<Button>("MiniMOSViewFullMOS").clicked += MiniMOSViewFullMOS_clicked;
        m_MOSOptions.Q<Button>("MOSCreate").clicked += MOSCreate_clicked;
        m_MOSOptions.Q<Button>("MOSBackToPTS").clicked += MOSBackToPTS_clicked;
        m_MOSOptions.Q<Button>("MOSScreenshot").clicked += MOSScreenshot_clicked;
        m_MOSOptions.Q<Button>("MOSResetPosition").clicked += ResetPositionButton_clicked;
        m_MOSInfo.Q<Button>("MOSPeriodPlus").clicked += MOSPeriodPlus_clicked;
        m_MOSInfo.Q<Button>("MOSPeriodMinus").clicked += MOSPeriodMinus_clicked;
        m_MOSInfo.Q<Button>("MOSGeneratorPlus").clicked += MOSGeneratorPlus_clicked;
        m_MOSInfo.Q<Button>("MOSGeneratorMinus").clicked += MOSGeneratorMinus_clicked;
        m_MOSInfo.Q<Button>("MOSEquivalenceIntervalPlus").clicked += MOSEquivalenceIntervalPlus_clicked;
        m_MOSInfo.Q<Button>("MOSEquivalenceIntervalMinus").clicked += MOSEquivalenceIntervalMinus_clicked;
    }

    private void SetupUIElementsReferencingTuningSpace()
    { 
        //setup textfields
        m_PrimeBasis.Q<TextField>("PrimeX").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) PrimeBasisCreate_clicked(); });
        m_PrimeBasis.Q<TextField>("PrimeY").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) PrimeBasisCreate_clicked(); });
        m_PrimeBasis.Q<TextField>("PrimeZ").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) PrimeBasisCreate_clicked(); });
        m_PrimeBasis.Q<TextField>("MinET").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) PrimeBasisCreate_clicked(); });
        m_PrimeBasis.Q<TextField>("MaxET").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) PrimeBasisCreate_clicked(); });
        m_PrimeBasis.Q<TextField>("MaxOffset").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) PrimeBasisCreate_clicked(); });

        m_PTSObjectCreator.Q<TextField>("CreateMappingX").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateMappingButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateMappingY").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateMappingButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateMappingZ").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateMappingButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateMappingED").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateMappingButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateMappingEDOf").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateMappingButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateMappingCents").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateMappingButton_clicked(); });

        m_PTSObjectCreator.Q<TextField>("CreateCommaX").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateCommaButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateCommaY").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateCommaButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateCommaZ").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateCommaButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateCommaNumerator").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateCommaButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateCommaDenominator").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateCommaButton_clicked(); });
        m_PTSObjectCreator.Q<TextField>("CreateCommaName").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) CreateCommaButton_clicked(); });

        m_MOSInfo.Q<TextField>("MOSPeriod").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) MOSCreate_clicked(); });
        m_MOSInfo.Q<TextField>("MOSGenerator").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) MOSCreate_clicked(); });
        m_MOSInfo.Q<TextField>("MOSEquivalenceInterval").RegisterCallback<KeyDownEvent>(e => { if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) MOSCreate_clicked(); });

        //setup dropdowns
        DropdownField m_CreateMappingTypeMenu = m_PTSObjectCreator.Q<DropdownField>("CreateMappingType");
        m_CreateMappingTypeMenu.choices.Clear();
        m_CreateMappingTypeMenu.choices.AddRange(System.Enum.GetNames(typeof(CreateMappingType)));
        m_CreateMappingTypeMenu.RegisterValueChangedCallback(v => CreateMappingTypeMenu_selected(v.newValue));
        m_CreateMappingTypeMenu.value = createMappingType.ToString();
        DropdownField m_CreateCommaTypeMenu = m_PTSObjectCreator.Q<DropdownField>("CreateCommaType");
        m_CreateCommaTypeMenu.choices.Clear();
        m_CreateCommaTypeMenu.choices.AddRange(System.Enum.GetNames(typeof(CreateCommaType)));
        m_CreateCommaTypeMenu.RegisterValueChangedCallback(v => CreateCommaTypeMenu_selected(v.newValue));
        m_CreateCommaTypeMenu.value = createCommaType.ToString();

        DropdownField m_MappingTextStyleMenu = m_Options.Q<DropdownField>("MappingTextStyle");
        m_MappingTextStyleMenu.choices.Clear();
        m_MappingTextStyleMenu.choices.AddRange(System.Enum.GetNames(typeof(TuningSpace.MappingTextStyle)));
        m_MappingTextStyleMenu.RegisterValueChangedCallback(v => MappingTextStyleMenu_selected(v.newValue));
        m_MappingTextStyleMenu.value = TuningSpace.Instance.mappingTextStyle.ToString();
        DropdownField m_CommaTextStyleMenu = m_Options.Q<DropdownField>("CommaTextStyle");
        m_CommaTextStyleMenu.choices.Clear();
        m_CommaTextStyleMenu.choices.AddRange(System.Enum.GetNames(typeof(TuningSpace.CommaTextStyle)));
        m_CommaTextStyleMenu.RegisterValueChangedCallback(v => CommaTextStyleMenu_selected(v.newValue));
        m_CommaTextStyleMenu.value = TuningSpace.Instance.commaTextStyle.ToString();
        DropdownField m_MappingColorStyleMenu = m_Options.Q<DropdownField>("MappingColorStyle");
        m_MappingColorStyleMenu.choices.Clear();
        m_MappingColorStyleMenu.choices.AddRange(System.Enum.GetNames(typeof(TuningSpace.MappingColorStyle)));
        m_MappingColorStyleMenu.RegisterValueChangedCallback(v => MappingColorStyleMenu_selected(v.newValue));
        m_MappingColorStyleMenu.value = TuningSpace.Instance.mappingColorStyle.ToString();
        DropdownField m_RainbowIntervalSelectorMenu = m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector");
        m_RainbowIntervalSelectorMenu.choices.Clear();
        m_RainbowIntervalSelectorMenu.choices.AddRange(TuningSpace.Instance.GetAllCommaNames());
        m_RainbowIntervalSelectorMenu.RegisterCallback<ClickEvent>(RainbowIntervalSelectorMenu_clicked);
        m_RainbowIntervalSelectorMenu.RegisterValueChangedCallback(v => RainbowIntervalSelectorMenu_selected(v.newValue));
        m_RainbowIntervalSelectorMenu.value = "";

        //setup sliders
        Slider m_MetaZoomSlider = m_Options.Q<Slider>("MetaZoomSlider");
        m_MetaZoomSlider.RegisterValueChangedCallback(v => MetaZoomSlider_changed(v.newValue));

        //setup toggles
        Toggle m_CenterNewTemperament = m_PTSObjectCreator.Q<Toggle>("PTSObjectCreaterCenterNewObject");
        m_CenterNewTemperament.RegisterValueChangedCallback(v => TuningSpace.Instance.CenterNewTemperament = v.newValue);

        Toggle m_DisplayEncipheredVals = m_Options.Q<Toggle>("DisplayEncipheredVals");
        m_DisplayEncipheredVals.RegisterValueChangedCallback(v => TuningSpace.Instance.displayEncipheredVals = v.newValue);

    }

    private void CreditsButton_clicked()
    {
        ShowInfo(TuningSpace.Instance.creditsMessage);
    }

    private float GetMOSIncrementBy()
    {
        string inc = m_MOSInfo.Q<TextField>("MOSIncrementBy").value;
        if (inc == "N/A") inc = "0";
        float value = getCents(inc);
        //if (float.TryParse(inc, out value))
        //{
        //    return value;
        //}
        //else
        //{
        //    throw new ArgumentException("Invalid IncrementBy: " + gen);
        //}
        return value;
    }

    private void MOSEquivalenceIntervalMinus_clicked()
    {
        string gen = m_MOSInfo.Q<TextField>("MOSEquivalenceInterval").value;
        if (gen == "N/A") gen = "0";
        float value;
        if (float.TryParse(gen, out value))
        {
            value -= GetMOSIncrementBy();
            m_MOSInfo.Q<TextField>("MOSEquivalenceInterval").value = value.ToString();
            MOSCreate_clicked();
        }
        else
        {
            throw new ArgumentException("Invalid EquivalenceInterval: " + gen);
        }
    }

    private void MOSEquivalenceIntervalPlus_clicked()
    {
        string gen = m_MOSInfo.Q<TextField>("MOSEquivalenceInterval").value;
        if (gen == "N/A") gen = "0";
        float value;
        if (float.TryParse(gen, out value))
        {
            value += GetMOSIncrementBy();
            m_MOSInfo.Q<TextField>("MOSEquivalenceInterval").value = value.ToString();
            MOSCreate_clicked();
        }
        else
        {
            throw new ArgumentException("Invalid EquivalenceInterval: " + gen);
        }
    }

    private void MOSPeriodMinus_clicked()
    {
        string gen = m_MOSInfo.Q<TextField>("MOSPeriod").value;
        if (gen == "N/A") gen = "0";
        float value;
        if (float.TryParse(gen, out value))
        {
            value -= GetMOSIncrementBy();
            m_MOSInfo.Q<TextField>("MOSPeriod").value = value.ToString();
            MOSCreate_clicked();
        }
        else
        {
            throw new ArgumentException("Invalid Period: " + gen);
        }
    }

    private void MOSPeriodPlus_clicked()
    {
        string gen = m_MOSInfo.Q<TextField>("MOSPeriod").value;
        if (gen == "N/A") gen = "0";
        float value;
        if (float.TryParse(gen, out value))
        {
            value += GetMOSIncrementBy();
            m_MOSInfo.Q<TextField>("MOSPeriod").value = value.ToString();
            MOSCreate_clicked();
        }
        else
        {
            throw new ArgumentException("Invalid Period: " + gen);
        }
    }

    private void MOSGeneratorPlus_clicked()
    {
        string gen = m_MOSInfo.Q<TextField>("MOSGenerator").value;
        if (gen == "N/A") gen = "0";
        float value;
        if (float.TryParse(gen, out value))
        {
            value += GetMOSIncrementBy();
            m_MOSInfo.Q<TextField>("MOSGenerator").value = value.ToString();
            MOSCreate_clicked();
        }
        else
        {
            throw new ArgumentException("Invalid generator: " + gen);
        }
    }

    private void MOSGeneratorMinus_clicked()
    {
        string gen = m_MOSInfo.Q<TextField>("MOSGenerator").value;
        if (gen == "N/A") gen = "0";
        float value;
        if (float.TryParse(gen, out value))
        {
            value -= GetMOSIncrementBy();
            m_MOSInfo.Q<TextField>("MOSGenerator").value = value.ToString();
            MOSCreate_clicked();
        }
        else
        {
            throw new ArgumentException("Invalid generator: " + gen);
        }
    }
    
    private void OnEnable()
    {
        SetupUIElements();
        HidePTSObjectInfo();
    }

    private void Start()
    {
        SetupUIElementsReferencingTuningSpace();
        InitializePrimeBasis();
    }
    
    private void CommaMergeMinus_clicked()
    {
        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.COMMA_MERGE_MINUS;
        TuningSpace.Instance.SelectedObject.OnJoinInit();
    }

    private void CommaMergePlus_clicked()
    {
        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.COMMA_MERGE_PLUS;
        TuningSpace.Instance.SelectedObject.OnJoinInit();
    }

    private void MappingMergeMinus_clicked()
    {
        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.MAPPING_MERGE_MINUS;
        TuningSpace.Instance.SelectedObject.OnJoinInit();
    }

    private void MappingMergePlus_clicked()
    {
        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.MAPPING_MERGE_PLUS;
        TuningSpace.Instance.SelectedObject.OnJoinInit();
    }

    private void MOSScreenshot_clicked()
    {
        screenshotCamera.enabled = true;
        string file = "Screenshot" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        //ScreenCapture.CaptureScreenshot(file, 1);
        SaveCameraView(screenshotCamera, file);
        Debug.Log("Screenshot captured: " + file);
        screenshotCamera.enabled = false;
    }

    private void MOSBackToPTS_clicked()
    {
        HideMOSMenu();
        TuningSpace.Instance.gameObject.SetActive(true);
        TuningSpace.Instance.miniMosMesh.gameObject.SetActive(false);
        TuningSpace.Instance.mos.gameObject.SetActive(false);
        TuningSpace.Instance.gameMode = TuningSpace.GameMode.PTS;
        ShowPTSMenu();
    }

    private void MOSCreate_clicked()
    {
        string generatorInput = m_MOSInfo.Q<TextField>("MOSGenerator").value;
        string periodInput = m_MOSInfo.Q<TextField>("MOSPeriod").value;
        string equivalenceIntervalInput = m_MOSInfo.Q<TextField>("MOSEquivalenceInterval").value;

        decimal generatorCents = (decimal)XenMath.getCents(generatorInput);
        decimal periodCents = (decimal)XenMath.getCents(periodInput);
        decimal equivalenceIntervalCents = (decimal)XenMath.getCents(equivalenceIntervalInput);

        if (generatorCents < 0)
            throw new ArgumentException("Generator cents cannot be below 0");
        if (periodCents < 0)
            throw new ArgumentException("Period cents cannot be below 0");
        if (equivalenceIntervalCents < 0)
            throw new ArgumentException("Equivalence Interval cents cannot be below 0");

        TuningSpace.Instance.ViewMainMos(generatorCents, periodCents, equivalenceIntervalCents);
        TuningSpace.Instance.gameMode = TuningSpace.GameMode.MOS;
    }

    private void InitializePrimeBasis()
    {
        //init prime basis
        float primeX, primeY, primeZ;
        string x = m_PrimeBasis.Q<TextField>("PrimeX").value;
        string y = m_PrimeBasis.Q<TextField>("PrimeY").value;
        string z = m_PrimeBasis.Q<TextField>("PrimeZ").value;
        //float.TryParse(x, out primeX);
        //float.TryParse(y, out primeY);
        //float.TryParse(z, out primeZ);
        primeX = getRatioAsDecimal(x);
        primeY = getRatioAsDecimal(y);
        primeZ = getRatioAsDecimal(z);
        PrimeBasis primes = new PrimeBasis(primeX, primeY, primeZ);
        TuningSpace.Instance.primes = primes;
    }

    private void MiniMOSHide_clicked()
    {
        HideMOSMenu();
        TuningSpace.Instance.miniMosMesh.gameObject.SetActive(false);
        ShowPTSMenu();
    }

    private void MiniMOSViewFullMOS_clicked()
    {
        TuningSpace.Instance.mos.gameObject.SetActive(true);
        TuningSpace.Instance.ViewMainMos(TuningSpace.Instance.SelectedObject);
        TuningSpace.Instance.miniMosMesh.gameObject.SetActive(false);
        TuningSpace.Instance.gameObject.SetActive(false);
        TuningSpace.Instance.gameMode = TuningSpace.GameMode.MOS;
        HidePTSMenu();
        ShowMOSMenu();
        FPSFlyer.Instance.ResetPosition();
    }

    private void LinkXenCalc_clicked()
    {
        Application.OpenURL(TuningSpace.Instance.SelectedObject.XenCalcURL);
    }

    private void LinkScaleWorkshopMapping_clicked()
    {
        Application.OpenURL(TuningSpace.Instance.SelectedObject.ScaleWorkshopURL);
    }

    private void LinkScaleWorkshopComma_clicked()
    { 
        Application.OpenURL(TuningSpace.Instance.SelectedObject.ScaleWorkshopURL);
    }

    private void LinkX31EQ_clicked()
    {
        Application.OpenURL(TuningSpace.Instance.SelectedObject.X31EQURL);
    }

    private void LinkXenWiki_clicked()
    {
        Application.OpenURL(TuningSpace.Instance.SelectedObject.XenWikiURL);
    }

    private void CommaViewHoragram_clicked()
    {
        //TuningSpace.Instance.ViewMiniMos(TuningSpace.Instance.SelectedObject);
        MiniMOSViewFullMOS_clicked();
    }

    private void MappingViewHoragram_clicked()
    {
        //TuningSpace.Instance.ViewMiniMos(TuningSpace.Instance.SelectedObject);
        MiniMOSViewFullMOS_clicked();
    }

    private void CommaSetPosition_clicked()
    {
        FPSFlyer.Instance.SetPosition(((Comma)TuningSpace.Instance.SelectedObject).transform.position);
    }

    private void MappingSetPosition_clicked()
    {
        FPSFlyer.Instance.SetPosition(((Mapping)TuningSpace.Instance.SelectedObject).transform.position);
    }

    private void MetaZoomSlider_changed(float newValue)
    {
        TuningSpace.Instance.MetaZoom = newValue;
    }

    private void RainbowIntervalSelectorMenu_clicked(ClickEvent evt)
    {
        RefreshRainbowIntervalSelectorMenu();
    }

    private void RainbowIntervalSelectorMenu_selected(string newValue)
    {
        if (!string.IsNullOrWhiteSpace(newValue))
        {
            string[] monzos = newValue.Replace("[", "").Replace(">", "").Trim().Split(" ");
            TuningSpace.Instance.rainbowIntervalSelection = new Monzo(float.Parse(monzos[0]), float.Parse(monzos[1]), float.Parse(monzos[2]));
        }
    }

    public void RefreshRainbowIntervalSelectorMenu()
    {
        DropdownField m_RainbowIntervalSelectorMenu = m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector");
        m_RainbowIntervalSelectorMenu.choices.Clear();
        m_RainbowIntervalSelectorMenu.choices.AddRange(TuningSpace.Instance.GetAllCommaNames());
    }

    private void QuestionCancel_clicked()
    {
        m_QuestionWindow.style.display = DisplayStyle.None;
        ResetQuestionResponses();
    }

    private void ResetQuestionResponses()
    {
        foreach (Button child in m_QuestionResponses.Children().Where(x => x.GetType() == typeof(Button)))
        {
            child.clickable.clickedWithEventInfo -= evt => QuestionResponse_clicked(child.text);
            m_QuestionResponses.Remove(child);
        }
        questionType = QuestionType.NONE;
    }

    private void QuestionResponse_clicked(string response)
    {
        QuestionResponse = response;
        ResetQuestionResponses();
    }

    private void InfoOK_clicked()
    {
        m_InfoWindow.style.display = DisplayStyle.None;
    }

    private void ErrorsOK_clicked()
    {
        m_ErrorsWindow.style.display = DisplayStyle.None;
    }

    private void MappingColorStyleMenu_selected(string newValue)
    {
        System.Enum.TryParse(newValue, true, out TuningSpace.Instance.mappingColorStyle);
        if (TuningSpace.Instance.mappingColorStyle == TuningSpace.MappingColorStyle.RAINBOW_INTERVAL)
            m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector").style.display = DisplayStyle.Flex;
        else
            m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector").style.display = DisplayStyle.None;
    }

    private void CommaTextStyleMenu_selected(string newValue)
    {
        System.Enum.TryParse(newValue, true, out TuningSpace.Instance.commaTextStyle);
    }

    private void MappingTextStyleMenu_selected(string newValue)
    {
        System.Enum.TryParse(newValue, true, out TuningSpace.Instance.mappingTextStyle);
    }

    private void CreateMappingTypeMenu_selected(string value)
    {
        System.Enum.TryParse(value, true, out createMappingType);
        switch (createMappingType)
        {
            case CreateMappingType.VALS:
                root.Q<VisualElement>("CreateMappingFromVals").style.display = DisplayStyle.Flex;
                root.Q<VisualElement>("CreateMappingFromED").style.display = DisplayStyle.None;
                root.Q<VisualElement>("CreateMappingFromCents").style.display = DisplayStyle.None;
                break;
            case CreateMappingType.ED:
                root.Q<VisualElement>("CreateMappingFromVals").style.display = DisplayStyle.None;
                root.Q<VisualElement>("CreateMappingFromED").style.display = DisplayStyle.Flex;
                root.Q<VisualElement>("CreateMappingFromCents").style.display = DisplayStyle.None;
                break;
            case CreateMappingType.CENTS:
                root.Q<VisualElement>("CreateMappingFromVals").style.display = DisplayStyle.None;
                root.Q<VisualElement>("CreateMappingFromED").style.display = DisplayStyle.None;
                root.Q<VisualElement>("CreateMappingFromCents").style.display = DisplayStyle.Flex;
                break;
            default:
                break;
        }
    }

    private void CreateCommaTypeMenu_selected(string value)
    {
        System.Enum.TryParse(value, true, out createCommaType);
        switch (createCommaType)
        {
            case CreateCommaType.MONZOS:
                root.Q<VisualElement>("CreateCommaFromMonzos").style.display = DisplayStyle.Flex;
                root.Q<VisualElement>("CreateCommaFromRatio").style.display = DisplayStyle.None;
                root.Q<VisualElement>("CreateCommaFromName").style.display = DisplayStyle.None;
                break;
            case CreateCommaType.RATIO:
                root.Q<VisualElement>("CreateCommaFromMonzos").style.display = DisplayStyle.None;
                root.Q<VisualElement>("CreateCommaFromRatio").style.display = DisplayStyle.Flex;
                root.Q<VisualElement>("CreateCommaFromName").style.display = DisplayStyle.None;
                break;
            case CreateCommaType.NAME:
                root.Q<VisualElement>("CreateCommaFromMonzos").style.display = DisplayStyle.None;
                root.Q<VisualElement>("CreateCommaFromRatio").style.display = DisplayStyle.None;
                root.Q<VisualElement>("CreateCommaFromName").style.display = DisplayStyle.Flex;
                break;
        }
    }

    private void ResetPositionButton_clicked()
    {
        FPSFlyer.Instance.ResetPosition();
    }

    private void CommaDelete_clicked()
    {
        StartCoroutine(TuningSpace.Instance.Delete((Comma)TuningSpace.Instance.SelectedObject));
        root.Q<VisualElement>("CommaInfo").style.display = DisplayStyle.None;
    }

    private void CommaJoin_clicked()
    {
        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.COMMA;
        TuningSpace.Instance.SelectedObject.OnJoinInit();
    }

    private void MappingDelete_clicked()
    {
        StartCoroutine(TuningSpace.Instance.Delete((Mapping)TuningSpace.Instance.SelectedObject));
        root.Q<VisualElement>("MappingInfo").style.display = DisplayStyle.None;
    }

    private void MappingJoin_clicked()
    {
        if (((Mapping)TuningSpace.Instance.SelectedObject).mappingType != Mapping.MappingType.JIP
         && ((Mapping)TuningSpace.Instance.SelectedObject).mappingType != Mapping.MappingType.TOP)
            TuningSpace.Instance.joinMode = TuningSpace.JoinMode.MAPPING;
            TuningSpace.Instance.SelectedObject.OnJoinInit();
    }

    public Camera screenshotCamera;
    private void ScreenshotButton_clicked()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            throw new NotImplementedException();
        }

        screenshotCamera.enabled = true;
        string file = "Screenshot" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        //ScreenCapture.CaptureScreenshot(file, 1);
        SaveCameraView(screenshotCamera, file);
        Debug.Log("Screenshot captured: " + file);
        screenshotCamera.enabled = false;
    }

    void SaveCameraView(Camera cam, string path)
    {
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;
        byte[] byteArray = renderedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, byteArray);
    }

    private void CreateCommaButton_clicked()
    {
        Comma c = null;
        switch (createCommaType)
        {
            case CreateCommaType.MONZOS:
                c = CreateCommaFromMonzos();
                break;
            case CreateCommaType.RATIO:
                c = CreateCommaFromRatio();
                break;
            case CreateCommaType.NAME:
                c = CreateCommaFromName();
                break;
            default:
                break;
        }
        if (c != null)
        {
            TuningSpace.Instance.SelectedObject = c;
            if (TuningSpace.Instance.CenterNewTemperament)
                FPSFlyer.Instance.SetPosition(c.transform.position + new Vector3(0, 0, -1));
            c.SetCharacterSize();
        }
    }

    private Comma CreateCommaFromMonzos()
    {
        float monzoX, monzoY, monzoZ;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaX").value, out monzoX);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaY").value, out monzoY);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaZ").value, out monzoZ);
        Comma c = TuningSpace.Instance.MakeComma(monzoX, monzoY, monzoZ);
        return c;
    }

    private Comma CreateCommaFromRatio()
    {
        //need to check if number is supported by prime basis
        float n, d;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaNumerator").value, out n);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaDenominator").value, out d);
        Monzo monzos = GetMonzosFromRatio(n, d, TuningSpace.Instance.primes);
        Comma c = TuningSpace.Instance.MakeComma(monzos.X, monzos.Y, monzos.Z);
        return c;
    }

    private Comma CreateCommaFromName()
    {
        string commaName = m_PTSObjectCreator.Q<TextField>("CreateCommaName").value;

        if (string.IsNullOrWhiteSpace(commaName))
            throw new System.FormatException("Rank-2 temperament name cannot be empty!");
        
        if (TuningSpace.Instance.primes.ToString() == "2.3.5" && string.Equals(commaName.Replace(" ", ""), "middlepath", StringComparison.InvariantCultureIgnoreCase))
        {
            //Easter Egg! Display all the rank-2 temperaments from Paul Erlich's Middle Path paper!
            TuningSpace.Instance.MakeDefaultCommas();
            return null;
        }
        
        //need to check if name is real
        Monzo monzos = NamedTemperaments.WhatMonzos(TuningSpace.Instance.primes, commaName);
        if (monzos != (object)null && monzos.Monzos != null)
        {
            Comma c = TuningSpace.Instance.MakeComma(monzos.X, monzos.Y, monzos.Z);
            return c;
        }
        else
            throw new System.FormatException($"The temperament {m_PTSObjectCreator.Q<TextField>("CreateCommaName").value} is not within the prime basis {TuningSpace.Instance.primes}.");
    }

    private void CreateMappingButton_clicked()
    {
        Mapping m = null;
        switch (createMappingType)
        {
            case CreateMappingType.VALS:
                m = CreateMappingFromVals();
                break;
            case CreateMappingType.ED:
                m = CreateMappingFromED();
                break;
            case CreateMappingType.CENTS:
                m = CreateMappingFromCents();
                break;
            default:
                break;
        }
        if (m != null)
        {
            TuningSpace.Instance.SelectedObject = m;
            if (TuningSpace.Instance.CenterNewTemperament)
                FPSFlyer.Instance.SetPosition(m.transform.position);
        }
    }

    private Mapping CreateMappingFromVals()
    {
        float valX, valY, valZ;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingX").value, out valX);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingY").value, out valY);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingZ").value, out valZ);
        Mapping m = TuningSpace.Instance.MakeMapping(valX, valY, valZ);
        return m;
    }

    private Mapping CreateMappingFromED()
    {
        float ed, of;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingED").value, out ed);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingEDOf").value, out of);
        Val vals = GetPatentVal(ed, TuningSpace.Instance.primes);
        Mapping m = TuningSpace.Instance.MakeMapping(vals.X, vals.Y, vals.Z);
        return m;
    }

    private Mapping CreateMappingFromCents()
    {
        float cents;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingCents").value, out cents);
        int ed = Mathf.RoundToInt(TuningSpace.Instance.primes.X / cents);
        Val vals = GetPatentVal(ed, TuningSpace.Instance.primes);
        Mapping m = TuningSpace.Instance.MakeMapping(vals.X, vals.Y, vals.Z);
        return m;
    }

    private void DestroyMappingsAndCommas_clicked()
    {
        //ShowInfo(TuningSpace.Instance.MappingsCount + " Rank-1s to be deleted.");
        TuningSpace.Instance.DeleteAllMappings();
        TuningSpace.Instance.DeleteAllCommas();
        HidePTSObjectInfo();

        DropdownField m_RainbowIntervalSelectorMenu = m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector");
        m_RainbowIntervalSelectorMenu.choices.Clear();
    }

    private void PrimeBasisDestroyAll_clicked()
    {
        TuningSpace.Instance.DeleteAll();
        HidePTSObjectInfo();
    }

    private void DestroyCommas_clicked()
    {
        TuningSpace.Instance.DeleteAllCommas();
        if (TuningSpace.Instance.SelectedObject == null || TuningSpace.Instance.SelectedObject is Comma)
            HidePTSObjectInfo();

        DropdownField m_RainbowIntervalSelectorMenu = m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector");
        m_RainbowIntervalSelectorMenu.choices.Clear();
    }

    private void DestroyMappings_clicked()
    {
        //ShowInfo(TuningSpace.Instance.MappingsCount + " Rank-1s to be deleted.");
        TuningSpace.Instance.DeleteAllMappings();
        if (TuningSpace.Instance.SelectedObject == null || TuningSpace.Instance.SelectedObject is Mapping)
            HidePTSObjectInfo();
    }

    private void PrimeBasisInit_clicked()
    {
        TuningSpace.Instance.MakeJIP();
        TuningSpace.Instance.MakeDamageHexagons(10);
        TuningSpace.Instance.MakeBoundaryMappingsAndCommas();
    }

    private void PrimeBasisCreate_clicked()
    {
        float primeX, primeY, primeZ;
        string x = m_PrimeBasis.Q<TextField>("PrimeX").value;
        string y = m_PrimeBasis.Q<TextField>("PrimeY").value;
        string z = m_PrimeBasis.Q<TextField>("PrimeZ").value;
        //float.TryParse(x, out primeX);
        //float.TryParse(y, out primeY);
        //float.TryParse(z, out primeZ);
        primeX = getRatioAsDecimal(x);
        primeY = getRatioAsDecimal(y);
        primeZ = getRatioAsDecimal(z);
        //Debug.Log($"X: {primeX}; Y: {primeY}; Z: {primeZ}");
        PrimeBasis primes = new PrimeBasis(primeX, primeY, primeZ);
        TuningSpace.Instance.primes = primes;
        
        float minET;
        float.TryParse(m_PrimeBasis.Q<TextField>("MinET").value, out minET);
        TuningSpace.Instance.min_val = minET;
        float maxET;
        float.TryParse(m_PrimeBasis.Q<TextField>("MaxET").value, out maxET);
        TuningSpace.Instance.max_val = maxET;

        int maxOffset;
        int.TryParse(m_PrimeBasis.Q<TextField>("MaxOffset").value, out maxOffset);
        TuningSpace.Instance.max_offset = maxOffset;

        TuningSpace.Instance.MakeMappings();
    }

    public void HidePTSObjectInfo()
    {
        root.Q<VisualElement>("CommaInfo").style.display = DisplayStyle.None;
        root.Q<VisualElement>("MappingInfo").style.display = DisplayStyle.None;
    }

    public void UpdatePTSObjectInfo(Mapping m)
    {
        m_CommaInfo.style.display = DisplayStyle.None;
        m_MappingInfo.style.display = DisplayStyle.Flex;

        m_MappingInfo.Q<Label>("MappingVals").text = m.vals.ToString();
        m_MappingInfo.Q<Label>("MappingWart").text = m.vals.X + m.warts;
        m_MappingInfo.Q<TextField>("MappingStepSize").value = m.stepSize.ToString();
        m_MappingInfo.Q<TextField>("MappingTOPOffset").value = m.topOffset.ToString();
        m_MappingInfo.Q<TextField>("MappingWeightedX").value = m.w_vals.Item1.ToString();
        m_MappingInfo.Q<TextField>("MappingWeightedY").value = m.w_vals.Item2.ToString();
        m_MappingInfo.Q<TextField>("MappingWeightedZ").value = m.w_vals.Item3.ToString();
        if (((Mapping)TuningSpace.Instance.SelectedObject).mappingType == Mapping.MappingType.JIP
         || ((Mapping)TuningSpace.Instance.SelectedObject).mappingType == Mapping.MappingType.TOP)
            m_MappingInfo.Q<Button>("MappingJoin").style.display = DisplayStyle.None;
        else
            m_MappingInfo.Q<Button>("MappingJoin").style.display = DisplayStyle.Flex;
    }

    public void UpdatePTSObjectInfo(Comma c)
    {
        m_MappingInfo.style.display = DisplayStyle.None;
        m_CommaInfo.style.display = DisplayStyle.Flex;

        m_CommaInfo.Q<Label>("CommaMonzos").text = c.TemperedInterval.Monzos.ToString();
        m_CommaInfo.Q<Label>("CommaName").text = c.title;
        m_CommaInfo.Q<Label>("CommaRatio").text = c.TemperedInterval.NumeratorBI + "/" + c.TemperedInterval.DenominatorBI;
        m_CommaInfo.Q<TextField>("CommaTemperedIntervalSize").value = XenMath.getCents(c.TemperedInterval).ToString();
        m_CommaInfo.Q<TextField>("CommaPeriod").value = c.period.Divisions == 1 ? "1" : ("1\\" + c.period.Divisions);
        m_CommaInfo.Q<TextField>("CommaGenerator").value = c.generator.Numerator + "/" + c.generator.Denominator;
        m_CommaInfo.Q<TextField>("CommaComplexity").value = c.Complexity.ToString();
        m_CommaInfo.Q<TextField>("CommaTOPDamage").value = c.top_damage.HasValue ? c.top_damage.ToString() : "Could not calculate";
    }

    char prevMappingOrCommaInfo;
    public void ShowHideMenu()
    {
        bool v = !root.Q<VisualElement>("MainScreen").visible;
        root.Q<VisualElement>("MainScreen").visible = v;
        if (v)
        {
            switch (prevMappingOrCommaInfo)
            {
                case 'm':
                    //mapping
                    root.Q<VisualElement>("MappingInfo").style.display = DisplayStyle.Flex;
                    root.Q<VisualElement>("CommaInfo").style.display = DisplayStyle.None;
                    break;
                case 'c':
                    //Comma
                    root.Q<VisualElement>("MappingInfo").style.display = DisplayStyle.None;
                    root.Q<VisualElement>("CommaInfo").style.display = DisplayStyle.Flex;
                    break;
                default:
                    root.Q<VisualElement>("MappingInfo").style.display = DisplayStyle.None;
                    root.Q<VisualElement>("CommaInfo").style.display = DisplayStyle.None;
                    break;
            }
        }
        else
        {
            if (root.Q<VisualElement>("MappingInfo").visible)
                prevMappingOrCommaInfo = 'm';
            if (root.Q<VisualElement>("CommaInfo").visible)
                prevMappingOrCommaInfo = 'c';
            root.Q<VisualElement>("MappingInfo").style.display = DisplayStyle.None;
            root.Q<VisualElement>("CommaInfo").style.display = DisplayStyle.None;
        }
    }

    public void ShowPTSMenu()
    {
        m_PrimeBasis.style.display = DisplayStyle.Flex;
        m_PTSObjectCreator.style.display = DisplayStyle.Flex;
        m_Options.style.display = DisplayStyle.Flex;
        m_PTSObjectInfo.style.display = DisplayStyle.Flex;
    }
    public void HidePTSMenu()
    {
        m_PrimeBasis.style.display = DisplayStyle.None;
        m_PTSObjectCreator.style.display = DisplayStyle.None;
        m_Options.style.display = DisplayStyle.None;
        m_PTSObjectInfo.style.display = DisplayStyle.None;
    }

    public void ShowMOSMenu()
    {
        m_MOSInfo.style.display = DisplayStyle.Flex;
        m_SoundOptions.style.display = DisplayStyle.Flex;
    }

    public void HideMOSMenu()
    {
        m_MOSInfo.style.display = DisplayStyle.None;
        m_SoundOptions.style.display = DisplayStyle.None;
    }

    public void ShowMiniMOSOptions()
    {
        m_MiniMOSOptions.style.display = DisplayStyle.Flex;
        m_MOSOptions.style.display = DisplayStyle.None;
        m_MOSInfo.Q<TextField>("MOSPeriod").isReadOnly = true;
        m_MOSInfo.Q<TextField>("MOSGenerator").isReadOnly = true;
        m_MOSInfo.Q<TextField>("MOSEquivalenceInterval").isReadOnly = true;
        m_MOSInfo.Q<Button>("MOSGeneratorPlus").style.display = DisplayStyle.None;
        m_MOSInfo.Q<Button>("MOSGeneratorMinus").style.display = DisplayStyle.None;
    }

    public void ShowMOSOptions()
    {
        m_MiniMOSOptions.style.display = DisplayStyle.None;
        m_MOSOptions.style.display = DisplayStyle.Flex;
        m_MOSInfo.Q<TextField>("MOSPeriod").isReadOnly = false;
        m_MOSInfo.Q<TextField>("MOSGenerator").isReadOnly = false;
        m_MOSInfo.Q<TextField>("MOSEquivalenceInterval").isReadOnly = false;
        m_MOSInfo.Q<Button>("MOSGeneratorPlus").style.display = DisplayStyle.Flex;
        m_MOSInfo.Q<Button>("MOSGeneratorMinus").style.display = DisplayStyle.Flex;
    }

    public void UpdateMOSInfo(string period, string generator, string equivalenceInterval)
    {
        m_MOSInfo.Q<TextField>("MOSPeriod").value = period;
        m_MOSInfo.Q<TextField>("MOSGenerator").value = generator;
        m_MOSInfo.Q<TextField>("MOSEquivalenceInterval").value = equivalenceInterval;
    }

    public void ShowQuestion(string question, string[] responses, QuestionType qType)
    {
        m_QuestionWindow.Q<Label>("QuestionText").text = question;
        foreach (string response in responses)
        {
            Button button = new Button();
            button.name = response;
            button.text = response;
            button.clickable.clickedWithEventInfo += evt => QuestionResponse_clicked(response);
            m_QuestionResponses.Add(button);
        }
    }

    public void ShowInfo(string info)
    {
        m_InfoWindow.Q<Label>("InfoText").text = info;
        m_InfoWindow.style.display = DisplayStyle.Flex;
    }

    public void ShowError(string logString, string stackTrace, LogType type)
    {
        string message;
        if (logString.StartsWith("Exception: "))
        {
            message = logString.Split("Exception: ")[1];
        }
        else
        {
            message = logString;
        }
        m_ErrorsWindow.Q<Label>("ErrorText").text = message;
        m_ErrorsWindow.style.display = DisplayStyle.Flex;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
