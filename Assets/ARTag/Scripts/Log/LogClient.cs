using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class LogClient : MonoBehaviour {

#region Attribute
    SocketIOComponent socket;
    bool isReady;
    List<string> logQueue;
#endregion

#region Constants
    const string SOCKET_TAG = "log";
#endregion

#region Singleton
    public static LogClient instance;

    void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);
        logQueue = new List<string>();
        DontDestroyOnLoad(gameObject);
    }
    #endregion

#region Unity Behavior
    void Start()
    {
        socket = GameObject.FindObjectOfType<SocketIOComponent>();
        socket.On("open", OnSocketOpen);
    }
    #endregion

    #region Log
    public void OnSocketOpen(SocketIOEvent e)
    {
        isReady = true;
        Log("Client Status", "Connection to Log Server is successful.");
    }

    public void Log(string tag, string msg)
    {
        string logData = tag + ": " + msg;
        logQueue.Add(logData);
        if (isReady)
        {
            foreach (string logMsg in logQueue)
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data["msg"] = logMsg;
                socket.Emit(SOCKET_TAG, new JSONObject(data));
            }
            logQueue.Clear();
        }
    }
    #endregion

}
