
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Pathfinding;

    public class NavigationController : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NavigateTo(GameObject target)
        {
            GameObject.FindObjectOfType<EffectSpawner>().target = target;
        }
    }

}
