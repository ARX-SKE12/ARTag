
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class TagFormController : MonoBehaviour
    {
        int type;
        public GameObject thumbnail, title, range, size, description; 

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SelectType(int type)
        {
            switch (type)
            {
                case 1:
                    thumbnail.SetActive(false);
                    description.SetActive(false);
                    break;
                case 2:
                    thumbnail.SetActive(false);
                    break;
                default:
                    break;
            }
            if (!thumbnail.activeSelf)
            {
                GetComponent<VerticalLayoutGroup>().padding.left = 40;
                GetComponent<VerticalLayoutGroup>().padding.right = 40;
                GetComponent<VerticalLayoutGroup>().padding.top = 40;
            } else
            {
                GetComponent<VerticalLayoutGroup>().padding.left = 0;
                GetComponent<VerticalLayoutGroup>().padding.right = 0;
                GetComponent<VerticalLayoutGroup>().padding.top = 0;
            } 
        }

        public void Create()
        {

        }
    }

}
