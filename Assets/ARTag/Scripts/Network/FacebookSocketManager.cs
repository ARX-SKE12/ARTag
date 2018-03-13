using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using FBAuthKit;
using SocketIOManager;
namespace ARTag
{
    public class FacebookSocketManager : MonoBehaviour
    {

        SocketManager socket;

        const string AUTH_SUCCESS_EVENT = "auth-success";
        const string AUTH_ERROR_EVENT = "auth-error";

        // Use this for initialization
        void Start()
        {
            BindSocket();
            BindFacebook();
        }

        void BindFacebook()
        {
            GameObject.FindObjectOfType<FacebookAuthController>().Register(gameObject);
        }

        void BindSocket()
        {
            socket = GameObject.FindObjectOfType<SocketManager>();
            socket.On(AUTH_SUCCESS_EVENT, OnAuthSuccess);
            socket.On(AUTH_ERROR_EVENT, OnAuthError);
        }

        void OnAuthSuccess(AccessToken token)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["token"] = token.TokenString;
            socket.Emit("auth", new JSONObject(data));
        }

        public void OnAuthError(SocketIOEvent e)
        {

        }

        public void OnAuthSuccess(SocketIOEvent e)
        {

        }
    }

}
