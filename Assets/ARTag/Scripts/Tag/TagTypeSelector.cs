
namespace ARTag
{
    using UnityEngine;

    public class TagTypeSelector : MonoBehaviour
    {
        public int type;
        public GameObject selectModal, tagForm;
        
        public void ShowForm()
        {
            tagForm.SetActive(true);
            Debug.Log(tagForm);
            tagForm.GetComponent<TagFormController>().SelectType(type);
            selectModal.SetActive(false);
        }

    }

}
