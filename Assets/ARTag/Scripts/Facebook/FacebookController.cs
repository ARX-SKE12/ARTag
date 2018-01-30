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
        else FB.ActivateApp();
    }

    void OnInitSuccess()
    {
        if (FB.IsInitialized)
        {
            Debug.Log("Facebook SDK Initialization Successful!");
            FB.ActivateApp();
        }
        else Debug.Log("Failed to Initialize the Facebook SDK");
    }

    void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void Auth()
    {
        List<string> permissions = new List<string>() { "public_profile" };
        FB.LogInWithReadPermissions(permissions, AuthCallback);
    }

    void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = Facebook.Unity.AccessToken.CurrentAccessToken;
            Debug.Log(token.UserId);
        } else
        {
            Debug.Log("Cancelled");
        }
    }
}
