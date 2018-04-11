
namespace ARTag {
    using UnityEngine;
    using FBAuthKit;
    using UnityEngine.UI;

    public class AuthenticationUIManager : MonoBehaviour
    {

        public GameObject authPanel, failPanel, loginButton;

        void Start()
        {
            GameObject.Find(ObjectsCollector.FACEBOOK_AUTH_CONTROLLER_OBJECT).GetComponent<FacebookAuthController>().Register(gameObject);
            GameObject.FindObjectOfType<FacebookAuthenticationResultListener>().Register(gameObject);
        }

        void OnAuthRequest()
        {
            failPanel.SetActive(false);
            loginButton.SetActive(false);
            authPanel.SetActive(true);
            GameObject.Find(ObjectsCollector.FACEBOOK_STATUS_TEXT).GetComponent<Text>().text = "Logging In";
        }

        void OnAuthFailure()
        {
            failPanel.SetActive(true);
            loginButton.SetActive(true);
            authPanel.SetActive(false);
            GameObject.Find(ObjectsCollector.FACEBOOK_STATUS_TEXT).GetComponent<Text>().text = "Authentication Failure";
        }

        void OnBackendAuthSuccess(string name)
        {
            GameObject.Find(ObjectsCollector.FACEBOOK_STATUS_TEXT).GetComponent<Text>().text = "Welcome, "+name+"!";
        }
    }

}
