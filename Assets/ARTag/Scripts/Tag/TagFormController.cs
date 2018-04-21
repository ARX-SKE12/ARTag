
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class TagFormController : MonoBehaviour
    {
        int type;
        public GameObject thumbnail, title, range, size, description;
        public GameObject[] tagPrefabs;
        public Sprite defaultThumbnail;
        public int defaultSize = 30;
        public float defaultRange = 1.5f;

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
            title.SetActive(true);
            thumbnail.SetActive(true);
            description.SetActive(true);
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
                case 3:
                    description.SetActive(false);
                    title.SetActive(false);
                    break;
                case 4:
                    description.SetActive(false);
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
            if (type != 3 ) data["title"] = title.GetComponentInChildren<InputField>().text;
            data["size"] = sizeVal;
            data["type"] = type;
            if (type == 2 || type ==5 || type == 6) data["description"] = description.GetComponentInChildren<TMP_InputField>().text;
            if (type == 3 || type == 4 || type == 5) data["image"] = thumbnail.GetComponentInChildren<ImagePicker>().selectedImage;
            tag.Initialize(data);
            ClearForm();
            gameObject.SetActive(false);
        }

        void ClearForm()
        {
            thumbnail.GetComponent<Image>().sprite = defaultThumbnail;
            title.GetComponent<InputField>().text = "";
            description.GetComponent<TMP_InputField>().text = "";
            range.GetComponent<InputField>().text = defaultRange.ToString();
            size.GetComponent<InputField>().text = defaultSize.ToString();
        }
    }

}
