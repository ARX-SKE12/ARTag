
namespace ARTag
{
    using UnityEngine;

    public class TagTypeSelector : MonoBehaviour
    {
        public GameObject tagForm;
        
        public void ShowForm(int type)
        {
            tagForm.SetActive(true);
            tagForm.GetComponent<TagFormController>().SelectType(type);
            gameObject.SetActive(false);
        }

    }

}
