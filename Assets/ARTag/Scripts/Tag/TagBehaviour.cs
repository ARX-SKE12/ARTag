
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
            JSONObject jsonData = PrepareTagData(data);
            manager.Emit(EventsCollector.TAG_CREATE, jsonData);
            title.GetComponent<Text>().text = "Creating...";
            float sizeVal = (float) data["size"] * 0.05f;
            transform.localScale = new Vector3(sizeVal, sizeVal, transform.localScale.z);
        }

        protected JSONObject PrepareTagData(Dictionary<string, object> data)
        {
            JSONObject jsonData = new JSONObject();
            Place place = (Place)GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace");
            jsonData.AddField("placeId", place.id);
            JSONObject tagDetail = new JSONObject();
            tagDetail.AddField("title", (string)data["title"]);
            tagDetail.AddField("size", (float)data["size"]);
            jsonData.AddField("detail", tagDetail);
            JSONObject posData = new JSONObject();
            Vector3 currentPos = transform.position - GameObject.FindObjectOfType<Calibration>().offsetPosition;
            Debug.Log(currentPos);
            posData.AddField("x", currentPos.x);
            posData.AddField("y", currentPos.y);
            posData.AddField("z", currentPos.z);
            jsonData.AddField("position", posData);
            JSONObject rotateData = new JSONObject();
            Quaternion currentRotate = Quaternion.Inverse(GameObject.FindObjectOfType<Calibration>().offsetRotation) * transform.rotation;
            rotateData.AddField("x", currentRotate.x);
            rotateData.AddField("y", currentRotate.y);
            rotateData.AddField("z", currentRotate.z);
            rotateData.AddField("w", currentRotate.w);
            jsonData.AddField("rotation", rotateData);
            return jsonData;
        }

        public void OnCreateSuccess(SocketIOEvent e)
        {
            if (id == "") ConstructTag(e.data);
            
        }

        protected void ConstructTag(JSONObject data)
        {
            Calibration calibration = GameObject.FindObjectOfType<Calibration>();
            JSONObject tagData = data.GetField("tag");
            id = tagData.GetField("id").str;
            JSONObject tagDetailData = tagData.GetField("detail");
            float sizeVal = tagDetailData.GetField("size").f * 0.05f;
            transform.localScale = new Vector3(sizeVal, sizeVal, transform.localScale.z);
            string title = tagDetailData.GetField("title").str;
            GetComponentInChildren<Text>().text = title;
            JSONObject tagPosition = tagData.GetField("position");
            float x = tagPosition.GetField("x").f;
            float y = tagPosition.GetField("y").f;
            float z = tagPosition.GetField("z").f;
            Debug.Log(transform.localPosition);
            transform.position = new Vector3(x, y, z) + calibration.offsetPosition;
            Debug.Log(transform.localPosition);
            JSONObject tagRotation = tagData.GetField("rotation");
            float xr = tagRotation.GetField("x").f;
            float yr = tagRotation.GetField("y").f;
            float zr = tagRotation.GetField("z").f;
            float wr = tagRotation.GetField("w").f;
            /**Debug.Log(transform.localRotation);
            transform.rotation = new Quaternion(xr, yr, zr, wr) * Quaternion.Inverse(calibration.offsetRotation) * Quaternion.Euler(new Vector3(180, 0, 0));
            Debug.Log(transform.localRotation);**/
        }
    }

}
