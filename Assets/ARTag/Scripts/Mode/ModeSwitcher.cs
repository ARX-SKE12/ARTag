
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ModeSwitcher : MonoBehaviour
    {
        public GameObject tag, plane;

        public void SwitchMode()
        {
            if (plane.activeSelf)
            {
                GetComponentInChildren<Text>().text = "Scan";
                plane.GetComponent<PlaneEditor>().Disable();
                tag.SetActive(true);
                plane.SetActive(false);
            } else
            {
                GetComponentInChildren<Text>().text = "Tag";
                plane.SetActive(true);
                tag.SetActive(false);
            }
        }
    }

}
