
namespace ARTag {

    using UnityEngine;
    using GoogleARCore;

    public class Calibration : MonoBehaviour
    {

        public Vector3 currentPosition;
        public Quaternion currentRotation;
        public Vector3 offsetPosition = Vector3.zero;
        public Quaternion offsetRotation = Quaternion.identity;
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
            } else
            {
                Touch touch;
                if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

                if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                {
                    offsetPosition = hit.Pose.position;
                    offsetRotation = hit.Pose.rotation * Quaternion.Inverse(Quaternion.Euler(0, 0, 90));
                    isCalibrated = true;
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
            return rotation * Quaternion.Inverse(offsetRotation);
        }
    }

}
