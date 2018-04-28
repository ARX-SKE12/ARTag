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
        Vector3 position = TransformPosition(transform.position, refPoint.transform.position, refPoint.transform.rotation.eulerAngles, true);
        Vector3 revRotation = TransformRevRotation(Quaternion.identity, refPoint.transform.rotation);
        Vector3 zero = TransformPosition(Vector3.zero, refPoint.transform.position, refPoint.transform.rotation.eulerAngles, true);
        Vector3 rPos = TransformPosition(position, zero, refPoint.transform.rotation.eulerAngles);
        //Vector3 zero = TransformPosition(Vector3.zero, refPoint.transform.position, Quaternion.identity.eulerAngles);
        //Vector3 zero2 = TransformPosition(Vector3.zero, refPoint.transform.position, Quaternion.identity.eulerAngles);

        Debug.Log("OOri " + transform.position);
        Debug.Log("Pos " + position);
        Debug.Log("Zero " + zero);
        Debug.Log("Ori " + rPos);
        Debug.Log("PP " + TransformPosition(rPos, refPoint.transform.position, refPoint.transform.rotation.eulerAngles, true));
    }

    Vector3 TransformPosition(Vector3 obj, Vector3 pos, Vector3 rot, bool isInverse=false)
    {
        Vector3 ori = obj - pos;

        float x = ori.x;
        float y = ori.y;
        float z = ori.z;

        float a = Mathf.Deg2Rad * rot.x;
        float b = Mathf.Deg2Rad * rot.y;
        float g = Mathf.Deg2Rad * rot.z;

        if(isInverse)
        {
            a = -a;
            b = -b;
            g = -g;
        }

        float[,] rx = new float[,] {
            { 1, 0, 0 },
            { 0, Mathf.Cos(a), -Mathf.Sin(a) },
            { 0, Mathf.Sin(a), Mathf.Cos(a) }
        };
        float[,] ry = new float[,] {
            { Mathf.Cos(b), 0, Mathf.Sin(b) },
            { 0, 1, 0 },
            { -Mathf.Sin(b), 0, Mathf.Cos(b) }
        };
        float[,] rz = new float[,] {
            { Mathf.Cos(g), -Mathf.Sin(g), 0 },
            { Mathf.Sin(g), Mathf.Cos(g), 0 },
            { 0, 0, 1 }
        };
        
        if (!isInverse)
        {
            return MatrixMult(rx, MatrixMult(ry, MatrixMult(rz, ori)));
        } else
        {
            return MatrixMult(rz, MatrixMult(ry, MatrixMult(rx, ori)));
        }
    }

    Vector3 TransformRevRotation(Quaternion rot, Quaternion refRot)
    {
        return rot.eulerAngles - refRot.eulerAngles;
    }
    
    Vector3 MatrixMult(float[,] mat, Vector3 pos)
    {
        float[] fuck = new float[3];
        fuck[0] = pos.x;
        fuck[1] = pos.y;
        fuck[2] = pos.z;

        float[] o = new float[] { 0, 0, 0 };
        for(int r=0; r<3; r++)
        {
            o[r] = 0;
            for(int c=0; c<3; c++)
            {
                o[r] += mat[r, c] * fuck[c];
            }
        }

        return new Vector3(o[0], o[1], o[2]);
    }
}
