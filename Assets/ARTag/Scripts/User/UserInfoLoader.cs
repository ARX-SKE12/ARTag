
namespace ARTag
{
    
    using UnityEngine;
    using UnityEngine.UI;

    public class UserInfoLoader : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            string name = PlayerPrefs.GetString("name");
            GameObject.Find(ObjectsCollector.USER_INFO_TEXT).GetComponent<Text>().text = name;
        }

    }

}
