
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using SocketIOManager;
    using SocketIO;

    public abstract class TagBehaviour : MonoBehaviour
    {
        public GameObject title;
        public string id = "";
        public string datPos = "";
        SocketManager manager;
        Calibrator calibrator;

        void Awake()
        {
            calibrator = GameObject.FindObjectOfType<Calibrator>();
            manager = GameObject.FindObjectOfType<SocketManager>();
            manager.On(EventsCollector.TAG_CREATE_SUCCESS, OnCreateSuccess);
        }

        public void Initialize(Dictionary<string, object> data)
        {
            JSONObject jsonData = PrepareTagData(data);
            manager.Emit(EventsCollector.TAG_CREATE, jsonData);
            title.GetComponent<Text>().text = "Creating...";
            float sizeVal = (float) data["size"] * 0.05f;
            transform.localScale = new Vector3(sizeVal, sizeVal, transform.localScale.z);
        }

        protected virtual JSONObject PrepareTagData(Dictionary<string, object> data)
        {
            JSONObject jsonData = new JSONObject();
            JSONObject posData = new JSONObject();
            Vector3 currentPos = calibrator.GetRealWorldPosition(transform.position);
            posData.AddField("x", currentPos.x);
            posData.AddField("y", currentPos.y);
            posData.AddField("z", currentPos.z);
            jsonData.AddField("position", posData);
            JSONObject rotateData = new JSONObject();
            Quaternion currentRotate = calibrator.GetRealWorldRotation(transform.localRotation);
            rotateData.AddField("x", currentRotate.x);
            rotateData.AddField("y", currentRotate.y);
            rotateData.AddField("z", currentRotate.z);
            rotateData.AddField("w", currentRotate.w);
            jsonData.AddField("rotation", rotateData);
            JSONObject tagDetail = new JSONObject();
            tagDetail.AddField("size", (float)data["size"]);
            jsonData.AddField("detail", tagDetail);
            Place place = (Place)GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace");
            jsonData.AddField("placeId", place.id);
            jsonData.AddField("type", (int)data["type"]);
            return jsonData;
        }

        public void OnCreateSuccess(SocketIOEvent e)
        {
            if (id == "")
            {
                ConstructTag(e.data);
                GameObject.FindObjectOfType<TagManager>().notification.GetComponentInChildren<Text>().text = "New Tag is created!";
                GameObject.FindObjectOfType<TagManager>().notification.SetActive(true);
            }
        }

        public virtual void ConstructTag(JSONObject data)
        {
            JSONObject tagData = GetTagData(data);
            id = tagData.GetField("id").str;
            GameObject.FindObjectOfType<TagManager>().RegisterTag(id, this);
            JSONObject tagDetailData = tagData.GetField("detail");
            float sizeVal = tagDetailData.GetField("size").f * 0.05f;
            transform.localScale = new Vector3(sizeVal, sizeVal, transform.localScale.z);
            JSONObject tagPosition = tagData.GetField("position");
            float x = tagPosition.GetField("x").f;
            float y = tagPosition.GetField("y").f;
            float z = tagPosition.GetField("z").f;
            datPos = new Vector3(x,y,z).ToString();
            transform.position = calibrator.GetVirtualPosition(new Vector3(x, y, z));
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            JSONObject tagRotation = tagData.GetField("rotation");
            float xr = tagRotation.GetField("x").f;
            float yr = tagRotation.GetField("y").f;
            float zr = tagRotation.GetField("z").f;
            float wr = tagRotation.GetField("w").f;
            transform.rotation = calibrator.GetVirtualRotation(new Quaternion(xr,yr,zr,wr));
        }

        protected JSONObject GetTagData(JSONObject data)
        {
            return data.HasField("tag") ? data.GetField("tag") : data;
        }

    }

}
