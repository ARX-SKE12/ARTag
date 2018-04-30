
namespace ARTag
{
    using System.Collections;
    using UnityEngine;
    using Facebook.Unity;

    public class FriendList : MonoBehaviour
    {
        public GameObject friendItem, friendList;

        const string BASE_URL = "https://graph.facebook.com/";

        // Use this for initialization
        void Start()
        {
            StartCoroutine(LoadFriendList());
        }

        IEnumerator LoadFriendList()
        {
            WWW www = new WWW(BASE_URL+"me/friends?access_token="+AccessToken.CurrentAccessToken.TokenString);
            yield return www;
            JSONObject data = JSONObject.CreateStringObject(www.text).GetField("data");
            for (int i = 0;i < data.Count;i++) {
                JSONObject userData = data[i];
                Instantiate(friendItem, friendList.transform).GetComponent<FriendItem>().Initialize(userData.GetField("id").str, userData.GetField("name").str);
            }
        }
    }

}
