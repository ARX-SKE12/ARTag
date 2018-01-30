using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookController : MonoBehaviour {

    void Awake()
    {
        InitFacebookSDKConnection();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitFacebookSDKConnection()
    {
        if (!FB.IsInitialized) FB.Init(OnInitSuccess, OnHideUnity);
        FB.ActivateApp();
    }

    void OnInitSuccess()
    {
        if (FB.IsInitialized) Debug.Log("Facebook SDK Initialization Successful!");
        else Debug.Log("Failed to Initialize the Facebook SDK");
    }

    void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
