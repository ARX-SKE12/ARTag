
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Instruction : MonoBehaviour
    {

        public GameObject thumbnail, instructionText, changeStepButton;
        public Sprite[] thumbnails;
        public string[] guideTexts;
        public Sprite finishButtonSprite;
        public string finishButtonText = "I got it!";
        public int numStep;
        public GameObject[] toActivateGameObjects;

        int step = 0;

        // Use this for initialization
        void Start()
        {
            NextStep();
        }

        public void NextStep()
        {
            UpdateInstruction();
            step++;
        }

        void UpdateInstruction()
        {
            if (step < numStep)
            {
                thumbnail.GetComponent<Image>().sprite = thumbnails[step];
                instructionText.GetComponent<Text>().text = guideTexts[step];
                if (step == numStep-1)
                {
                    changeStepButton.GetComponent<Image>().sprite = finishButtonSprite;
                    changeStepButton.GetComponentInChildren<Text>().text = finishButtonText;
                }
            } else
            {
                FinishAction();
            }
        }

        protected virtual void FinishAction()
        {
            foreach (GameObject go in toActivateGameObjects) go.SetActive(true);
            gameObject.SetActive(false);
        }

    }

}
