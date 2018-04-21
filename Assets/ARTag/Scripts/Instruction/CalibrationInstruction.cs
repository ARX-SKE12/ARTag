
namespace ARTag
{
    using UnityEngine;
    
    public class CalibrationInstruction : Instruction
    {
        public GameObject calibrator;

        protected override void FinishAction()
        {
            calibrator.GetComponent<Calibration>().enabled = true;
            base.FinishAction();
        }
        
    }

}
