
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using SocketIOManager;
    using SocketIO;

    public class TagBehaviour : MonoBehaviour
    {
        public GameObject title;
        string paddingStr = "          ";
        string id = "";
        SocketManager manager;

        void Awake()
        {
            manager = GameObject.FindObjectOfType<SocketManager>();
            manager.On(EventsCollector.TAG_CREATE_SUCCESS, OnCreateSuccess);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(Dictionary<string, object> data)
        {
            JSONObject jsonData = new JSONObject();
            Place place = (Place) GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace");
            jsonData.AddField("placeId", place.id);
            JSONObject tagDetail = new JSONObject();
            tagDetail.AddField("title", (string) data["title"]);
            tagDetail.AddField("size", (float)data["size"]);
            jsonData.AddField("detail", tagDetail);
            JSONObject posData = new JSONObject();
            Vector3 currentPos = GameObject.FindObjectOfType<Calibration>().offsetPosition - transform.localPosition;
            posData.AddField("x", currentPos.x);
            posData.AddField("y", currentPos.y);
            posData.AddField("z", currentPos.z);
            jsonData.AddField("position", posData);
            JSONObject rotateData = new JSONObject();
            Quaternion currentRotate = GameObject.FindObjectOfType<Calibration>().offsetRotation *Quaternion.Inverse(transform.localRotation);
            rotateData.AddField("x", currentRotate.x);
            rotateData.AddField("y", currentRotate.y);
            rotateData.AddField("z", currentRotate.z);
            rotateData.AddField("w", currentRotate.w);
            jsonData.AddField("rotation", rotateData);
            manager.Emit(EventsCollector.TAG_CREATE, jsonData);
        }

        public void OnCreateSuccess(SocketIOEvent e)
        {
            Debug.Log(e.data);
            if (id=="")
            {
                id = e.data.GetField("tag").GetField("id").str;
                JSONObject data = e.data.GetField("tag").GetField("detail");
                float sizeVal = data.GetField("size").f;
                transform.localScale = new Vector3(sizeVal, sizeVal, transform.localScale.z);
                string title = paddingStr + data.GetField("title").str + paddingStr;
                GetComponentInChildren<Text>().text = title;
            }
            
        }
    }

}
