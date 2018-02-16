using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackingQualityVisualizer : MonoBehaviour {

    public GameObject locationController;

    void Start()
    {
        locationController.GetComponent<LocationDetector>().Register(gameObject);
    }

    void OnQualityTracking(int progress)
    {
        GetComponent<Text>().text = progress.ToString();
    }

    void OnQualityTrackingFinish(Location location)
    {
        GetComponent<Text>().text = location.target.Name + " " + location.position.ToString();
        PlayerPrefs.SetFloat("x", location.position.x);
        PlayerPrefs.SetFloat("y", location.position.y);
        PlayerPrefs.SetFloat("z", location.position.z);
    }

    void OnQualityTrackingLost()
    {
        GetComponent<Text>().text = "Lost";
    }
}
