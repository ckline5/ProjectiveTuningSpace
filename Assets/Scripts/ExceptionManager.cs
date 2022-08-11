using UnityEngine;
public class ExceptionManager : MonoBehaviour
{
    UIHandler ui;

    void Awake()
    {
        ui = GameObject.Find("UI").GetComponent<UIHandler>();
        Application.logMessageReceived += HandleException;
        DontDestroyOnLoad(gameObject);
    }

    void HandleException(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            //handle here
            ui.ShowError(logString, stackTrace, type);
        }
    }
}