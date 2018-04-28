
namespace ARTag
{
    using UnityEngine;
    
    public class CalibrationInstruction : Instruction
    {
        public GameObject calibrator;

        protected override void FinishAction()
        {
            GameObject.FindObjectOfType<Calibrator>().ChangeCalibrationState(Calibrator.CalibratorState.TRACKING_MARKER);
            base.FinishAction();
        }
        
    }

}
