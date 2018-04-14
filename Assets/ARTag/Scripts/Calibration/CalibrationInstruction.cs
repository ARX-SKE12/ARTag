
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;

    public class CalibrationInstruction : MonoBehaviour
    {
        public GameObject thumbnail, instructionText, changeStepButton, pointCloud, scanUI, calibrator;
        public Sprite finishButtonSprite;
        public Sprite[] stepThumbnail;
        int step = 0;

        public void NextStep()
        {
            switch (++step)
            {
                case 1:
                    instructionText.GetComponent<Text>().text = "Feature Point is shown on the marker";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[0];
                    break;
                case 2:
                    instructionText.GetComponent<Text>().text = "Tap On Center of Marker!";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[1];
                    changeStepButton.GetComponent<Image>().sprite = finishButtonSprite;
                    changeStepButton.GetComponentInChildren<Text>().text = "I got it!";
                    break;
                case 3:
                    pointCloud.SetActive(true);
                    scanUI.SetActive(true);
                    calibrator.GetComponent<Calibration>().enabled = true;
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

}
