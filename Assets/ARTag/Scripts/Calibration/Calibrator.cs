
namespace ARTag {

    using UnityEngine;
    using GoogleARCore;
    using PublisherKit;

    public class Calibrator : Publisher
    {
        public GameObject calibrationCube, pointCloud;
        
        public Vector3 refPointPosition;
        public Quaternion refPointRotation;

        public enum CalibratorState
        {
            INSTRUCTING,
            TRACKING_MARKER,
            ADJUSTING_LOCATION,
            READY
        }

        CalibratorState state = CalibratorState.INSTRUCTING;

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case CalibratorState.TRACKING_MARKER:
                    Touch touch;
                    if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

                    TrackableHit hit;
                    TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

                    if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                    {
                        calibrationCube = Instantiate(calibrationCube, hit.Pose.position, hit.Pose.rotation);
                        calibrationCube.transform.LookAt(Camera.main.transform);
                        pointCloud.SetActive(false);
                        ChangeCalibrationState(CalibratorState.ADJUSTING_LOCATION);
                        Broadcast("OnFinishTrackingState");
                    }
                    break;
                case CalibratorState.ADJUSTING_LOCATION:
                    break;
                case CalibratorState.READY:
                    refPointPosition = calibrationCube.transform.localPosition;
                    refPointRotation = calibrationCube.transform.localRotation;
                    break;
            }
        }
        
        public void ChangeCalibrationState(CalibratorState state)
        {
            this.state = state;
        }

        public Vector3 GetRealWorldPosition(Vector3 position)
        {
            return CoordinateUtils.TransformPosition(position, refPointPosition, refPointRotation.eulerAngles, true);
        }

        public Vector3 GetVirtualPosition(Vector3 position)
        {
            Vector3 zero = CoordinateUtils.TransformPosition(Vector3.zero, refPointPosition, refPointRotation.eulerAngles, true);
            return CoordinateUtils.TransformPosition(position, zero, refPointRotation.eulerAngles);
        }

        public Quaternion GetRealWorldRotation(Quaternion rotation)
        {
            Vector3 eulerAngle = CoordinateUtils.TransformRevRotation(rotation, refPointRotation);
            return Quaternion.Euler(eulerAngle.x, eulerAngle.y, eulerAngle.z);
        }

        public Quaternion GetVirtualRotation(Quaternion rotation)
        {
            Vector3 eulerAngle = refPointRotation.eulerAngles + rotation.eulerAngles;//CoordinateUtils.TransformRevRotation(refPointRotation, rotation);
            return Quaternion.Euler(eulerAngle.x, eulerAngle.y, eulerAngle.z);
        }

        public void FinishLocationAdjustment()
        {
            Broadcast("OnFinishCalibration");
            ChangeCalibrationState(CalibratorState.READY);
            calibrationCube.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
