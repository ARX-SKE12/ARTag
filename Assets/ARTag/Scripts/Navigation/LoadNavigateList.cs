
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LoadNavigateList : MonoBehaviour
    {
        public GameObject tagListItemPrefab, tagList;
        // Use this for initialization
        void Start()
        {
            LoadTagList();
        }

        void LoadTagList()
        {
            foreach (TagBehaviour tag in GameObject.FindObjectsOfType<TagBehaviour>())
            {
                if (tag.title.activeSelf)
                {
                    GameObject tagObject = Instantiate(tagListItemPrefab, tagList.transform);
                    tagObject.GetComponent<NavigateListItem>().Initialize(tag);
                }
            }
        }
    }

}
