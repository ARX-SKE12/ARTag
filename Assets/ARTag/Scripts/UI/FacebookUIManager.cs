using System.Collections;
using FBAuthKit;
using Facebook.Unity;
using UnityEngine;

namespace ARTag
{

    public class FacebookUIManager : MonoBehaviour
    {

        public GameObject spinner, loginButton;

        // Use this for initialization
        void Start()
        {
            BindFacebookController();
        }

        void BindFacebookController()
        {
            GameObject.FindObjectOfType<FacebookAuthController>().Register(gameObject);
        }

        void OnAuthRequest()
        {
            spinner.SetActive(true);
            loginButton.SetActive(false);
        }

        void OnAuthSuccess(AccessToken token)
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

}