
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using SocketIOManager;

    public class SceneLoader : MonoBehaviour
    {

        public void LoadScene(string sceneName)
        {
            GameObject.FindObjectOfType<SocketManager>().Emit(EventsCollector.PLACE_CLEAR_PAGGING);
            PlayerPrefs.SetString("prevScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneName);
        }
    }

}
