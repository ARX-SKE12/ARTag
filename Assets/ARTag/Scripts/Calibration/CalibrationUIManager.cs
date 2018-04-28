
namespace ARTag
{
    using UnityEngine;

    public class CalibrationUIManager : MonoBehaviour
    {

        public GameObject calibrationCanvas, pointCloud, scanUI, adjustUI;

        // Use this for initialization
        void Start()
        {
            GetComponent<Calibrator>().Register(gameObject);
        }

        void OnFinishCalibration()
        {
            adjustUI.SetActive(false);
            calibrationCanvas.SetActive(false);
        }

        void OnFinishTrackingState()
        {
            pointCloud.SetActive(false);
            scanUI.SetActive(false);
            adjustUI.SetActive(true);
        }

    }

}
