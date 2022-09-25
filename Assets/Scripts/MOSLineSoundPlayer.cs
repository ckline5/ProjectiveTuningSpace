using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOSLineSoundPlayer : MonoBehaviour
{
    MOSLine parent;

    private void Awake()
    {
        parent = transform.GetComponentInParent<MOSLine>();
    }


    public void PlaySound()
    {
        Debug.Log(parent.cents);
        //play a sound too
    }

    public void OnMouseEnter()
    {
        if (!UIHandler.Instance.mouseInUI)
            if (parent.lineType == MOSLine.LineType.LINE)
            {
                parent.line.startColor = Color.cyan;
                parent.line.endColor = parent.line.startColor;
            }
    }

    public void OnMouseOver()
    {
        if (UIHandler.Instance.mouseInUI)
            if (parent.lineType == MOSLine.LineType.LINE)
            {
                parent.line.startColor = Color.black;
                parent.line.endColor = parent.line.startColor;
            }
    }

    private void OnMouseExit()
    {
        if (parent.lineType == MOSLine.LineType.LINE)
        {
            parent.line.startColor = Color.black;
            parent.line.endColor = parent.line.startColor;
        }
    }
}
