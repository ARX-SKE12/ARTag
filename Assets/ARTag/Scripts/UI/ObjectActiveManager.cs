
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ObjectActiveManager : MonoBehaviour
    {

        public bool isActive;
        public GameObject go;

        public void ChangeState()
        {
            go.SetActive(isActive);
        }
    }

}
