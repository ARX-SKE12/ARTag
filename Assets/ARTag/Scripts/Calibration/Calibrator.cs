
namespace ARTag {

    using UnityEngine;
    using GoogleARCore;
    using PublisherKit;

    public class Calibrator : Publisher
    {
        public GameObject calibrationCube;
        
        public Vector3 refPointPosition = Vector3.zero;
        public Quaternion refPointRotation = Quaternion.identity;

        bool isCalibrated;

        void Start()
        {
            GameObject.FindObjectOfType<ARCoreSession>().OnDestroy();
            GameObject.FindObjectOfType<ARCoreSession>().Start();
            GameObject.FindObjectOfType<ARCoreSession>().OnEnable();
        }

        // Update is called once per frame
        void Update()
        {
            if (isCalibrated)
            {
                
            }
            else
            {
                Touch touch;
                if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

                if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                {
                    calibrationCube = Instantiate(calibrationCube, hit.Pose.position, hit.Pose.rotation);
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
            Vector3 eulerAngle = CoordinateUtils.TransformRevRotation(refPointRotation, rotation);
            return Quaternion.Euler(eulerAngle.x, eulerAngle.y, eulerAngle.z);
        }

    }

}
