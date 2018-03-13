using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
using ARCoreToolkit;
namespace ARTag
{

    public class TrackingSwitch : MonoBehaviour
    {

        public Sprite pauseIcon, trackingIcon;

        public void ActionTrackingStatus()
        {
            GameObject.FindObjectOfType<PlaneController>().ChangePlaneTrackingState();
            if (!GameObject.FindObjectOfType<ARCoreSession>().SessionConfig.EnablePlaneFinding) GetComponentInChildren<Image>().sprite = pauseIcon;
            else GetComponentInChildren<Image>().sprite = trackingIcon;
        }
    }

}