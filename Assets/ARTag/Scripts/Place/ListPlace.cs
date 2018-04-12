
namespace ARTag
{
    using UnityEngine;
    using SocketIO;
    using SocketIOManager;
    using LetC;

    public class ListPlace : MonoBehaviour
    {
        SocketManager manager;
        public GameObject placeCard, content, carousel;

        // Use this for initialization
        void Start()
        {
            carousel.GetComponent<Carousel>().Register(gameObject);
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
                Instantiate(placeCard, content.transform).GetComponent<PlaceCard>().Initialize(new Place(data[i]));
            }
        }

        public void OnPlaceListError(SocketIOEvent e)
        {
            Debug.Log(e.data.GetField("error").str);
        }

        public void GetList()
        {
            manager.Emit(EventsCollector.PLACE_LIST_REQUEST);
        }
        
        void ShouldUpdateNextPage()
        {
            GetList();
        }
    }

}
