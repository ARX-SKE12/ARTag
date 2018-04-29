
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Pathfinding;

    public class NavigationController : MonoBehaviour
    {

        public void NavigateTo(GameObject target)
        {
            GameObject.FindObjectOfType<EffectSpawner>().target = target;
        }
    }

}
