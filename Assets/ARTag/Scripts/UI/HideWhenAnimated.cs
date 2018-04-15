
namespace ARTag
{
    using UnityEngine;

    public class HideWhenAnimated : MonoBehaviour
    {
        public bool isFinishAnimation;

        // Use this for initialization
        void Start()
        {
            isFinishAnimation = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isFinishAnimation) gameObject.SetActive(false);
        }
    }

}
