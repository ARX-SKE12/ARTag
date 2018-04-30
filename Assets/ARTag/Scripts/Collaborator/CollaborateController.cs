
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using SocketIO;
    using SocketIOManager;

    public class CollaborateController : MonoBehaviour
    {
        public GameObject collabList, collabItemPrefab;
        List<string> users;
        SocketManager socket;

        // Use this for initialization
        void Start()
        {
            socket.On(EventsCollector.COLLABORATE_UPDATE_SUCCESS, OnCollabUpdate);
            Create();
        }

        public void OnCollabUpdate(SocketIOEvent e)
        {
            foreach (Transform child in collabList.transform) Destroy(child);
            Place place = new Place(e.data.GetField("place"));
            GameObject.FindObjectOfType<TemporaryDataManager>().Put("currentPlace", place);
            Create();
        }

        void Create()
        {
            Place place = (Place) GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace");
            users = place.users;
            foreach (string user in place.users)
            {
                CollaborateItem collabItem = Instantiate(collabItemPrefab, collabList.transform).GetComponent<CollaborateItem>();
                collabItem.Initialize(user);
            }
        }

        public void AddCollaborate(string id)
        {
            users.Add(id);
            UpdateCollaborate();
        }

        public void RemoveCollaborate(string id)
        {
            users.Remove(id);
            UpdateCollaborate();
        }

        void UpdateCollaborate()
        {
            JSONObject usersData = new JSONObject();
            for (int i = 0; i < users.Count; i++) usersData[i] = new JSONObject(users[i]);
            JSONObject data = new JSONObject();
            data.AddField("users", usersData);
            socket.Emit(EventsCollector.COLLABORATE_UPDATE, data);
        }
    }

}
