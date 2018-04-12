
namespace ARTag
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using SocketIO;
    using SocketIOManager;
    using LetC;

    public class ListPlace : MonoBehaviour
    {
        SocketManager manager;
        public GameObject placeCard, content, carousel, backgroundImage;
        const string baseUrl = "https://storage.googleapis.com/artag-thumbnail/";

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

        void OnDataChange(GameObject selected)
        {
            //Place place = selected.GetComponent<PlaceCard>().place;
            //backgroundImage.GetComponent<Image>().sprite = selected.GetComponent<PlaceCard>().thumbnail.GetComponent<Image>().sprite;
            //StartCoroutine(UpdateBackground(baseUrl + place.timestamp + "-" + place.name + ".png"));
            StartCoroutine(UpdateBackground(selected));
        }

        IEnumerator UpdateBackground(GameObject selected)
        {
            yield return new WaitUntil(() => selected.GetComponent<PlaceCard>().isThumbnailLoaded);
            Sprite sprite = selected.GetComponent<PlaceCard>().thumbnail.GetComponent<Image>().sprite;
            backgroundImage.GetComponent<Image>().sprite = sprite;
        }
    }

}
