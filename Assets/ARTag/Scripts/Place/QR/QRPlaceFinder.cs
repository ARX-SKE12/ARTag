
namespace ARTag
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using JustAQRScanner;
    using SocketIOManager;
    using SocketIO;

    public class QRPlaceFinder : MonoBehaviour
    {
        SocketManager socketManager;
        public GameObject errorNotification;
        bool canSubmit = true;

        // Use this for initialization
        void Start()
        {
            GameObject.FindObjectOfType<QRReader>().Register(gameObject);
            socketManager = GameObject.FindObjectOfType<SocketManager>();
            socketManager.On(EventsCollector.PLACE_RESPONSE_SIGNIFICANT, OnLoadPlace);
            socketManager.On(EventsCollector.PLACE_ERROR_SIGNIFICANT, OnLoadError);
        }

        void OnQRDetect(string result)
        {
            if (canSubmit)
            {
                JSONObject data = new JSONObject();
                data.AddField("encodedSignificant", result);
                socketManager.Emit(EventsCollector.PLACE_RETRIEVE_SIGNIFICANT, data);
                canSubmit = false;
                StartCoroutine(DelaySubmitFrame());
            }
        }

        IEnumerator DelaySubmitFrame()
        {
            yield return new WaitForSeconds(1);
            canSubmit = true;
        }

        void OnLoadPlace(SocketIOEvent e)
        {
            Place place = new Place(e.data);
            string significant = place.timestamp + "-" + place.name;
            GameObject.FindObjectOfType<TemporaryDataManager>().Put("significant", significant);
            GameObject.FindObjectOfType<TemporaryDataManager>().Put("currentPlace", place);
            SceneManager.LoadScene("Detail Place");
        }

        void OnLoadError(SocketIOEvent e)
        {
            errorNotification.GetComponentInChildren<Text>().text = e.data.GetField("error").str;
            errorNotification.SetActive(true);
        }
    }

}
