using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FPSFlyer : MonoBehaviour
{
    public float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 dragOrigin;
    private Vector3? positionToMoveTo = null;

    CharacterController controller;
    TuningSpace ts;
    UIHandler ui;
    VisualElement root;

    void FixedUpdate()
    {
        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("UpDown"));
        //if (moveDirection != Vector3.zero)
        //{
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= speed * -Mathf.Pow(transform.position.z + .01f, 1);

        //    this.transform.position += moveDirection * Time.deltaTime;
        //    ts.Zoom = Mathf.Abs(this.transform.position.z - ts.transform.position.z);
        //    //if (transform.position.z > -0.01f) transform.position.Set(transform.position.x, transform.position.y, -0.01f);
        //}

    }

    private void Update()
    {
        CameraDrag();
        MoveToPosition();
        ScrollWheelZoom();
        if (Input.GetButtonDown("Menu"))
        {
            ui.ShowHideMenu();
        }
    }

    private void ScrollWheelZoom()
    {
        if (!ui.mouseInUI)
        {
            float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");           //This little peece of code is written by JelleWho https://github.com/jellewie
            if (ScrollWheelChange != 0)
            {                                            //If the scrollwheel has changed
                positionToMoveTo = null;
                float R = ScrollWheelChange * (ts.Zoom - .01f);                                   //The radius from current camera
                float PosX = transform.eulerAngles.x + 90;              //Get up and down
                float PosY = -1 * (transform.eulerAngles.y - 90);       //Get left to right
                PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
                PosY = PosY / 180 * Mathf.PI;                                       //^
                float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
                float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
                float Y = R * Mathf.Cos(PosX);                                      //^
                float CamX = transform.position.x;                      //Get current camera postition for the offset
                float CamY = transform.position.y;                      //^
                float CamZ = transform.position.z;                      //^
                float newZoom = Mathf.Abs(CamZ + Z - ts.transform.position.z);
                if (newZoom < 5000)
                    transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
                ts.Zoom = Mathf.Abs(this.transform.position.z - ts.transform.position.z);
            }
        }
    }

    private void CameraDrag()
    {
        if (!ui.mouseInUI)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                positionToMoveTo = null;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * speed, pos.y * speed, 0);
            //move *= -Mathf.Pow(transform.position.z + .01f, 1);
            move *= (ts.Zoom - .01f);

            transform.Translate(-move, Space.World);
            dragOrigin = Input.mousePosition;
        }
        else
        {
            dragOrigin = Input.mousePosition;
        }
    }

    private void MoveToPosition()
    {
        if (positionToMoveTo != null)
        {
            if (Vector3.Distance(transform.position, (Vector3)positionToMoveTo) >= .01f)
                transform.position = Vector3.Lerp(transform.position, (Vector3)positionToMoveTo, Time.deltaTime);
            else
                positionToMoveTo = null;
            ts.Zoom = Mathf.Abs(this.transform.position.z - ts.transform.position.z);
        }
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        ts = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        ui = GameObject.Find("UI").GetComponent<UIHandler>();
        root = ui.GetComponent<UIDocument>().rootVisualElement;
        ResetPosition();
    }

    private Vector3 GetDefaultPosition()
    {
        return ProjectionTools.Project(Tuple.Create(1f, 1f, 1f)) - new Vector3(0, 0, 2f);
    }

    public void ResetPosition()
    {
        positionToMoveTo = GetDefaultPosition();
    }

    public void SetPosition(Vector3 position)
    {
        //positionToMoveTo = position + new Vector3(0, 0, -Mathf.Pow(Vector3.Distance(position, ts.JIP().transform.position), 0.5f) - .1f);
        positionToMoveTo = position + new Vector3(0, 0, -1);
    }
}
