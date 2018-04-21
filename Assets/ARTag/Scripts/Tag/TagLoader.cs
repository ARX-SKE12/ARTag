
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using SocketIO;
    using SocketIOManager;

    public class TagLoader : MonoBehaviour
    {
        SocketManager socketManager;
        Dictionary<string, TagBehaviour> tags;
        public GameObject[] tagPrefabs;

        // Use this for initialization
        void Start()
        {
            socketManager = GameObject.FindObjectOfType<SocketManager>();
            tags = new Dictionary<string, TagBehaviour>();
            socketManager.On(EventsCollector.TAG_LIST, LoadTag);
            socketManager.Emit(EventsCollector.TAG_LIST_REQUEST);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadTag(SocketIOEvent e)
        {
            JSONObject data = e.data.GetField("tags");
            for (int i = 0; i < data.Count; i++)
            {
                JSONObject datum = data[i];
                string id = datum.GetField("id").str;
                int type = (int) datum.GetField("type").n;
                GameObject tag = Instantiate(tagPrefabs[type - 1], Vector3.zero, Quaternion.identity, transform);
                tag.GetComponent<TagBehaviour>().ConstructTag(datum);
                RegisterTag(id, tag.GetComponent<TagBehaviour>());
            }
        }

        public void RegisterTag(string id, TagBehaviour tag)
        {
            tags[id] = tag;
        }

        public void CreateTag(Dictionary<string, object> data)
        {

        }

    }

}
