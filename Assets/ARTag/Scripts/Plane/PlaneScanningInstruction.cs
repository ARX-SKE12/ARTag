
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlaneScanningInstruction : MonoBehaviour
    {

        public GameObject thumbnail, instructionText, changeStepButton, actionBar, modeBar;
        public Sprite finishButtonSprite;
        public Sprite[] stepThumbnail;
        int step = 0;

        public void NextStep()
        {
            switch (++step)
            {
                case 1:
                    instructionText.GetComponent<Text>().text = "Pause Room Scanning Mode";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[0];
                    break;
                case 2:
                    instructionText.GetComponent<Text>().text = "Upload Scanned Room to Server";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[1];
                    changeStepButton.GetComponent<Image>().sprite = finishButtonSprite;
                    changeStepButton.GetComponentInChildren<Text>().text = "I got it!";
                    break;
                case 3:
                    actionBar.SetActive(true);
                    modeBar.SetActive(true);
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

}
