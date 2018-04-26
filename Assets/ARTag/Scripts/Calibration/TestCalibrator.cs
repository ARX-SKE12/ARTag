
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;
    using GoogleARCore;

    public class TestCalibrator : MonoBehaviour
    {

        Vector3 refPosition, refRotation;
        bool isCalibrated;

        // Update is called once per frame
        void Update()
        {
            if (!isCalibrated)
            {
                Touch touch;
                if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

                if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                {
                    refPosition = hit.Pose.position;
                    refRotation = hit.Pose.rotation.eulerAngles;
                    isCalibrated = true;
                }

            }
            else
            {
                Vector3 realPos = CoordinateUtils.TransformPosition(Camera.main.transform.position, refPosition, refRotation, true);
                Vector3 zero = CoordinateUtils.TransformPosition(Vector3.zero, refPosition, refRotation, true);
                string text = refPosition.ToString() + "\n" 
                                + refRotation + "\n" 
                                + realPos + "\n" 
                                + CoordinateUtils.TransformPosition(realPos, zero, refRotation) 
                                + "\n" + Camera.main.transform.position;
                GameObject.FindObjectOfType<Text>().text = text;
            }
        }

    }

}
