
namespace ARTag
{
    using UnityEngine;
    
    public class CalibrationInstruction : Instruction
    {
        public GameObject pointCloud, scanUI, calibrator;

        protected override void FinishAction()
        {
            pointCloud.SetActive(false);
            scanUI.SetActive(false);
            calibrator.SetActive(false);
            base.FinishAction();
        }
        
    }

}
