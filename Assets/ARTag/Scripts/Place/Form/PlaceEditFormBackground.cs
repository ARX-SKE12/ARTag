
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;

    public class PlaceEditFormBackground : MonoBehaviour
    {
        public GameObject imagePicker;

        // Use this for initialization
        void Start()
        {
            imagePicker.GetComponent<ImagePicker>().Register(gameObject);
        }

        void OnImageLoaded(Sprite sprite)
        {
            GetComponent<Image>().sprite = sprite;
        }
    }

}
