using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.HelloAR;

public class EnvironmentController : Publisher
{

    public Camera firstPersonCamera;

    public GameObject trackedPlanePrefab;

    List<TrackedPlane> newPlanes;
    List<TrackedPlane> allPlanes;

    bool previousSearchingStatus;

    const int SLEEP_TRACKING_TIMEOUT = 15;

    void Awake()
    {
        newPlanes = new List<TrackedPlane>();
        allPlanes = new List<TrackedPlane>();
    }
	
	// Update is called once per frame
	void Update () {
        if (IsTracking())
        {
            PreventSleep();
            EnvironmentUpdate();
            CheckSearchingPlanesStatus();
        }
        else
        {
            SetLostTrackingTimeout();
        }
    }

#region Plane Construct
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
    #endregion

#region Tracking Status
    bool IsTracking()
    {
        return Frame.TrackingState == TrackingState.Tracking;
    }

    void SetLostTrackingTimeout()
    {
        Screen.sleepTimeout = SLEEP_TRACKING_TIMEOUT;
    }

    void PreventSleep()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    #endregion

#region Plane Searching Status
    bool IsPlaneInTrackingState(TrackedPlane plane)
    {
        return plane.TrackingState == TrackingState.Tracking;
    }

    void UpdateNewSearchingStatus(bool newSearchingStatus)
    {
        Broadcast("OnSearchingStateChange", newSearchingStatus);
        previousSearchingStatus = newSearchingStatus;
    }

    void CheckSearchingPlanesStatus()
    {
        Frame.GetPlanes(allPlanes);
        bool isSearching = true;
        foreach (TrackedPlane trackedPlane in allPlanes)
        {
            if (IsPlaneInTrackingState(trackedPlane))
            {
                isSearching = false;
                break;
            }
        }
        if (isSearching != previousSearchingStatus)
            UpdateNewSearchingStatus(isSearching);
    }
#endregion



}
