
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;

    public class NavigateListItem : MonoBehaviour
    {

        public TagBehaviour tagObject;

        public void Initialize(TagBehaviour tag)
        {
            GetComponentInChildren<Text>().text = tag.title.GetComponent<Text>().text;
            this.tagObject = tag;
        }

        public void Select()
        {
            GameObject.FindObjectOfType<NavigationController>().NavigateTo(tagObject.gameObject);
            GameObject.FindObjectOfType<LoadNavigateList>().gameObject.SetActive(false);
        }
    }

}
