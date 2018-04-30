
namespace ARTag
{
    using UnityEngine;
    using UnityEngine.UI;

    public class FriendItem : MonoBehaviour
    {

        string id;
        public GameObject title;

        public void Initialize(string id, string name)
        {
            this.id = id;
            title.GetComponent<Text>().text = name;
        }

        public void AddCollaborator()
        {
            GameObject.FindObjectOfType<CollaborateController>().AddCollaborate(id);
        }
    }

}
