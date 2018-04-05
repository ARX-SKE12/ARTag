
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;
    using JustAQRScanner;

    public class ReadSignificantQR : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GameObject.FindObjectOfType<QRReader>().Register(gameObject);
        }

        void OnQRDetect(string result)
        {
            GetComponent<Text>().text = result;
        }
    }

}
