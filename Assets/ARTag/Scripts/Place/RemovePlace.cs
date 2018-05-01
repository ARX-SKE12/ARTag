
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using SocketIO;
    using SocketIOManager;

    public class RemovePlace : MonoBehaviour
    {
        public GameObject deletingObject;

        void Start()
        {
            GameObject.FindObjectOfType<SocketManager>().On(EventsCollector.PLACE_UPDATE_SUCCESS, OnPlaceDelete);
            GameObject.FindObjectOfType<SocketManager>().On(EventsCollector.PLACE_UPDATE_ERROR, OnPlaceDeleteError);
        }

        public void DeletePlace()
        {
            JSONObject data = new JSONObject();
            data.AddField("id", ((Place)GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace")).id);
            JSONObject detail = new JSONObject();
            detail.AddField("isActive", false);
            data.SetField("updatedData", detail);
            GameObject.FindObjectOfType<SocketManager>().Emit(EventsCollector.PLACE_UPDATE, data);
            deletingObject.SetActive(true);
        }

        public void OnPlaceDelete(SocketIOEvent e)
        {
            if (!e.data.GetField("place").GetField("isActive").b)
            {
                GameObject.FindObjectOfType<SocketManager>().Emit(EventsCollector.PLACE_CLEAR_PAGGING);
                SceneManager.LoadScene("Select Place");
            } 
        }

        public void OnPlaceDeleteError(SocketIOEvent e)
        {
            deletingObject.SetActive(false);
        }
    }

}
