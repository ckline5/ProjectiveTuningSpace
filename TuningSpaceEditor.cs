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

        TuningSpace ts = (TuningSpace)target;
        if (GUILayout.Button("Make JIP"))
        {
            ts.MakeJIP();
        }
        if (GUILayout.Button("Make Damage Hexagons (10)"))
        {
            ts.MakeDamageHexagons(10);
        }
        if (GUILayout.Button("Make Mappings"))
        {
            ts.MakeMappings();
        }
        if (GUILayout.Button("Make Default (5-limit) Commas"))
        {
            ts.MakeDefaultCommas();
        }
        if (GUILayout.Button("Delete All Mappings and Commas"))
        {
            ts.DeleteAllMappings();
            ts.DeleteAllCommas();
        }
        if (GUILayout.Button("Delete All"))
        {
            ts.DeleteAll();
        }
    }
}
#endif