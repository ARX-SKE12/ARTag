
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using SocketIO;
    using SocketIOManager;

    public class TagManager : MonoBehaviour
    {
        SocketManager socketManager;
        Dictionary<string, TagBehaviour> tags;
        public GameObject[] tagPrefabs;
        public GameObject notification, errorNotification;
        public float distanceFromCam = 0.15f;

        Vector3 ROTATE_OFFSET = new Vector3(0, 180);

        void Start()
        {
            socketManager = GameObject.FindObjectOfType<SocketManager>();
            tags = new Dictionary<string, TagBehaviour>();
            socketManager.On(EventsCollector.TAG_LIST, LoadTag);
            socketManager.Emit(EventsCollector.TAG_LIST_REQUEST);
            socketManager.On(EventsCollector.TAG_ERROR, OnError);
        }

        public void LoadTag(SocketIOEvent e)
        {
            JSONObject data = e.data.GetField("tags");
            for (int i = 0; i < data.Count; i++) ReconstructTag(data[i]);
        }

        public void RegisterTag(string id, TagBehaviour tag)
        {
            tags[id] = tag;
        }

        public void CreateTag(Dictionary<string, object> data)
        {
            int type = (int)data["type"];
            GameObject tagObj = Instantiate(tagPrefabs[type - 1], Camera.main.transform.position + Camera.main.transform.forward * distanceFromCam, Quaternion.identity, transform);
            TagBehaviour tag = tagObj.GetComponent<TagBehaviour>();
            tag.transform.LookAt(Camera.main.transform);
            tag.transform.Rotate(ROTATE_OFFSET);
            tag.Initialize(data);
        }

        public void ReconstructTag(JSONObject datum)
        {
            string id = datum.GetField("id").str;
            int type = (int)datum.GetField("type").n;
            GameObject tag = Instantiate(tagPrefabs[type - 1], Vector3.zero, Quaternion.identity, transform);
            tag.GetComponent<TagBehaviour>().ConstructTag(datum);
            RegisterTag(id, tag.GetComponent<TagBehaviour>());
        }

        public void OnTagDataUpdate(SocketIOEvent e)
        {
            JSONObject data = e.data.GetField("tag");
            string id = data.GetField("id").str;
            if (tags.ContainsKey(id)) tags[id].ConstructTag(data);
            else ReconstructTag(data);
            notification.GetComponentInChildren<Text>().text = "Some Tag in this room is updated!";
            notification.SetActive(true);
        }

        public void OnError(SocketIOEvent e)
        {
            GameObject.FindObjectOfType<TagManager>().errorNotification.GetComponentInChildren<Text>().text = e.data.GetField("error").str;
            GameObject.FindObjectOfType<TagManager>().errorNotification.SetActive(true);
        }
        
    }

}
