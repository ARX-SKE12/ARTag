
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;
    using GoogleARCore;

    public class TestCalibrator : MonoBehaviour
    {
        public GameObject cube, calibCube;
        Gyroscope gyro;
        Vector3 refPosition, refRotation;
        bool isCalibrated, shouldRotate;
        int step = 0;
        int sign;
        // Update is called once per frame
        void Update()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            gyro = Input.gyro;
            gyro.enabled = true;
            if (!isCalibrated)
            {
                Touch touch;
                if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

                if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                {
                    refPosition = hit.Pose.position;
                    refRotation = hit.Pose.rotation.eulerAngles*-1;
                    calibCube = Instantiate(cube, refPosition, Quaternion.Euler(refRotation.x, refRotation.y, refRotation.z));
                    /*cube.transform.localPosition = refPosition;
                    cube.transform.localRotation = Quaternion.Euler(refRotation.x, refRotation.y, refRotation.z);*/
                    isCalibrated = true;
                }

            }
            else
            {
                refPosition = calibCube.transform.localPosition;
                refRotation = calibCube.transform.localRotation.eulerAngles;
                Vector3 realPos = CoordinateUtils.TransformPosition(Camera.main.transform.position, refPosition, refRotation, true);
                Vector3 zero = CoordinateUtils.TransformPosition(Vector3.zero, refPosition, refRotation, true);
                string text = "\nPos :" + realPos
                                + "\nAc :" + Input.acceleration * 360
                                + "\nCam :" + Camera.main.transform.rotation.eulerAngles
                                + "\nDiff :" + (gyro.attitude.eulerAngles - Camera.main.transform.rotation.eulerAngles);
                GameObject.FindObjectOfType<Text>().text = text;
                if (shouldRotate) Rotate();
            }
        }

        public void Rotate()
        {
            float x = calibCube.transform.localRotation.eulerAngles.x;
            float y = calibCube.transform.localRotation.eulerAngles.y;
            float z = calibCube.transform.localRotation.eulerAngles.z;
            switch(step)
            {
                case 0:
                    x += sign;
                    break;
                case 1:
                    y += sign;
                    break;
                case 2:
                    z += sign;
                    break;
                default:
                    break;
            }
            calibCube.transform.localRotation = Quaternion.Euler(x, y, z);
        }

        public void StartRotate(int sign)
        {
            this.sign = sign;
            shouldRotate = true;
        }

        public void StopRotate()
        {
            shouldRotate = false;
        }

        public void ChangeMode()
        {
            step++;
        }

        public void Back()
        {
            step--;
        }
    }

}
