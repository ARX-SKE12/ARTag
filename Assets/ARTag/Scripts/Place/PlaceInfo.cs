
namespace ARTag
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlaceInfo : MonoBehaviour
    {
        public GameObject thumbnail, nameText, authorText, descriptionText, background;
        Place place;

        const string baseUrl = "https://storage.googleapis.com/artag-thumbnail/";

        // Use this for initialization
        void Start()
        {
            place = (Place) GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace");
            nameText.GetComponent<Text>().text = place.name;
            authorText.GetComponent<Text>().text = place.user.name;
            descriptionText.GetComponent<Text>().text = place.description;
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
    }

}
