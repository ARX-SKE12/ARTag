using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookUIManager : MonoBehaviour {

    public GameObject spinner, loginButton;

	// Use this for initialization
	void Start () {
        BindFacebookController();
	}

    void BindFacebookController()
    {
        GetComponent<FacebookController>().Register(gameObject);
    }

    void OnAuthRequest()
    {
        spinner.SetActive(true);
        loginButton.SetActive(false);
    }

    void OnAuthSuccess()
    {
        StartCoroutine(HideSpinner());
    }

    void OnAuthFailure()
    {
        loginButton.SetActive(true);
    }

    IEnumerator HideSpinner()
    {
        yield return new WaitForSeconds(1);
        spinner.SetActive(false);
    }
}
