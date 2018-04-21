
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using SocketIO;
    using SocketIOManager;
    using LetC;

    public class PlacesManager : MonoBehaviour
    {
        SocketManager manager;
        public GameObject placeCard, content, carousel, backgroundImage, errorNotification;
        Dictionary<string, PlaceCard> places;
        const string baseUrl = "https://storage.googleapis.com/artag-thumbnail/";

        // Use this for initialization
        void Start()
        {
            places = new Dictionary<string, PlaceCard>();
            GameObject.FindObjectOfType<TemporaryDataManager>().Delete("currentPlace");
            carousel.GetComponent<Carousel>().Register(gameObject);
            manager = GameObject.Find(ObjectsCollector.SOCKETIO_MANAGER_OBJECT).GetComponent<SocketManager>();
            manager.On(EventsCollector.PLACE_LIST, LoadPlaces);
            manager.On(EventsCollector.PLACE_LIST_ERROR, OnPlaceListError);
            manager.On(EventsCollector.PLACE_DATA_UPDATE, OnPlaceDataUpdate);
            GetList();
        }

        public void LoadPlaces(SocketIOEvent e)
        {
            JSONObject data = e.data.GetField("places");
            for (int i = 0; i < data.Count; i++) UpdatePlace(data[i]);
        }

        public void OnPlaceListError(SocketIOEvent e)
        {
            errorNotification.GetComponentInChildren<Text>().text = e.data.GetField("error").str;
            errorNotification.SetActive(true);
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
            StartCoroutine(UpdateBackground(selected));
        }

        IEnumerator UpdateBackground(GameObject selected)
        {
            yield return new WaitUntil(() => selected.GetComponent<PlaceCard>().isThumbnailLoaded);
            Sprite sprite = selected.GetComponent<PlaceCard>().thumbnail.GetComponent<Image>().sprite;
            backgroundImage.GetComponent<Image>().sprite = sprite;
        }

        public void OnPlaceDataUpdate(SocketIOEvent e)
        {
            UpdatePlace(e.data.GetField("place"));
        }

        void UpdatePlace(JSONObject placeData)
        {
            Place place = new Place(placeData);
            PlaceCard card;
            if (!places.ContainsKey(place.id)) card = Instantiate(placeCard, content.transform).GetComponent<PlaceCard>();
            else card = places[place.id];
            card.Initialize(place);
            places[place.id] = card;
        }
    }

}
