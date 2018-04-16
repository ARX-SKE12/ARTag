
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class TagBehaviour : MonoBehaviour
    {
        public GameObject title;
        string paddingStr = "          ";

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(Dictionary<string, object> data)
        {
            title.GetComponent<Text>().text = paddingStr+(string)data["title"]+paddingStr;
        }
    }

}
