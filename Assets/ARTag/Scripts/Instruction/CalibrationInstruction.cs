
namespace ARTag
{
    using UnityEngine;
    
    public class CalibrationInstruction : Instruction
    {
        public GameObject calibrator;

        protected override void FinishAction()
        {
            calibrator.GetComponent<Calibrator>().enabled = true;
            base.FinishAction();
        }
        
    }

}
