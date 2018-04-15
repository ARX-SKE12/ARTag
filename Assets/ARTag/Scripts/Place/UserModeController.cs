﻿
namespace ARTag
{
    using UnityEngine;

    public class UserModeController : MonoBehaviour
    {
        public GameObject planeSpawner;

        // Use this for initialization
        void Start()
        {
            GameObject.FindObjectOfType<Calibration>().Register(gameObject);
        }

        void OnFinishCalibration()
        {
            planeSpawner.SetActive(true);
            planeSpawner.GetComponent<ServerPlaneGenerator>().GeneratePlane();
        }
    }

}
