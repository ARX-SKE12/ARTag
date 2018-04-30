
namespace ARTag
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using Facebook.Unity;

    public class CollaborateItem : MonoBehaviour
    {
        public GameObject title;
        string id;

        const string BASE_URL = "https://graph.facebook.com/";

        public void Initialize(string id)
        {
            this.id = id;
            StartCoroutine(LoadFriendData());
        }

        IEnumerator LoadFriendData()
        {
            WWW www = new WWW(BASE_URL+id+"?access_token="+AccessToken.CurrentAccessToken.TokenString);
            yield return www;
            JSONObject data = JSONObject.StringObject(www.text);
            title.GetComponent<Text>().text = data.GetField("name").str;
        }

        public void DeleteCollaborator()
        {
            GameObject.FindObjectOfType<CollaborateController>().RemoveCollaborate(id);
        }

    }

}
