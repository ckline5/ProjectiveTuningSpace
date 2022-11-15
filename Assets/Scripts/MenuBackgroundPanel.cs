using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuBackgroundPanel : MonoBehaviour
{
    //UIHandler UIHandler.Instance;

    // variable to hold the panel UIHandler.Instance element
    private List<VisualElement> _menuBackgroundPanelUiElements;


    // Callback to be called when the Mouse enters the panel.
    private void MouseEnterCallback(MouseEnterEvent evt)
    {
        // check if the target of the event is the background element (because UI events "bubble" through the UI tree, so it could be an event for a different element).
        VisualElement target = (VisualElement)evt.target;
        if (_menuBackgroundPanelUiElements.Contains(target))
        {
            //Debug.Log("Mouse has entered the panel");
            //Debug.Log("Update some kind of global flag that says the mouse is now over the UI.");
            UIHandler.Instance.mouseInUI = true;
        }
    }
    
    // Callback to be called when the Mouse leaves the panel.
    private void MouseLeaveCallback(MouseLeaveEvent evt)
    {
        // check if the target of the event is the background element (because UI events "bubble" through the UI tree, so it could be an event for a different element).
        VisualElement target = (VisualElement)evt.target;
        if (_menuBackgroundPanelUiElements.Contains(target))
        {
            //Debug.Log("mouse has left the panel");
            //Debug.Log("Update some kind of global flag that says the mouse has left the UI.");
            UIHandler.Instance.mouseInUI = false;
        }
    }

    // Callback to be called when the Pointer enters the panel.
    private void PointerEnterCallback(PointerEnterEvent evt)
    {
        // check if the target of the event is the background element (because UI events "bubble" through the UI tree, so it could be an event for a different element).
        VisualElement target = (VisualElement)evt.target;
        if (_menuBackgroundPanelUiElements.Contains(target))
        {
            //Debug.Log("Mouse has entered the panel");
            //Debug.Log("Update some kind of global flag that says the mouse is now over the UI.");
            UIHandler.Instance.mouseInUI = true;
        }
    }

    // Callback to be called when the Pointer leaves the panel.
    private void PointerLeaveCallback(PointerLeaveEvent evt)
    {
        // check if the target of the event is the background element (because UI events "bubble" through the UI tree, so it could be an event for a different element).
        VisualElement target = (VisualElement)evt.target;
        if (_menuBackgroundPanelUiElements.Contains(target))
        {
            //Debug.Log("mouse has left the panel");
            //Debug.Log("Update some kind of global flag that says the mouse has left the UI.");
            UIHandler.Instance.mouseInUI = false;
        }
    }

    // Method that registers the MouseEnterCallback on the MouseEnter UI event.
    private void RegisterMouseEnterCallback()
    {
        // Registers a callback on the MouseEnterEvent which will call MouseEnterCallback
        foreach (VisualElement element in _menuBackgroundPanelUiElements)
        {
            element.RegisterCallback<MouseEnterEvent>(MouseEnterCallback);
            element.RegisterCallback<PointerEnterEvent>(PointerEnterCallback);
        }
    }

    // Method that registers the MouseLeaveCallback on the MouseLeave UI event.
    private void RegisterMouseLeaveCallback()
    {
        // Registers a callback on the MouseLeaveEvent which will call MouseLeaveCallback
        foreach (VisualElement element in _menuBackgroundPanelUiElements)
        {
            element.RegisterCallback<MouseLeaveEvent>(MouseLeaveCallback);
            element.RegisterCallback<PointerLeaveEvent>(PointerLeaveCallback);
        }
    }

    private void Start()
    {
        // query and store the panel UIHandler.Instance element
        //UIHandler.Instance = GetComponent<UIHandler>();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _menuBackgroundPanelUiElements = new List<VisualElement>();
        _menuBackgroundPanelUiElements.Add(root.Q<VisualElement>("PrimeBasis"));
        _menuBackgroundPanelUiElements.Add(root.Q<VisualElement>("Options"));
        _menuBackgroundPanelUiElements.Add(root.Q<VisualElement>("PTSObjectCreator"));
        _menuBackgroundPanelUiElements.Add(root.Q<VisualElement>("PTSObjectInfo"));
        _menuBackgroundPanelUiElements.Add(root.Q<VisualElement>("ErrorsWindow"));
        _menuBackgroundPanelUiElements.Add(root.Q<VisualElement>("InfoWindow"));
        _menuBackgroundPanelUiElements.Add(root.Q<VisualElement>("MOSInfo"));

        // register callbacks for MouseEnter and MouseLeave events on the panel element
        RegisterMouseEnterCallback();
        RegisterMouseLeaveCallback();
    }
}