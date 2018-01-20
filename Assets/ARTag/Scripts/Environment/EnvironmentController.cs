using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.HelloAR;

public class EnvironmentController : Publisher
{

#region Camera
    public Camera firstPersonCamera;
#endregion

#region Prototype Object
    public GameObject trackedPlanePrefab;
#endregion

#region Planes
    List<TrackedPlane> newPlanes;
    List<TrackedPlane> allPlanes;
#endregion

#region Status
    bool previousSearchingStatus;
    #endregion

#region Constants
    const int SLEEP_TRACKING_TIMEOUT = 15;
    #endregion

#region Unity
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
            CheckHittingPlanesStatus();
        }
        else
        {
            SetLostTrackingTimeout();
        }
    }
#endregion

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

#region Hitting Plane Status
    bool IsTouch(Touch touch)
    {
        return Input.touchCount < 1 || touch.phase != TouchPhase.Began;
    }

    void CheckHittingPlanesStatus()
    {
        Touch touch = Input.GetTouch(0);
        if (IsTouch(touch))
        {
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;

            if (Session.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                Broadcast("OnHitPlanes", hit);
            }
        }
    }
#endregion

}
