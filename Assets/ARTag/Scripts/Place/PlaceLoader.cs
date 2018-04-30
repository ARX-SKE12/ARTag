
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlaceLoader : MonoBehaviour
    {
        public GameObject background, placeName;
        string significant;
        Place place;

        const string BASE_URL = "https://storage.googleapis.com/artag-thumbnail/";

        // Use this for initialization
        void Start()
        {
            significant = (string)GameObject.FindObjectOfType<TemporaryDataManager>().Get("significant");
            place = (Place)GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace");
            if (placeName) placeName.GetComponent<Text>().text = place.name;
            StartCoroutine(LoadImage());
        }

        IEnumerator LoadImage()
        {
            string url = BASE_URL + significant + ".png";
            WWW request = new WWW(url);
            yield return request;
            Texture2D source = request.texture;
            background.GetComponent<Image>().sprite = Sprite.Create(source, new Rect(0, 0, source.width, source.height), Vector2.zero);
        }
    }

}
