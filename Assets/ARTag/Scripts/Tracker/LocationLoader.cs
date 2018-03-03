using LoggingClient;
using UnityEngine;
using Wikitude;
public class LocationLoader : MonoBehaviour {
    public GameObject wikitudeObject, arCoreObject, refPoint, wikiCam;
    ImageTarget recognizedTarget = null;
    bool isTracking;
    Vector3 avg = Vector3.zero;
    int numQualityTracking = 0;

	// Use this for initialization
	void Start () {
        LogClient.instance.Log("Status", "Init");
	}
	
	// Update is called once per frame
	void Update () {
		if (isTracking)
        {
            Vector3 camPos = wikiCam.transform.position;
            if (avg == Vector3.zero) avg = camPos;
            else if (numQualityTracking >= 10)
            {
                LogClient.instance.Log("Quality", numQualityTracking.ToString());
                isTracking = false;
                wikiCam.SetActive(false);
                wikitudeObject.SetActive(false);
                arCoreObject.SetActive(true);
                arCoreObject.transform.GetChild(0).position = avg;
            }
            else
            {
                LogClient.instance.Log("AVG", avg.ToString());
                LogClient.instance.Log("DIFF", (avg - camPos).ToString());
                Vector3 diff = avg - camPos;
                avg = (avg + camPos) / 2;
                if (Mathf.Abs(diff.x) <= 0.1 && Mathf.Abs(diff.y) <= 0.1 && Mathf.Abs(diff.z) <= 0.1)
                {
                    numQualityTracking++;
                }
                else
                {
                    numQualityTracking = 0;
                }
            }
            /*LogClient.instance.Log("Location Name", recognizedTarget.Name);
            LogClient.instance.Log("Location ID", recognizedTarget.ID.ToString());
            LogClient.instance.Log("Distance", recognizedTarget.ComputeCameraDistanceToTarget().ToString());
            LogClient.instance.Log("Ref", ((refPoint.transform.position)).ToString());
            LogClient.instance.Log("Cam", ((wikiCam.transform.position)).ToString());*/
        }else
        {
            LogClient.instance.Log("ARCore", arCoreObject.transform.position.ToString());
            LogClient.instance.Log("ARCoreCam", arCoreObject.transform.GetChild(0).position.ToString());
            arCoreObject.transform.GetChild(0).position = avg;
        }
	}

    public void OnImageRecognized(ImageTarget recognizedTarget)
    {
        if (recognizedTarget.ID!=0)
        {
            isTracking = true;
            this.recognizedTarget = recognizedTarget;
        }
    }

    public void OnImageLost()
    {
        isTracking = false;
        recognizedTarget = null;
    }
}
