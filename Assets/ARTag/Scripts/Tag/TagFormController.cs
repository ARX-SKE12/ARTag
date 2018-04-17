
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
        public GameObject[] tagPrefabs;

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
            this.type = type;
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
            GameObject tagObj = Instantiate(tagPrefabs[type - 1], Camera.main.transform.position + Camera.main.transform.forward * 0.15f, Quaternion.identity, GameObject.Find("Tag Editor").transform);
            TagBehaviour tag = tagObj.GetComponent<TagBehaviour>();
            float sizeVal = (int)int.Parse(size.GetComponentInChildren<InputField>().text);
            tag.transform.LookAt(Camera.main.transform);
            tag.transform.Rotate(new Vector3(0, 180, 0));
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["title"] = title.GetComponentInChildren<InputField>().text;
            data["size"] = sizeVal;
            tag.Initialize(data);
            gameObject.SetActive(false);
        }
    }

}
