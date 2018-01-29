using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PositionController : MonoBehaviour {
    public GameObject positionText, targetCamera;
    bool isAppear;

    private void Update()
    {
        if (isAppear)
        {
            TargetAppear();
        }else
        {
            targetCamera.GetComponent<Text>().text = "Nothing";
        }
    }

    void TargetAppear()
    {
        positionText.GetComponent<Text>().text = targetCamera.transform.position.ToString();
    }

    public void OnAppear()
    {
        isAppear = true;
    }

    public void OnHidden()
    {
        isAppear = false;
    }

}
