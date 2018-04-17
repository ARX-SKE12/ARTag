
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;
        
    public class TagInstruction : MonoBehaviour
    {
        public GameObject thumbnail, instructionText, changeStepButton, actionPanel;
        public Sprite finishButtonSprite;
        public Sprite[] stepThumbnail;
        int step = 0;

        public void NextStep()
        {
            switch (++step)
            {
                case 1:
                    instructionText.GetComponent<Text>().text = "Press Tag Button";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[0];
                    break;
                case 2:
                    instructionText.GetComponent<Text>().text = "Select Tag Type";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[1];
                    break;
                case 3:
                    instructionText.GetComponent<Text>().text = "Fill the Form";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[2];
                    break;
                case 4:
                    instructionText.GetComponent<Text>().text = "Tag Appear!";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[3];
                    changeStepButton.GetComponent<Image>().sprite = finishButtonSprite;
                    changeStepButton.GetComponentInChildren<Text>().text = "I got it!";
                    break;
                case 5:
                    actionPanel.SetActive(true);
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
