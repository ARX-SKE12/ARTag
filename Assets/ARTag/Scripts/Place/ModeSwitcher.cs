
namespace ARTag
{
    using UnityEngine;

    public class ModeSwitcher : MonoBehaviour
    {
        public GameObject tag, plane;

        public void SwitchMode()
        {
            if (plane.activeSelf)
            {
                plane.GetComponent<PlaneEditor>().Disable();
                tag.SetActive(true);
                plane.SetActive(false);
            } else
            {
                plane.SetActive(true);
                tag.SetActive(false);
            }
        }
    }

}
