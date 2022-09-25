using UnityEngine;
public class ExceptionManager : MonoBehaviour
{
    //UIHandler UIHandler.Instance;

    void Awake()
    {
        //UIHandler.Instance = GameObject.Find("UI").GetComponent<UIHandler>();
        Application.logMessageReceived += HandleException;
        DontDestroyOnLoad(gameObject);
    }

    void HandleException(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            //handle here
            UIHandler.Instance.ShowError(logString, stackTrace, type);
        }
    }
}