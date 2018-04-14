
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EditorModeController : MonoBehaviour
    {
        public GameObject planeMode, tagMode;

        // Use this for initialization
        void Start()
        {
            GameObject.FindObjectOfType<Calibration>().Register(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnFinishCalibration()
        {
            planeMode.SetActive(true);
        }
    }

}
