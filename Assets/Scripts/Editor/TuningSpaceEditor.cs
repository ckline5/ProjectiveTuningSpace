#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TuningSpace))]
public class TuningSpaceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //TuningSpace TuningSpace.Instance = (TuningSpace)target;
        if (GUILayout.Button("Make JIP"))
        {
            TuningSpace.Instance.MakeJIP();
        }
        if (GUILayout.Button("Make Damage Hexagons (10)"))
        {
            TuningSpace.Instance.MakeDamageHexagons(10);
        }
        if (GUILayout.Button("Make Mappings"))
        {
            TuningSpace.Instance.MakeMappings();
        }
        if (GUILayout.Button("Make Default (5-limit) Commas"))
        {
            TuningSpace.Instance.MakeDefaultCommas();
        }
        if (GUILayout.Button("Delete All Mappings and Commas"))
        {
            TuningSpace.Instance.DeleteAllMappings();
            TuningSpace.Instance.DeleteAllCommas();
        }
        if (GUILayout.Button("Delete All"))
        {
            TuningSpace.Instance.DeleteAll();
        }
    }
}
#endif