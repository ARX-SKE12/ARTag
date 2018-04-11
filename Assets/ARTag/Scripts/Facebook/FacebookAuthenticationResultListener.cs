
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Facebook.Unity;
    using FBAuthKit;
    using SocketIOManager;
    using SocketIO;
    using PublisherKit;

    public class FacebookAuthenticationResultListener : Publisher
    {
        SocketManager socketManager;
        const string TOKEN = "token";
        const string NAME = "name";
        const string PROFILE_PICTURE = "profilePictureURL";
        const string ID = "id";

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

        void OnAuthSuccess(AccessToken token)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data[TOKEN] = token.TokenString;
            socketManager.Emit(EventsCollector.AUTH, new JSONObject(data));
        }
        #endregion

#region Socket IO Authentication
        void OnBackendAuthSuccess(SocketIOEvent e) {
            JSONObject data = e.data;
            Broadcast("OnBackendAuthSuccess", data.GetField(NAME).str);
            PlayerPrefs.SetString(NAME, data.GetField(NAME).str);
            PlayerPrefs.SetString(PROFILE_PICTURE, data.GetField(PROFILE_PICTURE).str);
            PlayerPrefs.SetString(ID, data.GetField(ID).str);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        void OnBackendAuthError(SocketIOEvent e)
        {
            Broadcast("OnAuthFailure");
        }
        #endregion

    }

}
