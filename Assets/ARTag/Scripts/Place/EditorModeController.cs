
namespace ARTag
{
    using GoogleARCore;
    using UnityEngine;

    public class EditorModeController : MonoBehaviour
    {
        public GameObject planeMode, tagMode;

        // Use this for initialization
        void Awake()
        {
            GameObject.FindObjectOfType<Calibration>().Register(gameObject);
            GameObject.FindObjectOfType<ARCoreSession>().SessionConfig.EnablePlaneFinding = false;
            GameObject.FindObjectOfType<ARCoreSession>().OnEnable();
        }

        void OnFinishCalibration()
        {
            planeMode.SetActive(true);
        }
    }

}
