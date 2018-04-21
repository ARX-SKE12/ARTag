
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ImageTagBehaviour : TagBehaviour
    {
        public GameObject thumbnail;
        const string BASE_URL = "https://storage.googleapis.com/artag-content/";

        protected override JSONObject PrepareTagData(Dictionary<string, object> data)
        {
            JSONObject jsonData = base.PrepareTagData(data);
            JSONObject detail = jsonData.GetField("detail");
            JSONObject image = new JSONObject();
            ImageData imageData = (ImageData)data["image"];
            image.SetField("height", imageData.height);
            image.SetField("width", imageData.width);
            image.AddField("image", imageData.data);
            detail.SetField("image", image);
            return jsonData;
        }

        public override void ConstructTag(JSONObject data)
        {
            base.ConstructTag(data);
            StartCoroutine(FetchThumbnail(BASE_URL + GetTagData(data).GetField("timestamp").str + ".png"));
            title.SetActive(false);
        }

        IEnumerator FetchThumbnail(string url)
        {
            WWW request = new WWW(url);
            yield return request;
            Texture2D source = request.texture;
            thumbnail.GetComponent<Image>().sprite = Sprite.Create(source, new Rect(0, 0, source.width, source.height), Vector2.zero);
        }
    }

}
