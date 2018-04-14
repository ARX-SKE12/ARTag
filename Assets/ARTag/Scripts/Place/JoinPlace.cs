
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using SocketIOManager;

    public class JoinPlace : MonoBehaviour
    {

        SocketManager socketManager;

        // Use this for initialization
        void Start()
        {
            socketManager = GameObject.FindObjectOfType<SocketManager>();
        }

        public void JoinRoom(string sceneName)
        {
            JSONObject data = new JSONObject();
            data.AddField("placeId", ((Place)GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace")).id);
            socketManager.Emit(EventsCollector.ROOM_JOIN, data);
            SceneManager.LoadScene(sceneName);
        }
    }

}
