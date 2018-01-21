using UnityEngine;
using UnityEngine.UI;

public class TrackingSwitch : MonoBehaviour {

    public Sprite pauseIcon, trackingIcon;
    
    public GameObject environmentController;
	
	public void ActionTrackingStatus()
    {
        environmentController.GetComponent<EnvironmentController>().SwitchStatus();
        if (!environmentController.GetComponent<EnvironmentController>().isPause) GetComponentInChildren<Image>().sprite = pauseIcon;
        else GetComponentInChildren<Image>().sprite = trackingIcon;
    }
}
