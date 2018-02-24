using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class FacebookSocketManager : MonoBehaviour {

    public GameObject facebookController;

    SocketManager socket;

    const string AUTH_SUCCESS_EVENT = "auth-success";
    const string AUTH_ERROR_EVENT = "auth-error";

	// Use this for initialization
	void Start () {
        BindSocket();
        BindFacebook();
	}

    void BindFacebook()
    {
        facebookController.GetComponent<FacebookController>().Register(gameObject);
    }
	
    void BindSocket()
    {
        socket = GetComponent<SocketManager>();
        socket.On(AUTH_SUCCESS_EVENT, OnAuthSuccess);
        socket.On(AUTH_ERROR_EVENT, OnAuthError);
    }

    void OnAuthSuccess(string token)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["token"] = token;
        socket.Emit("auth", new JSONObject(data));
    }

    public void OnAuthError(SocketIOEvent e)
    {

    }

    public void OnAuthSuccess(SocketIOEvent e)
    {
        
    }
}
