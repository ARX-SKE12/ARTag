
namespace ARTag
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class UserInfoLoader : MonoBehaviour
    {

        public GameObject userText, userProfilePic;

        // Use this for initialization
        void Start()
        {
            string name = PlayerPrefs.GetString("name");
            string profilePictureURL = PlayerPrefs.GetString("profilePictureURL");
            userText.GetComponent<Text>().text = name;
            StartCoroutine(LoadProfileImage(profilePictureURL));
        }

        IEnumerator LoadProfileImage(string profileUrl)
        {
            WWW www = new WWW(profileUrl);
            yield return www;
            Texture2D profileImage = www.texture;
            userProfilePic.GetComponent<Image>().sprite = Sprite.Create(profileImage, new Rect(0, 0, profileImage.width, profileImage.height), new Vector2(0.5f, 0.5f), 100);
        }

    }

}
