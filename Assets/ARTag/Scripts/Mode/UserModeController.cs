
namespace ARTag
{
    using UnityEngine;

    public class UserModeController : MonoBehaviour
    {
        public GameObject planeSpawner, canvas, tagManager;

        // Use this for initialization
        void Start()
        {
            GameObject.FindObjectOfType<Calibrator>().Register(gameObject);
        }

        void OnFinishCalibration()
        {
            planeSpawner.SetActive(true);
            planeSpawner.GetComponent<ServerPlaneGenerator>().GeneratePlane();
            tagManager.SetActive(true);
            canvas.SetActive(true);
        }

    }

}
