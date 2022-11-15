using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FPSFlyer : MonoBehaviour
{
    private static FPSFlyer _instance;
    public static FPSFlyer Instance { get { return _instance; } }

    public float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 dragOrigin;
    private Vector3? positionToMoveTo = null;

    CharacterController controller;
    //TuningSpace TuningSpace.Instance;
    //UIHandler UIHandler.Instance;
    VisualElement root;

    void FixedUpdate()
    {
        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("UpDown"));
        //if (moveDirection != Vector3.zero)
        //{
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= speed * -Mathf.Pow(transform.position.z + .01f, 1);

        //    this.transform.position += moveDirection * Time.deltaTime;
        //    TuningSpace.Instance.Zoom = Mathf.Abs(this.transform.position.z - TuningSpace.Instance.transform.position.z);
        //    //if (transform.position.z > -0.01f) transform.position.Set(transform.position.x, transform.position.y, -0.01f);
        //}

    }

    int highestRecentTouchCount = 0;
    private void Update()
    {
        if (Input.touchCount > highestRecentTouchCount)
            highestRecentTouchCount = Input.touchCount;
        else if (highestRecentTouchCount > 0 && Input.touchCount == 0)
            highestRecentTouchCount = 0;

        if (TuningSpace.Instance.gameMode != TuningSpace.GameMode.MOS)
        {
            CameraDrag();
            ScrollWheelZoom();
            PinchZoom();
        }
        MoveToPosition();
        if (Input.GetButtonDown("Menu"))
        {
            UIHandler.Instance.ShowHideMenu();
        }
    }

    private float touchDist;
    private float lastDist;
    private void PinchZoom()
    {
        if (!UIHandler.Instance.mouseInUI)
        {
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
                {
                    lastDist = Vector2.Distance(touch1.position, touch2.position);
                }

                if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                {
                    float newDist = Vector2.Distance(touch1.position, touch2.position);
                    touchDist = lastDist - newDist;
                    lastDist = newDist;

                    // Your Code Here
                    //Camera.main.fieldOfView += touchDist * 0.1f;
                    float amount = touchDist * 0.001f;
                    ZoomByAmount(-amount);
                }
            }
        }
    }

    private void ScrollWheelZoom()
    {
        if (!UIHandler.Instance.mouseInUI)
        {
            float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");           //This little peece of code is written by JelleWho https://github.com/jellewie

            if (ScrollWheelChange != 0)
            {                                            //If the scrollwheel has changed
                ZoomByAmount(ScrollWheelChange);
            }
        }
    }

    private void ZoomByAmount(float amount)
    {
        positionToMoveTo = null;
        float R = amount * (TuningSpace.Instance.Zoom - .01f);                                   //The radius from current camera
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
        float newZoom = Mathf.Abs(CamZ + Z - TuningSpace.Instance.transform.position.z);
        if (newZoom < 5000)
            transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
        TuningSpace.Instance.Zoom = Mathf.Abs(this.transform.position.z - TuningSpace.Instance.transform.position.z);
    }

    private void CameraDrag()
    {
        if (!UIHandler.Instance.mouseInUI)
        {
            if (highestRecentTouchCount > 1) return;

            var mousePosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = mousePosition;
                positionToMoveTo = null;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * speed, pos.y * speed, 0);
            //move *= -Mathf.Pow(transform.position.z + .01f, 1);
            move *= (TuningSpace.Instance.Zoom * 9/10);

            transform.Translate(-move, Space.World);
            dragOrigin = mousePosition;
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
            TuningSpace.Instance.Zoom = Mathf.Abs(this.transform.position.z - TuningSpace.Instance.transform.position.z);
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        controller = GetComponent<CharacterController>();
        //TuningSpace.Instance = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        //UIHandler.Instance = GameObject.Find("UI").GetComponent<UIHandler>();
        root = UIHandler.Instance.GetComponent<UIDocument>().rootVisualElement;
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
        //positionToMoveTo = position + new Vector3(0, 0, -Mathf.Pow(Vector3.Distance(position, TuningSpace.Instance.JIP().transform.position), 0.5f) - .1f);
        positionToMoveTo = position + new Vector3(0, 0, -1);
    }
}
