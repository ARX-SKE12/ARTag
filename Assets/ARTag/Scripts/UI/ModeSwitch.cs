using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSwitch : MonoBehaviour {

    enum Mode
    {
        TRACKING,
        TAGGING
    }

    Mode currentMode;

    public GameObject trackingPanel, taggingPanel, environmentController;

    const string TAGGING_LABEL = "Tag";
    const string TRACKKING_LABEL = "Scan";

    void Awake()
    {
        currentMode = Mode.TRACKING;
    }

    public void SwitchMode()
    {
        switch (currentMode)
        {
            case Mode.TRACKING:
                currentMode = Mode.TAGGING;
                GetComponentInChildren<Text>().text = TRACKKING_LABEL;
                taggingPanel.SetActive(true);
                trackingPanel.SetActive(false);
                break;
            case Mode.TAGGING:
                currentMode = Mode.TRACKING;
                GetComponentInChildren<Text>().text = TAGGING_LABEL;
                taggingPanel.SetActive(false);
                trackingPanel.SetActive(true);
                break;
        }
    }

}
