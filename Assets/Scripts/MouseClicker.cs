using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    //TuningSpace TuningSpace.Instance;
    //UIHandler UIHandler.Instance;

    private void Awake()
    {
        //TuningSpace.Instance = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        //UIHandler.Instance = GameObject.Find("UI").GetComponent<UIHandler>();
    }

    object lastClicked;
    Vector3 lastClickedPos;

    // Update is called once per frame
    void Update()
    {
        if (TuningSpace.Instance.SelectedObject != null)
        {
            if (TuningSpace.Instance.SelectedObject is Mapping)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.MAPPING;
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.MAPPING_MERGE_PLUS;
                    if (Input.GetKeyDown(KeyCode.LeftAlt))
                        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.MAPPING_MERGE_MINUS;
                    TuningSpace.Instance.SelectedObject.OnJoinInit();
                }
            }
            else if (TuningSpace.Instance.SelectedObject is Comma)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.COMMA;
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.COMMA_MERGE_PLUS;
                    if (Input.GetKeyDown(KeyCode.LeftAlt))
                        TuningSpace.Instance.joinMode = TuningSpace.JoinMode.COMMA_MERGE_MINUS;
                    TuningSpace.Instance.SelectedObject.OnJoinInit();
                }
            }
            else
            {
                TuningSpace.Instance.joinMode = TuningSpace.JoinMode.NONE;
                TuningSpace.Instance.SelectedObject.OnJoinCancel();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftAlt))
            {
                TuningSpace.Instance.joinMode = TuningSpace.JoinMode.NONE;
                TuningSpace.Instance.SelectedObject.OnJoinCancel();
            }
        }

        if (!UIHandler.Instance.mouseInUI)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000000))
            {
                //Debug.Log(hit.transform.gameObject.name);


                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(hit.collider.gameObject.name);

                    lastClickedPos = Input.mousePosition;
                    PTSObject obj;
                    MOSLineSoundPlayer line;
                    if (hit.transform.gameObject.TryGetComponent<PTSObject>(out obj))
                    {
                        //the gameobject is a PTSObject
                        lastClicked = obj;
                    }
                    else if (hit.transform.gameObject.TryGetComponent<MOSLineSoundPlayer>(out line))
                    {
                        //the gameobject is a MOSLine
                        lastClicked = line;
                    }
                    else
                    {
                        lastClicked = null;
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    PTSObject obj;
                    MOSLineSoundPlayer line;
                    if (hit.transform.gameObject.TryGetComponent<PTSObject>(out obj)
                        && obj == lastClicked)
                    {
                        //the gameobject is a PTSObject
                        if (obj is Comma || (obj is Mapping && (Mapping)obj != TuningSpace.Instance.JIP()))
                            TuningSpace.Instance.SelectObject(obj);
                        //if (obj is Comma)
                        //    Debug.Log($"{hit.point} --> {XenMath.GetGeneratorCentsAtLocation((Comma)obj, hit.point, TuningSpace.Instance.primes)} ({((Comma)obj).generatorCentsPerDistance})");
                    }
                    else if (hit.transform.gameObject.TryGetComponent<MOSLineSoundPlayer>(out line)
                        && line == (object)lastClicked)
                    {
                        //the gameobject is a MOSLine
                        line.PlaySound();
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
                    TuningSpace.Instance.SelectObject(null);
                }
            }
        }
    }
}
