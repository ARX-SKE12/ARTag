
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class Back : MonoBehaviour
    {
        string prevScene;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) BackToPreviousScene();
        }

        public void BackToPreviousScene()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            if (index == 0) Application.Quit();
            else SceneManager.LoadScene(PlayerPrefs.GetString("prevScene"));
        }
    }

}
