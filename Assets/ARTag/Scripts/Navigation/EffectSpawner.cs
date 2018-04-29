﻿
namespace ARTag
{
    using System.Collections;
    using UnityEngine;

    public class EffectSpawner : MonoBehaviour
    {
        public GameObject effect;
        public float spawnInterval, time;
        public GameObject target;

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time>spawnInterval)
            {
                if (target != null)
                {
                    time = 0;
                    NavigationPather particle = Instantiate(effect, Camera.main.transform.position, Quaternion.identity).GetComponent<NavigationPather>();
                    particle.target = target;
                }
            }
        }

    }

}
