
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;

    public class CalibratorCube : MonoBehaviour
    {
        public string[] guideMsg;
        public GameObject backButton, instructionText;
        bool shouldRotate;
        int step = 0;
        int sign;

        // Update is called once per frame
        void Update()
        {
            if (shouldRotate) Rotate();
        }

        public void Rotate()
        {
            GameObject cube = GameObject.FindObjectOfType<Calibrator>().calibrationCube;
            float x = cube.transform.localRotation.eulerAngles.x;
            float y = cube.transform.localRotation.eulerAngles.y;
            float z = cube.transform.localRotation.eulerAngles.z;
            switch (step)
            {
                case 0:
                    x += sign;
                    break;
                case 1:
                    y += sign;
                    break;
                case 2:
                    z += sign;
                    break;
                default:
                    break;
            }
            cube.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        public void StartRotate(int sign)
        {
            this.sign = sign;
            shouldRotate = true;
        }

        public void StopRotate()
        {
            shouldRotate = false;
        }

        public void NextStep()
        {
            step++;
            if (step > 0) backButton.GetComponent<Button>().interactable = true;
            if (step > 2) GameObject.FindObjectOfType<Calibrator>().FinishLocationAdjustment();
            if (step <= 2) instructionText.GetComponent<Text>().text = guideMsg[step];            
        }

        public void Back()
        {
            step--;
            instructionText.GetComponent<Text>().text = guideMsg[step];
            if (step == 0) backButton.GetComponent<Button>().interactable = false;
        }
    }

}
