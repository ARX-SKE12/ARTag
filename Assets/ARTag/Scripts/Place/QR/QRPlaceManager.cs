
namespace ARTag
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class QRPlaceManager : MonoBehaviour
    {

        public GameObject qrcodeImage, notification;
        Texture2D source;
        string significant;
        const string BASE_URL = "https://storage.googleapis.com/artag-qr/";

        // Use this for initialization
        void Start()
        {
            significant = (string) GameObject.FindObjectOfType<TemporaryDataManager>().Get("significant");
            StartCoroutine(LoadQRToImage());
        }

        IEnumerator LoadQRToImage()
        {
            string url = BASE_URL + significant + ".png";
            WWW request = new WWW(url);
            yield return request;
            source = request.texture;
            qrcodeImage.GetComponent<Image>().sprite = Sprite.Create(source, new Rect(0, 0, source.width, source.height), Vector2.zero);
            qrcodeImage.SetActive(true);
        }

        public void SaveQR()
        {
            byte[] bytes = source.EncodeToPNG();
            string fileName = significant + ".png";
            NativeGallery.SaveImageToGallery(bytes, Application.productName, fileName);
            notification.GetComponentInChildren<Text>().text = "QR code is saved in your gallery!";
            notification.SetActive(true);
        }
    }

}
