
namespace ARTag
{
    using System.Collections;
    using UnityEngine;

    public class EffectSpawner : MonoBehaviour
    {
        public GameObject effect;
        public float spawnInterval;
        public GameObject target;
        // Use this for initialization
        void Start()
        {
            //target = GameObject.Find("Target");
            StartCoroutine(SpawnEffect());
        }

        // Update is called once per frame
        void Update()
        {

        }
        IEnumerator SpawnEffect()
        {
            while (true)
            {
                if (target != null)
                {
                    NavigationPather particle = Instantiate(effect).GetComponent<NavigationPather>();
                    particle.target = target;
                    yield return new WaitForSeconds(spawnInterval);
                }
            }
        }
    }

}
