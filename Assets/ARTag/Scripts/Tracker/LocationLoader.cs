using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wikitude;
public class LocationLoader : MonoBehaviour {
    public GameObject wikitudeObject, arCoreObject;
	// Use this for initialization
	void Start () {
        LogClient.instance.Log("Status", "Init");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnImageRecognized(ImageTarget recognizedTarget)
    {
        LogClient.instance.Log("Location Name", recognizedTarget.Name);
        LogClient.instance.Log("Location ID", recognizedTarget.ID.ToString());
    }
}
