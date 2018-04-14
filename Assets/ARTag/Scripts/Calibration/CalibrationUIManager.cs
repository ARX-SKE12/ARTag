
namespace ARTag
{
    using UnityEngine;

    public class CalibrationUIManager : MonoBehaviour
    {

        public GameObject calibrationCanvas;

        // Use this for initialization
        void Start()
        {
            GetComponent<Calibration>().Register(gameObject);
        }

        void OnFinishCalibration()
        {
            calibrationCanvas.SetActive(false);
        }

    }

}
