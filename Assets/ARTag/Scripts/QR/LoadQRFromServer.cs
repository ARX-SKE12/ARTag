
namespace ARTag
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class LoadQRFromServer : MonoBehaviour
    {

        public string QR_URL;
        public Texture2D source;

        // Use this for initialization
        IEnumerator Start()
        {
           // string significant = QR_URL + GameObject.FindObjectOfType<TemporaryDataManager>().Get("significant")+".png";
            string significant = "https://storage.googleapis.com/artag-qr/1522901313057-hdhde.png";
            WWW request = new WWW(significant);
            yield return request;
            source = request.texture;
            GetComponent<Image>().sprite = Sprite.Create(source, new Rect(0, 0, source.width, source.height), Vector2.zero);
        }
        
    }

}
