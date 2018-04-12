
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneLoader : MonoBehaviour
    {

        public void LoadScene(string sceneName)
        {
            PlayerPrefs.SetString("prevScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneName);
        }
    }

}
