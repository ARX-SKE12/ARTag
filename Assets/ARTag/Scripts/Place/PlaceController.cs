
namespace ARTag
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using SocketIO;
    using SocketIOManager;

    public class PlaceController : MonoBehaviour
    {
        public GameObject thumbnail, nameText, authorText, descriptionText, background, moreButton;
        Place place;

        const string baseUrl = "https://storage.googleapis.com/artag-thumbnail/";

        // Use this for initialization
        void Start()
        {
            GameObject.FindObjectOfType<SocketManager>().On(EventsCollector.PLACE_DATA_UPDATE, OnPlaceUpdate);
            LoadPlace();
        }

        void LoadPlace()
        {
            place = (Place)GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace");
            nameText.GetComponent<Text>().text = place.name;
            authorText.GetComponent<Text>().text = place.user.name;
            descriptionText.GetComponent<Text>().text = place.description;
            if (PlayerPrefs.GetString("id") != place.user.id) moreButton.SetActive(false);
            StartCoroutine(LoadThumbnail(baseUrl + place.timestamp + "-" + place.name + ".png"));
        }

        IEnumerator LoadThumbnail(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            Texture2D thumbnailImage = www.texture;
            Sprite sprite = Sprite.Create(thumbnailImage, new Rect(0, 0, thumbnailImage.width, thumbnailImage.height), new Vector2(0.5f, 0.5f), 100);
            thumbnail.GetComponent<Image>().sprite = sprite;
            background.GetComponent<Image>().sprite = sprite;
        }

        public void OnPlaceUpdate(SocketIOEvent e)
        {
            JSONObject placeData = e.data.GetField("place");
            Place place = new Place(placeData);
            if (place.id==this.place.id)
            {
                GameObject.FindObjectOfType<TemporaryDataManager>().Put("currentPlace", place);
                LoadPlace();
            }
        }
        
    }

}
