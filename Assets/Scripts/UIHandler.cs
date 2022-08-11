using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static XenObjects;
using static XenMath;

public class UIHandler : MonoBehaviour
{
    TuningSpace ts;
    FPSFlyer player;

    public bool mouseInUI;

    VisualElement root;

    VisualElement m_PrimeBasis;
    VisualElement m_PTSObjectCreator;
    VisualElement m_Options;
    VisualElement m_PTSObjectInfo;
    VisualElement m_MappingInfo;
    VisualElement m_CommaInfo;
    VisualElement m_ErrorsWindow;

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

    private void Awake()
    {
        ts = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        player = GameObject.Find("Player").GetComponent<FPSFlyer>();
        root = GetComponent<UIDocument>().rootVisualElement;
        screenshotCamera.enabled = false;

        HidePTSObjectInfo();

        m_PrimeBasis = root.Q<VisualElement>("PrimeBasis");
        m_PTSObjectCreator = root.Q<VisualElement>("PTSObjectCreator");
        m_Options = root.Q<VisualElement>("Options");
        m_PTSObjectInfo = root.Q<VisualElement>("PTSObjectInfo");
        m_MappingInfo = m_PTSObjectInfo.Q<VisualElement>("MappingInfo");
        m_CommaInfo = m_PTSObjectInfo.Q<VisualElement>("CommaInfo");
        m_ErrorsWindow = root.Q<VisualElement>("ErrorsWindow");
        m_ErrorsWindow.style.display = DisplayStyle.None;
        m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector").style.display = DisplayStyle.None;

        m_PrimeBasis.Q<Button>("PrimeBasisInit").clicked += PrimeBasisInit_clicked;
        m_PrimeBasis.Q<Button>("PrimeBasisCreate").clicked += PrimeBasisCreate_clicked;
        m_PrimeBasis.Q<Button>("PrimeBasisDestroy").clicked += PrimeBasisDestroy_clicked;
        m_PrimeBasis.Q<Button>("PrimeBasisDestroyAll").clicked += PrimeBasisDestroyAll_clicked;
        m_PTSObjectCreator.Q<Button>("CreateMappingButton").clicked += CreateMappingButton_clicked;
        m_PTSObjectCreator.Q<Button>("CreateCommaButton").clicked += CreateCommaButton_clicked;
        m_Options.Q<Button>("ScreenshotButton").clicked += ScreenshotButton_clicked;
        m_Options.Q<Button>("ResetPositionButton").clicked += ResetPositionButton_clicked;
        m_MappingInfo.Q<Button>("MappingJoin").clicked += MappingJoin_clicked;
        m_MappingInfo.Q<Button>("MappingSetPosition").clicked += MappingSetPosition_clicked;
        m_MappingInfo.Q<Button>("MappingViewHoragram").clicked += MappingViewHoragram_clicked;
        m_MappingInfo.Q<Button>("MappingDelete").clicked += MappingDelete_clicked;
        m_CommaInfo.Q<Button>("CommaJoin").clicked += CommaJoin_clicked;
        m_CommaInfo.Q<Button>("CommaSetPosition").clicked += CommaSetPosition_clicked;
        m_CommaInfo.Q<Button>("CommaViewHoragram").clicked += CommaViewHoragram_clicked;
        m_CommaInfo.Q<Button>("CommaDelete").clicked += CommaDelete_clicked;
        m_MappingInfo.Q<Button>("LinkXenWiki").clicked += LinkXenWiki_clicked;
        m_MappingInfo.Q<Button>("LinkX31EQ").clicked += LinkX31EQ_clicked;
        m_MappingInfo.Q<Button>("LinkScaleWorkshop").clicked += LinkScaleWorkshop_clicked;
        m_MappingInfo.Q<Button>("LinkXenCalc").clicked += LinkXenCalc_clicked;
        m_CommaInfo.Q<Button>("LinkXenWiki").clicked += LinkXenWiki_clicked;
        m_CommaInfo.Q<Button>("LinkX31EQ").clicked += LinkX31EQ_clicked;
        m_CommaInfo.Q<Button>("LinkScaleWorkshop").clicked += LinkScaleWorkshop_clicked;
        m_CommaInfo.Q<Button>("LinkXenCalc").clicked += LinkXenCalc_clicked;
        m_ErrorsWindow.Q<Button>("ErrorsOK").clicked += ErrorsOK_clicked;

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
        m_MappingTextStyleMenu.value = ts.mappingTextStyle.ToString();
        DropdownField m_CommaTextStyleMenu = m_Options.Q<DropdownField>("CommaTextStyle");
        m_CommaTextStyleMenu.choices.Clear();
        m_CommaTextStyleMenu.choices.AddRange(System.Enum.GetNames(typeof(TuningSpace.CommaTextStyle)));
        m_CommaTextStyleMenu.RegisterValueChangedCallback(v => CommaTextStyleMenu_selected(v.newValue));
        m_CommaTextStyleMenu.value = ts.commaTextStyle.ToString();
        DropdownField m_MappingColorStyleMenu = m_Options.Q<DropdownField>("MappingColorStyle");
        m_MappingColorStyleMenu.choices.Clear();
        m_MappingColorStyleMenu.choices.AddRange(System.Enum.GetNames(typeof(TuningSpace.MappingColorStyle)));
        m_MappingColorStyleMenu.RegisterValueChangedCallback(v => MappingColorStyleMenu_selected(v.newValue));
        m_MappingColorStyleMenu.value = ts.mappingColorStyle.ToString();
        DropdownField m_RainbowIntervalSelectorMenu = m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector");
        m_RainbowIntervalSelectorMenu.choices.Clear();
        m_RainbowIntervalSelectorMenu.choices.AddRange(ts.GetAllCommaNames());
        m_RainbowIntervalSelectorMenu.RegisterCallback<ClickEvent>(RainbowIntervalSelectorMenu_clicked);
        m_RainbowIntervalSelectorMenu.RegisterValueChangedCallback(v => RainbowIntervalSelectorMenu_selected(v.newValue));
        m_RainbowIntervalSelectorMenu.value = "";

        Slider m_MetaZoomSlider = m_Options.Q<Slider>("MetaZoomSlider");
        m_MetaZoomSlider.RegisterValueChangedCallback(v => MetaZoomSlider_changed(v.newValue));
    }

    private void LinkXenCalc_clicked()
    {
        Application.OpenURL(ts.SelectedObject.XenCalcURL);
    }

    private void LinkScaleWorkshop_clicked()
    {
        Application.OpenURL(ts.SelectedObject.ScaleWorkshopURL);
    }

    private void LinkX31EQ_clicked()
    {
        Application.OpenURL(ts.SelectedObject.X31EQURL);
    }

    private void LinkXenWiki_clicked()
    {
        Application.OpenURL(ts.SelectedObject.XenWikiURL);
    }

    private void CommaViewHoragram_clicked()
    {
        throw new NotImplementedException();
    }

    private void MappingViewHoragram_clicked()
    {
        throw new NotImplementedException();
    }

    private void CommaSetPosition_clicked()
    {
        player.SetPosition(((Comma)ts.SelectedObject).transform.position);
    }

    private void MappingSetPosition_clicked()
    {
        player.SetPosition(((Mapping)ts.SelectedObject).transform.position);
    }

    private void MetaZoomSlider_changed(float newValue)
    {
        ts.MetaZoom = newValue;
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
            ts.rainbowIntervalSelection = new Monzo(float.Parse(monzos[0]), float.Parse(monzos[1]), float.Parse(monzos[2]));
        }
    }

    public void RefreshRainbowIntervalSelectorMenu()
    {
        DropdownField m_RainbowIntervalSelectorMenu = m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector");
        m_RainbowIntervalSelectorMenu.choices.Clear();
        m_RainbowIntervalSelectorMenu.choices.AddRange(ts.GetAllCommaNames());
    }

    private void ErrorsOK_clicked()
    {
        m_ErrorsWindow.style.display = DisplayStyle.None;
    }

    private void MappingColorStyleMenu_selected(string newValue)
    {
        System.Enum.TryParse(newValue, true, out ts.mappingColorStyle);
        if (ts.mappingColorStyle == TuningSpace.MappingColorStyle.RAINBOW_INTERVAL)
            m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector").style.display = DisplayStyle.Flex;
        else
            m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector").style.display = DisplayStyle.None;
    }

    private void CommaTextStyleMenu_selected(string newValue)
    {
        System.Enum.TryParse(newValue, true, out ts.commaTextStyle);
    }

    private void MappingTextStyleMenu_selected(string newValue)
    {
        System.Enum.TryParse(newValue, true, out ts.mappingTextStyle);
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
        player.ResetPosition();
    }

    private void CommaDelete_clicked()
    {
        ts.Delete((Comma)ts.SelectedObject);
        root.Q<VisualElement>("CommaInfo").style.display = DisplayStyle.None;
    }

    private void CommaJoin_clicked()
    {
        ts.joinMode = TuningSpace.JoinMode.COMMA;
        ts.SelectedObject.OnJoinInit();
    }

    private void MappingDelete_clicked()
    {
        ts.Delete((Mapping)ts.SelectedObject);
        root.Q<VisualElement>("MappingInfo").style.display = DisplayStyle.None;
    }

    private void MappingJoin_clicked()
    {
        if (((Mapping)ts.SelectedObject).mappingType != Mapping.MappingType.JIP
         && ((Mapping)ts.SelectedObject).mappingType != Mapping.MappingType.TOP)
            ts.joinMode = TuningSpace.JoinMode.MAPPING;
            ts.SelectedObject.OnJoinInit();
    }

    public Camera screenshotCamera;
    private void ScreenshotButton_clicked()
    {
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
            ts.SelectedObject = c;
            player.SetPosition(c.transform.position + new Vector3(0, 0, -1));
            c.SetCharacterSize();
        }
    }

    private Comma CreateCommaFromMonzos()
    {
        float monzoX, monzoY, monzoZ;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaX").value, out monzoX);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaY").value, out monzoY);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaZ").value, out monzoZ);
        Comma c = ts.MakeComma(monzoX, monzoY, monzoZ);
        return c;
    }

    private Comma CreateCommaFromRatio()
    {
        //need to check if number is supported by prime basis
        float n, d;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaNumerator").value, out n);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateCommaDenominator").value, out d);
        Monzo monzos = GetMonzosFromRatio(n, d, ts.primes);
        Comma c = ts.MakeComma(monzos.X, monzos.Y, monzos.Z);
        return c;
    }

    private Comma CreateCommaFromName()
    {
        //need to check if name is real
        Monzo monzos = NamedTemperaments.WhatMonzos(ts.primes, m_PTSObjectCreator.Q<TextField>("CreateCommaName").value);
        if (monzos != (object)null && monzos.Monzos != null)
        {
            Comma c = ts.MakeComma(monzos.X, monzos.Y, monzos.Z);
            return c;
        }
        else
            throw new System.FormatException($"The temperament {m_PTSObjectCreator.Q<TextField>("CreateCommaName").value} is not within the prime basis {ts.primes}.");
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
            ts.SelectedObject = m;
            player.SetPosition(m.transform.position);
        }
    }

    private Mapping CreateMappingFromVals()
    {
        float valX, valY, valZ;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingX").value, out valX);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingY").value, out valY);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingZ").value, out valZ);
        Mapping m = ts.MakeMapping(valX, valY, valZ);
        return m;
    }

    private Mapping CreateMappingFromED()
    {
        float ed, of;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingED").value, out ed);
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingEDOf").value, out of);
        Val vals = GetPatentVal(ed, ts.primes);
        Mapping m = ts.MakeMapping(vals.X, vals.Y, vals.Z);
        return m;
    }

    private Mapping CreateMappingFromCents()
    {
        float cents;
        float.TryParse(m_PTSObjectCreator.Q<TextField>("CreateMappingCents").value, out cents);
        int ed = Mathf.RoundToInt(ts.primes.X / cents);
        Val vals = GetPatentVal(ed, ts.primes);
        Mapping m = ts.MakeMapping(vals.X, vals.Y, vals.Z);
        return m;
    }

    private void PrimeBasisDestroy_clicked()
    {
        ts.DeleteAllMappings();
        ts.DeleteAllCommas();
        HidePTSObjectInfo();

        DropdownField m_RainbowIntervalSelectorMenu = m_Options.Q<DropdownField>("MappingColorStyleRainbowIntervalSelector");
        m_RainbowIntervalSelectorMenu.choices.Clear();
    }

    private void PrimeBasisDestroyAll_clicked()
    {
        ts.DeleteAll();
        HidePTSObjectInfo();
    }

    private void PrimeBasisInit_clicked()
    {
        ts.MakeJIP();
        ts.MakeDamageHexagons(10);
    }

    private void PrimeBasisCreate_clicked()
    {
        float primeX, primeY, primeZ;
        float.TryParse(m_PrimeBasis.Q<TextField>("PrimeX").value, out primeX);
        float.TryParse(m_PrimeBasis.Q<TextField>("PrimeY").value, out primeY);
        float.TryParse(m_PrimeBasis.Q<TextField>("PrimeZ").value, out primeZ);
        PrimeBasis primes = new PrimeBasis(primeX, primeY, primeZ);
        ts.primes = primes;

        float maxET;
        float.TryParse(m_PrimeBasis.Q<TextField>("MaxET").value, out maxET);
        ts.max_val = maxET;

        int maxOffset;
        int.TryParse(m_PrimeBasis.Q<TextField>("MaxOffset").value, out maxOffset);
        ts.max_offset = maxOffset;

        ts.MakeMappings();
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
        if (((Mapping)ts.SelectedObject).mappingType == Mapping.MappingType.JIP
         || ((Mapping)ts.SelectedObject).mappingType == Mapping.MappingType.TOP)
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
        m_CommaInfo.Q<TextField>("CommaTOPDamage").value = c.top_damage.ToString();
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
                    //comma
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

    public void ShowError(string logString, string stackTrace, LogType type)
    {
        m_ErrorsWindow.Q<Label>("ErrorText").text = logString.Split("Exception: ")[1];
        m_ErrorsWindow.style.display = DisplayStyle.Flex;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
