
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class TagFormController : MonoBehaviour
    {
        int type;
        public GameObject thumbnail, title, size, description;
        public Sprite defaultThumbnail;
        public int defaultSize = 30;
        public float defaultRange = 1.5f;

        public void SelectType(int type)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            gameObject.SetActiveRecursively(true);
#pragma warning restore CS0618 // Type or member is obsolete
            this.type = type;
            if (type==1 || type == 2) thumbnail.SetActive(false);
            if (type == 1 || type == 3 || type == 4) description.SetActive(false);
            if (type == 3) title.SetActive(false);
            GetComponent<VerticalLayoutGroup>().padding.left = !thumbnail.activeSelf ? 40 : 0;
            GetComponent<VerticalLayoutGroup>().padding.right = !!thumbnail.activeSelf ? 40 : 0;
            GetComponent<VerticalLayoutGroup>().padding.top = !thumbnail.activeSelf ? 40 : 0;
        }

        public void Create()
        {
            float sizeVal = (int)int.Parse(size.GetComponentInChildren<InputField>().text);
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (type != 3 ) data["title"] = title.GetComponentInChildren<InputField>().text;
            data["size"] = sizeVal;
            data["type"] = type;
            if (type == 2 || type ==5 || type == 6) data["description"] = description.GetComponentInChildren<TMP_InputField>().text;
            if (type == 3 || type == 4 || type == 5) data["image"] = thumbnail.GetComponentInChildren<ImagePicker>().selectedImage;
            GameObject.FindObjectOfType<TagManager>().CreateTag(data);
            ClearForm();
            gameObject.SetActive(false);
        }

        void ClearForm()
        {
            thumbnail.GetComponent<Image>().sprite = defaultThumbnail;
            title.GetComponentInChildren<InputField>().text = "";
            description.GetComponentInChildren<TMP_InputField>().text = "";
            size.GetComponentInChildren<InputField>().text = defaultSize.ToString();
        }
    }

}
