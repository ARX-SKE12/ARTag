﻿
namespace ARTag {

    using UnityEngine;
    using GoogleARCore;
    using PublisherKit;

    public class Calibrator : Publisher
    {
        public Vector3 currentPosition;
        public Quaternion currentRotation;
        public Vector3 offsetPosition = Vector3.zero;
        public Quaternion offsetRotation = Quaternion.identity;

        public Vector3 refPointPosition = Vector3.zero;
        public Quaternion refPointRotation = Quaternion.identity;

        bool isCalibrated;

        // Update is called once per frame
        void Update()
        {
            currentPosition = Camera.main.transform.position;
            currentRotation = Camera.main.transform.rotation;
            if (isCalibrated)
            {
                currentPosition = offsetPosition - Camera.main.transform.position;
                currentRotation = offsetRotation * Quaternion.Inverse(Camera.main.transform.rotation);
            }
            else
            {
                Touch touch;
                if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

                if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                {
                    offsetPosition = hit.Pose.position;
                    offsetRotation = hit.Pose.rotation;
                    refPointRotation = hit.Pose.rotation;
                    refPointPosition = hit.Pose.position;
                    isCalibrated = true;
                    Broadcast("OnFinishCalibration");
                }
            }

        }
        
        public void Recalibrate()
        {
            isCalibrated = false;
        }

        public Vector3 GetVirtualPosition(Vector3 position)
        {
            return position - offsetPosition;
        }

        public Quaternion GetVirtualRotation(Quaternion rotation)
        {
            Vector3 inv = rotation.eulerAngles * -1;
            return Quaternion.Euler(inv.x,inv.y, inv.z);
        }
        
    }

}
