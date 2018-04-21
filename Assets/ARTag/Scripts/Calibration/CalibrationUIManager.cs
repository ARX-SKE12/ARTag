
namespace ARTag
{
    using UnityEngine;

    public class CalibrationUIManager : MonoBehaviour
    {

        public GameObject calibrationCanvas, pointCloud;

        // Use this for initialization
        void Start()
        {
            GetComponent<Calibrator>().Register(gameObject);
        }

        void OnFinishCalibration()
        {
            calibrationCanvas.SetActive(false);
            pointCloud.SetActive(false);
        }

    }

}
