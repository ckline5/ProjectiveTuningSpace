#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FPSFlyer))]
public class FPSFlyerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FPSFlyer f = (FPSFlyer)target;
        if (GUILayout.Button("Reset Position"))
        {
            f.ResetPosition();
        }
    }
}
#endif