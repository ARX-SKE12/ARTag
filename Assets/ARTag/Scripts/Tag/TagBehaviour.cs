using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARTag
{

    public class TagBehaviour : MonoBehaviour
    {

        void Update()
        {
            transform.LookAt(GameObject.Find("First Person Camera").transform);
        }

        public void SetText(string text)
        {
            GetComponentInChildren<Text>().text = text;
        }
    }

}