
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UserModeInstruction : MonoBehaviour
    {

        public GameObject thumbnail, instructionText, changeStepButton, actionBar, numPeopleText;
        public Sprite finishButtonSprite;
        public Sprite[] stepThumbnail;
        int step = 0;

        public void NextStep()
        {
            switch (++step)
            {
                case 1:
                    instructionText.GetComponent<Text>().text = "Tag Will Appear When You Get Close";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[0];
                    break;
                case 2:
                    instructionText.GetComponent<Text>().text = "Navigate to Your Interested Tag";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[1];
                    break;
                case 5:
                    instructionText.GetComponent<Text>().text = "Enjoy ARTag!";
                    thumbnail.GetComponent<Image>().sprite = stepThumbnail[2];
                    changeStepButton.GetComponent<Image>().sprite = finishButtonSprite;
                    changeStepButton.GetComponentInChildren<Text>().text = "Let's Start!";
                    break;
                case 3:
                    actionBar.SetActive(true);
        //            numPeopleText.SetActive(true);
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }


}
