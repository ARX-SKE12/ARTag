
namespace ARTag
{
    using UnityEngine;
    using SocketIO;
    using SocketIOManager;
    using FBAuthKit;

    public class GeneralErrorHandler : MonoBehaviour
    {
        SocketManager socketManager;
        
        // Use this for initialization
        void Start()
        {
            socketManager = GameObject.FindObjectOfType<SocketManager>();
            socketManager.On(EventsCollector.AUTH_ERROR, OnError);
            socketManager.On(EventsCollector.COLLABORATE_ERROR, OnError);
            socketManager.On(EventsCollector.PLACE_CREATE_ERROR, OnError);
            socketManager.On(EventsCollector.PLACE_ERROR_SIGNIFICANT, OnError);
            socketManager.On(EventsCollector.PLACE_LIST_ERROR, OnError);
            socketManager.On(EventsCollector.PLACE_UPDATE_ERROR, OnError);
            socketManager.On(EventsCollector.PLANE_ERROR, OnError);
            socketManager.On(EventsCollector.ROOM_ERROR, OnError);
            socketManager.On(EventsCollector.TAG_ERROR, OnError);
        }

        public void OnError(SocketIOEvent e) {
            if (e.data.GetField("error").str == "Token Lost") GameObject.FindObjectOfType<FacebookAuthController>().Auth();
        }
    }

}
