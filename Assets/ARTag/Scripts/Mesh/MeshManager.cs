using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshManager : MonoBehaviour {
	public GameObject cube;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SpawnAndModifyMesh(Vector3[] sentVertices, Vector2[] sentUV, int[] sentTriangles) {
		Mesh mesh = new Mesh();
		mesh.vertices = sentVertices;
		mesh.uv = sentUV;
		mesh.triangles = sentTriangles;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		GameObject newCube = Instantiate(cube, Vector3.zero, Quaternion.identity);
		newCube.GetComponent<MeshFilter>().mesh = mesh;
		newCube.GetComponent<MeshFilter>().mesh.UploadMeshData(true);
		Debug.LogWarning("Spawned a cube");
	}
}
