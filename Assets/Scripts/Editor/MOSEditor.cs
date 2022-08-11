#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MOS))]
public class MOSEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MOS mos = (MOS)target;
        if (GUILayout.Button("Make MOS"))
        {
            mos.Initialize(mos.transform.position, mos.generatorAsFloat, mos.periodAsFloat, mos.EquivalenceIntervalAsFloat);
        }
        if (GUILayout.Button("Delete MOS"))
        {
            mos.DeleteLines();
        }
        if (GUILayout.Button("Delete MOS Circles"))
        {
            mos.DeleteCircles();
        }
        if (GUILayout.Button("Delete Entire MOS Structure"))
        {
            mos.DeleteAll();
        }
    }
}
#endif