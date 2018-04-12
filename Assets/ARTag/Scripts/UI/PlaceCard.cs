
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlaceCard : MonoBehaviour
    {
        public GameObject thumbnail, nameText, authorText;
        public Place place;
        const string baseUrl = "https://storage.googleapis.com/artag-thumbnail/";

        public void Initialize(Place place)
        {
            this.place = place;
            nameText.GetComponent<Text>().text = place.name;
            authorText.GetComponent<Text>().text = place.user.name;
            StartCoroutine(LoadThumbnail(baseUrl + place.timestamp + "-" + place.name + ".png"));
        }

        IEnumerator LoadThumbnail(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            Texture2D thumbnailImage = www.texture;
            thumbnail.GetComponent<Image>().sprite = Sprite.Create(thumbnailImage, new Rect(0, 0, thumbnailImage.width, thumbnailImage.height), new Vector2(0.5f, 0.5f), 100);
        }
    }

}
