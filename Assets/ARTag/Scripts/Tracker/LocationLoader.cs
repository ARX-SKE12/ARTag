using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wikitude;
public class LocationLoader : MonoBehaviour {
    public GameObject wikitudeObject, arCoreObject, refPoint, wikiCam;
    ImageTarget recognizedTarget = null;
    bool isTracking;
	// Use this for initialization
	void Start () {
        LogClient.instance.Log("Status", "Init");
	}
	
	// Update is called once per frame
	void Update () {
		if (isTracking)
        {
            LogClient.instance.Log("Location Name", recognizedTarget.Name);
            LogClient.instance.Log("Location ID", recognizedTarget.ID.ToString());
            LogClient.instance.Log("Distance", recognizedTarget.ComputeCameraDistanceToTarget().ToString());
            LogClient.instance.Log("Diff", (wikiCam.transform.position - refPoint.transform.position).ToString());
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
