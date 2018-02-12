using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class LogClient : MonoBehaviour {

#region Attribute
    SocketIOComponent socket;
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
        DontDestroyOnLoad(gameObject);
    }
    #endregion

#region Unity Behavior
    void Start()
    {
        socket = GameObject.FindObjectOfType<SocketIOComponent>();
    }

    #endregion

#region Log
    public void Log(string tag, string msg)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["msg"] = tag + ": " + msg;
        Debug.Log(socket);
        socket.Emit(SOCKET_TAG, new JSONObject(data));
        socket.Emit(SOCKET_TAG);
    }
    #endregion

}
