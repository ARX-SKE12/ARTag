
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
    }

}
