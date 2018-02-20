using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookController : Publisher {

#region Unity Behaviour
    void Awake()
    {
        InitFacebookSDKConnection();
    }
#endregion

#region Initialize SDK
    void InitFacebookSDKConnection()
    {
        if (!FB.IsInitialized) FB.Init(OnInitSuccess, OnHideUnity);
        else
        {
            FB.ActivateApp();
            Broadcast("OnAuthSuccess", AccessToken.CurrentAccessToken.TokenString);
        }
    }

    void OnInitSuccess()
    {
        if (FB.IsInitialized) FB.ActivateApp();
        else GetComponent<ApplicationController>().ToastMessage("Failed to Initialize the Facebook SDK");
    }

    void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
#endregion

#region Auth
    public void Auth()
    {
        List<string> permissions = new List<string>() { "public_profile" };
        Broadcast("OnAuthRequest");
        FB.LogInWithReadPermissions(permissions, AuthCallback);
    }
    
    void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = AccessToken.CurrentAccessToken;
            Broadcast("OnAuthSuccess", token.TokenString);
        } else
        {
            Broadcast("OnAuthFailure");
        }
    }
    #endregion

}
