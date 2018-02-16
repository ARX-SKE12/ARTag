using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wikitude;

public class LocationDetector : Publisher {

#region Attributes
    public GameObject wikitudeCam;

    ImageTarget recognizedTarget;
    int qualityCounter;
    Vector3 avgPosition;
    #endregion

#region Constants
    const int MAXIMUM_QUALITY = 100;
    #endregion

#region Unity Behaviours
    // Use this for initialization
    void Start () {
        ResetQualityCounter();
	}
	
	// Update is called once per frame
	void Update () {
        TrackQuality();
	}
    #endregion

#region Tracking
    void OnTargetRecognized(ImageTarget target)
    {
        if (ShouldTracking(target)) recognizedTarget = target;
    }

    void OnTargetLost()
    {
        recognizedTarget = null;
    }

    bool ShouldTracking(ImageTarget target)
    {
        return target != null || target.ID != 0;
    }
    #endregion

#region Quality Tracking
    void ResetQualityCounter()
    {
        qualityCounter = 0;
        avgPosition = Vector3.zero;
        Broadcast("OnQualityTrackingLost");
    }

    void TrackQuality()
    {
        if (ShouldTracking(recognizedTarget))
        {
            if (ShouldQualityTrackingFinish()) Broadcast("OnQualityTrackingFinish", new Location(recognizedTarget, avgPosition));
            else CalculateQuality();
        }
        else ResetQualityCounter();
    }

    void CalculateQuality()
    {
        Vector3 wikitudeCamPosition = wikitudeCam.transform.position;
        if (IsFirstTimeTracking()) avgPosition = wikitudeCamPosition;
        Vector3 diff = avgPosition - wikitudeCamPosition;
        if (IsQuality(diff))
        {
            avgPosition = (avgPosition + wikitudeCamPosition) / 2;
            qualityCounter++;
            Broadcast("OnQualityTracking", qualityCounter);
        }
        else ResetQualityCounter();
    }

    bool IsFirstTimeTracking()
    {
        return qualityCounter == 0;
    }

    bool ShouldQualityTrackingFinish()
    {
        return qualityCounter >= MAXIMUM_QUALITY;
    }

    bool IsQuality(Vector3 diff)
    {
        return Mathf.Abs(diff.x) <= 0.1f && Mathf.Abs(diff.y) <= 0.1f && Mathf.Abs(diff.z) <= 0.1f;
    }

#endregion

}
