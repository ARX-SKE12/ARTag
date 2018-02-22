using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FacebookSocketManager : MonoBehaviour {

    public GameObject facebookController;

    SocketManager socket;

    const string authURL = "http://localhost:3000/auth/facebook/token";

	// Use this for initialization
	void Start () {
        socket = GetComponent<SocketManager>();
        facebookController.GetComponent<FacebookController>().Register(gameObject);
	}
	
    void OnAuthSuccess(string token)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["token"] = token;
        socket.Emit("auth", new JSONObject(data));
    }
}
