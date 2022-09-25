using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PTSObject
{
    void OnSelect();
    void OnJoinInit();
    void OnJoinCancel();
    void OnDeselect();

    string XenWikiURL { get; }
    string X31EQURL { get; }
    string ScaleWorkshopURL { get; }
    string XenCalcURL { get; }
}
