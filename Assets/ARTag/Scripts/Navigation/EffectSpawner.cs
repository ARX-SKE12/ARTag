
namespace ARTag
{
    using System.Collections;
    using UnityEngine;

    public class EffectSpawner : MonoBehaviour
    {
        public GameObject effect;
        public float spawnInterval, time;
        public GameObject target;
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time>spawnInterval)
            {
                if (target != null)
                {
                    time = 0;
                    NavigationPather particle = Instantiate(effect, transform.localPosition, Quaternion.identity).GetComponent<NavigationPather>();
                    particle.target = target;
                }
            }
        }

    }

}
