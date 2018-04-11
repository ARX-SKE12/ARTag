
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using Facebook.Unity;
    using FBAuthKit;
    using SocketIOManager;
    using SocketIO;

    public class FacebookAuthenticationResultListener : MonoBehaviour
    {
        SocketManager socketManager;
        const string TOKEN = "token";
        const string NAME = "name";
        const string PROFILE_PICTURE = "profilePictureURL";
        const string ID = "id";
        bool isInitialized;

#region Unity Behavior
        void Start()
        {
            GameObject.Find(ObjectsCollector.FACEBOOK_AUTH_CONTROLLER_OBJECT).GetComponent<FacebookAuthController>().Register(gameObject);
            socketManager = GameObject.Find(ObjectsCollector.SOCKETIO_MANAGER_OBJECT).GetComponent<SocketManager>();
            socketManager.On(EventsCollector.AUTH_SUCCESS, OnBackendAuthSuccess);
            socketManager.On(EventsCollector.AUTH_ERROR, OnBackendAuthError);
        }
        #endregion

#region Facebook SDK Authentication
        void OnAuthRequest()
        {
            GameObject.Find(ObjectsCollector.FACEBOOK_STATUS_TEXT).GetComponent<Text>().text = "Authenticating...";
        }

        void OnAuthSuccess(AccessToken token)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data[TOKEN] = token.TokenString;
            socketManager.Emit(EventsCollector.AUTH, new JSONObject(data));
        }

        void OnAuthFailure()
        {
            GameObject.Find(ObjectsCollector.FACEBOOK_STATUS_TEXT).GetComponent<Text>().text = "Authentication Failure";
        }
        #endregion

#region Socket IO Authentication
        void OnBackendAuthSuccess(SocketIOEvent e) {
            if (isInitialized) return;
            JSONObject data = e.data;
            GameObject.Find(ObjectsCollector.FACEBOOK_STATUS_TEXT).GetComponent<Text>().text = data.GetField(NAME).str;
            PlayerPrefs.SetString(NAME, data.GetField(NAME).str);
            PlayerPrefs.SetString(PROFILE_PICTURE, data.GetField(PROFILE_PICTURE).str);
            PlayerPrefs.SetString(ID, data.GetField(ID).str);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            isInitialized = true;
        }

        void OnBackendAuthError(SocketIOEvent e)
        {
            GameObject.Find(ObjectsCollector.FACEBOOK_STATUS_TEXT).GetComponent<Text>().text = "Authentication Failure";
        }
        #endregion

    }

}
