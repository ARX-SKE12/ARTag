using UnityEngine;
using UnityEngine.UI;

public class TrackingSwitch : MonoBehaviour {

    const string CONTINUE_TRACKING_LABEL = "Continue Tracking";
    const string STOP_TRACKING_LABEL = "Stop Tracking";

    public GameObject environmentController;
	
	public void ActionTrackingStatus()
    {
        environmentController.GetComponent<EnvironmentController>().SwitchStatus();
        if (!environmentController.GetComponent<EnvironmentController>().isPause) GetComponentInChildren<Text>().text = STOP_TRACKING_LABEL;
        else GetComponentInChildren<Text>().text = CONTINUE_TRACKING_LABEL;
    }
}
