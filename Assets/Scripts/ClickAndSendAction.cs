using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndSendAction : MonoBehaviour {
	ConnectionController cc;
	// Use this for initialization
	void Start () {
		cc = FindObjectOfType<ConnectionController>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
			if(Input.GetMouseButtonDown(0) && hit.collider.gameObject == gameObject) {
				 cc.SendMeshData(GetComponent<MeshFilter>().mesh);
				 
				 Destroy(gameObject);
			}
		}	
	}

}
