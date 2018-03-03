using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ARTag
{

    public class ModeSwitch : MonoBehaviour
    {

        enum Mode
        {
            TRACKING,
            TAGGING
        }

        Mode currentMode;

        public GameObject trackingPanel, taggingPanel, environmentController;

        public Sprite trackingIcon, taggingIcon;

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
                    GetComponent<Image>().sprite = trackingIcon;
                    taggingPanel.SetActive(true);
                    trackingPanel.SetActive(false);
                    break;
                case Mode.TAGGING:
                    currentMode = Mode.TRACKING;
                    GetComponent<Image>().sprite = taggingIcon;
                    taggingPanel.SetActive(false);
                    trackingPanel.SetActive(true);
                    break;
            }
        }

    }

}