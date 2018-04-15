using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour {
	public GameObject effect;
	public float spawnInterval;
	public GameObject target;
	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnEffect());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator SpawnEffect() {
		while(true) {
			NavigationPather particle = Instantiate(effect).GetComponent<NavigationPather>();
			particle.target = target;
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
