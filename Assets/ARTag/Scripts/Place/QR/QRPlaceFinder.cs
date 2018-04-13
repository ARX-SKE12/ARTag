
namespace ARTag
{
    using UnityEngine;
    using JustAQRScanner;

    public class QRPlaceFinder : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GameObject.FindObjectOfType<QRReader>().Register(gameObject);
        }

        void OnQRDetect(string result)
        {
            Debug.Log(result);
        }
    }

}
