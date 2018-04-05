
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;
    using Wikitude;

    public class CloudStatusReporter : MonoBehaviour
    {
        double recognitionInterval = 1;

        public void OnInitialized()
        {
            GameObject.Find(ObjectsCollector.CLOUD_STATUS_TEXT).GetComponent<Text>().text = "Cloud Initialized!";
            GetComponent<ImageTracker>().CloudRecognitionService.StartContinuousRecognition(recognitionInterval);
        }

        public void OnError(int code, string err)
        {
            GameObject.Find(ObjectsCollector.CLOUD_STATUS_TEXT).GetComponent<Text>().text = code+" "+err;
        }

        public void OnRecognitionResponse(CloudRecognitionServiceResponse response)
        {
            if (response.Recognized)
            {
                GameObject.Find(ObjectsCollector.CLOUD_STATUS_TEXT).GetComponent<Text>().text = response.Info["name"];
            }
            else
            {
                GameObject.Find(ObjectsCollector.CLOUD_STATUS_TEXT).GetComponent<Text>().text = "No target recognized";
            }
        }

        public void OnInterruption(double suggestedInterval)
        {
            recognitionInterval = suggestedInterval;
            GetComponent<ImageTracker>().CloudRecognitionService.StartContinuousRecognition(recognitionInterval);
        }
    }

}
