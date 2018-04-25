using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockCalibration : MonoBehaviour {

    public GameObject refPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = TransformPosition(transform.position, refPoint.transform.position, refPoint.transform.rotation.eulerAngles);
        Vector3 zero = TransformPosition(Vector3.zero, refPoint.transform.position, refPoint.transform.rotation.eulerAngles);

        Debug.Log("Pos " + position);
        Debug.Log("Zero " + zero);
        Debug.Log("Ori " + TransformPosition(position, zero, TransformRotation(transform.rotation, refPoint.transform.rotation)));
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

    Vector3 TransformRotation(Quaternion rot, Quaternion refRot)
    {
        return rot.eulerAngles - refRot.eulerAngles;
    }
    
}
