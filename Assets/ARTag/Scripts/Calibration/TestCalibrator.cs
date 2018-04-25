using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class TestCalibrator : MonoBehaviour {

    Vector3 refPosition, refRotation;
    bool isCalibrated;
	// Use this for i nitialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isCalibrated)
        {
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                refPosition = hit.Pose.position;
                refRotation = hit.Pose.rotation.eulerAngles;
                isCalibrated = true;
            }
        
        }else
        {
            Vector3 realPos = TransformPosition(Camera.main.transform.position, refPosition, refRotation);
            string text = refPosition.ToString() + "\n" + refRotation.ToString() + "\n" + realPos + "\n" + TransformPosition(realPos, Vector3.zero, TransformRotation(Quaternion.identity.eulerAngles, refRotation))+"\n"+Camera.main.transform.position.ToString();
            GameObject.FindObjectOfType<Text>().text = text;
        }
    }

    Vector3 TransformPosition(Vector3 obj, Vector3 pos, Vector3 rot)
    {
        Vector3 ori = obj - pos;

        float x = ori.x;
        float y = ori.y;
        float z = ori.z;

        float a = rot.x;
        float b = rot.y;
        float g = rot.z;

        float xp = x * Mathf.Cos(g) * Mathf.Cos(b) - y * Mathf.Sin(g) * Mathf.Cos(a) + y * Mathf.Cos(g) * Mathf.Sin(b) * Mathf.Sin(a) + z * Mathf.Sin(g) * Mathf.Sin(a) + z * Mathf.Cos(g) * Mathf.Sin(b) * Mathf.Cos(a);
        float yp = x * Mathf.Sin(g) * Mathf.Cos(b) + y * Mathf.Cos(g) * Mathf.Cos(a) + y * Mathf.Sin(g) * Mathf.Sin(b) * Mathf.Sin(a) - z * Mathf.Cos(g) * Mathf.Sin(a) + z * Mathf.Sin(g) * Mathf.Sin(b) * Mathf.Cos(a);
        float zp = -1 * x * Mathf.Sin(b) + y * Mathf.Cos(b) * Mathf.Sin(a) + z * Mathf.Cos(b) * Mathf.Cos(a);
        return new Vector3(xp, yp, zp);
    }

    Vector3 TransformRotation(Vector3 rot, Vector3 refRot)
    {
        return rot - refRot;
    }
}
