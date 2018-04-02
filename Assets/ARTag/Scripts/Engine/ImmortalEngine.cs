
namespace ARTag
{
    using UnityEngine;

    public class ImmortalEngine : MonoBehaviour
    {

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}
