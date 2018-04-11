using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class Calibration : MonoBehaviour {

    public Vector3 currentPosition;
    public Quaternion currentRotation;
    Vector3 offsetPosition = Vector3.zero;
    Quaternion offsetRotation = Quaternion.identity;
    bool isTracked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentPosition = Camera.main.transform.position;
        currentRotation = Camera.main.transform.rotation;
        if (isTracked)
        {
            currentPosition = offsetPosition - Camera.main.transform.position;
            currentRotation = offsetRotation * Quaternion.Inverse(Camera.main.transform.rotation);
        }
        string pos = currentPosition.ToString() + "\n" + currentRotation.eulerAngles.ToString();

        GameObject.Find("Pos").GetComponent<Text>().text = pos;

        // If the player has not touched the screen, we are done with this update.
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // Raycast against the location the player touched to search for planes.
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            Quaternion decrease = hit.Pose.rotation * Quaternion.Inverse(Quaternion.Euler(0, 0, 90));
            //Vector3 diff = hit.Pose.position-Camera.main.transform.position;
            string stat = Camera.main.transform.rotation.eulerAngles.ToString()+"\n";
            stat += hit.Pose.rotation.eulerAngles.ToString()+"\n";
            stat += decrease.eulerAngles.ToString();
            //stat += diff.ToString();
            offsetPosition = hit.Pose.position;
            offsetRotation = hit.Pose.rotation * Quaternion.Inverse(Quaternion.Euler(0, 0, 90));
            isTracked = true;
            GameObject.Find("Status").GetComponent<Text>().text = stat;
        }
    }
}
