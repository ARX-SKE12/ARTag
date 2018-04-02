
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PositionController : MonoBehaviour
    {
        TemporaryDataManager tempManager;

        // Use this for initialization
        void Start()
        {
            tempManager = GameObject.FindObjectOfType<TemporaryDataManager>();
            transform.position = (Vector3) tempManager.Get("position");
        }

        // Update is called once per frame
        void Update()
        {
            GameObject.Find(ObjectsCollector.CAMERA_POSITION_TEXT).GetComponent<Text>().text = Camera.main.transform.position.ToString();
        }
    }

}
