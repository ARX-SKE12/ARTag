using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookController : MonoBehaviour {

    public GameObject spinner, loginButton;

    void Awake()
    {
        InitFacebookSDKConnection();
    }

    void InitFacebookSDKConnection()
    {
        if (!FB.IsInitialized) FB.Init(OnInitSuccess, OnHideUnity);
        else
        {
            FB.ActivateApp();
            loginButton.SetActive(true);
        }
    }

    void OnInitSuccess()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            loginButton.SetActive(true);
        }
        else GetComponent<ApplicationController>().ToastMessage("Failed to Initialize the Facebook SDK");
    }

    void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void Auth()
    {
        List<string> permissions = new List<string>() { "public_profile" };
        spinner.SetActive(true);
        loginButton.SetActive(false);
        FB.LogInWithReadPermissions(permissions, AuthCallback);
    }

    void AuthCallback(ILoginResult result)
    {
        StartCoroutine(HideSpinner());
        if (FB.IsLoggedIn)
        {
            AccessToken token = Facebook.Unity.AccessToken.CurrentAccessToken;
            Debug.Log(token.UserId);
        } else
        {
            loginButton.SetActive(true);
        }
    }

    IEnumerator HideSpinner()
    {
        yield return new WaitForSeconds(1);
        spinner.SetActive(false);
    }
}
