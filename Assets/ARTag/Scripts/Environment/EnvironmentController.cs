using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.HelloAR;

public class EnvironmentController : MonoBehaviour {

    public Camera firstPersonCamera;
    public GameObject trackedPlanePrefab;

    List<TrackedPlane> newPlanes;
    List<TrackedPlane> allPlanes;

    void Awake()
    {
        newPlanes = new List<TrackedPlane>();
        allPlanes = new List<TrackedPlane>();
    }
	
	// Update is called once per frame
	void Update () {
        EnvironmentUpdate();
    }

    void GetNewPlanes()
    {
        Frame.GetPlanes(newPlanes, TrackableQueryFilter.New);
    }

    void CreateNewPlane(TrackedPlane plane)
    {
        GameObject planeObject = Instantiate(trackedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
        planeObject.GetComponent<TrackedPlaneVisualizer>().Initialize(plane);
    }

    void EnvironmentUpdate()
    {
        GetNewPlanes();
        foreach (TrackedPlane trackedPlane in newPlanes)
        {
            CreateNewPlane(trackedPlane);
        }
    }
}
