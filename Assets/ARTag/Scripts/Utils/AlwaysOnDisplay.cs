
namespace ARTag
{
    using UnityEngine;

    public class AlwaysOnDisplay : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }

}
