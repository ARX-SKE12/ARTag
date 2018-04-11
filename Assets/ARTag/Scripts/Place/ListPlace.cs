
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using SocketIO;
    using SocketIOManager;
    using FBAuthKit;

    public class ListPlace : MonoBehaviour
    {
        SocketManager manager;
        List<Place> places;

        // Use this for initialization
        void Start()
        {
            places = new List<Place>();
            manager = GameObject.Find(ObjectsCollector.SOCKETIO_MANAGER_OBJECT).GetComponent<SocketManager>();
            manager.On(EventsCollector.PLACE_LIST, LoadPlaces);
            manager.On(EventsCollector.PLACE_LIST_ERROR, OnPlaceListError);
            GetList();
        }

        public void LoadPlaces(SocketIOEvent e)
        {
            JSONObject data = e.data.GetField("places");
            for (int i = 0; i < data.Count; i++)
            {
                places.Add(new Place(data[i]));
            }
            Debug.Log(places.Count);
        }

        public void OnPlaceListError(SocketIOEvent e)
        {
            Debug.Log(e.data.GetField("error").str);
        }

        public void GetList()
        {
            manager.Emit(EventsCollector.PLACE_LIST_REQUEST);
        }
        
    }

}
