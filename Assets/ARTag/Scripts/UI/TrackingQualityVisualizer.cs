using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace ARTag
{

    public class TrackingQualityVisualizer : MonoBehaviour
    {

        public GameObject locationController, camera;

        void Start()
        {
            locationController.GetComponent<LocationDetector>().Register(gameObject);
        }

        void OnQualityTracking(int progress)
        {
            GetComponent<Text>().text = progress.ToString() + " " + camera.transform.position;
        }

        void OnQualityTrackingFinish(Location location)
        {
            GetComponent<Text>().text = location.target.Name + " " + location.position.ToString();
            PlayerPrefs.SetFloat("x", location.position.x);
            PlayerPrefs.SetFloat("y", location.position.y);
            PlayerPrefs.SetFloat("z", location.position.z);
            SceneManager.LoadScene("ARCore");
        }

        void OnQualityTrackingLost()
        {
            GetComponent<Text>().text = "Lost";
        }
    }

}