using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    TuningSpace ts;
    UIHandler ui;

    private void Awake()
    {
        ts = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        ui = GameObject.Find("UI").GetComponent<UIHandler>();
    }

    PTSObject lastClicked;
    Vector3 lastClickedPos;

    // Update is called once per frame
    void Update()
    {
        if (!ui.mouseInUI)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000000))
            {
                //Debug.Log(hit.transform.gameObject.name);

                if (Input.GetMouseButtonDown(0))
                {
                    lastClickedPos = Input.mousePosition;
                    PTSObject obj;
                    if (hit.transform.gameObject.TryGetComponent<PTSObject>(out obj))
                    {
                        //the gameobject is a PTSObject
                        lastClicked = obj;
                    }
                    else
                    {
                        lastClicked = null;
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    PTSObject obj;
                    if (hit.transform.gameObject.TryGetComponent<PTSObject>(out obj)
                        && obj == lastClicked)
                    {
                        //the gameobject is a PTSObject
                        if (obj is Comma || (obj is Mapping && (Mapping)obj != ts.JIP()))
                        ts.SelectObject(obj);
                    }
                    else
                    {
                        lastClicked = null;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lastClickedPos = Input.mousePosition;
                    lastClicked = null;
                }
                if (Input.GetMouseButtonUp(0) && lastClicked == null && lastClickedPos == Input.mousePosition)
                {
                    ts.SelectObject(null);
                }
            }
        }
    }
}
