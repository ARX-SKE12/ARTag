using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class ConnectionController : MonoBehaviour {
	public SocketIOComponent socket;
	JSONObject testData;
	// Use this for initialization
	void Start () {
		socket = FindObjectOfType<SocketIOComponent>();
		if(socket != null) {
			socket.On("INCOMING_MESH", OnReceiveMesh);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendMeshData(Mesh mesh) {
		Dictionary<string,string> dict = new Dictionary<string, string>();

		//Debug.LogWarning("========SENDING MESH DATA========");
		dict["normals"] = Vector3ArrayToString(mesh.normals);
		//Debug.LogWarning("NORMALS : " + Vector3ArrayToString(mesh.normals));

		dict["bounds"] =  BoundsToString(mesh.bounds); 
		//Debug.LogWarning("BOUNDS : " + BoundsToString(mesh.bounds));

		dict["triangles"] = IntArrayToString(mesh.triangles);
		//Debug.LogWarning("TRIANGLES : " + IntArrayToString(mesh.triangles));

		dict["uv"] = Vector2ArrayToString(mesh.uv); 
		//Debug.LogWarning("UV : " + Vector2ArrayToString(mesh.uv));

		dict["vertices"] = Vector3ArrayToString(mesh.vertices);
		//Debug.LogWarning(Vector3ArrayToString(mesh.vertices));
		//dict["msg"] = "TEST";
		//Debug.LogWarning(Vector3ArrayToString(ParseVector3(Vector3ArrayToString(mesh.vertices))));
		//Debug.LogWarning("========END SENDING MESH DATA========");
		testData = new JSONObject(dict);
		//socket.Emit("MESH_DELIVERY", data);
		//socket.Emit("SIMPLE_EVENT");
		TestMeshData();
	}

	public void OnReceiveMesh(SocketIOEvent evt) {
		
		Vector3[] sentNormals = ParseVector3(evt.data.GetField("normals").ToString());
		Vector3[] sentBoundsVectors = ParseVector3(evt.data.GetField("bounds").ToString());
		Bounds sentBounds = new Bounds();
		sentBounds.center = sentBoundsVectors[0];
		sentBounds.extents = sentBoundsVectors[1];
		int[] sentTriangles = ParseIntArray(evt.data.GetField("triangles").ToString());
		Vector2[] sentUV = ParseVector2(evt.data.GetField("uv").ToString());
		Vector3[] sentVertices = ParseVector3(evt.data.GetField("vertices").ToString());

		FindObjectOfType<MeshManager>().SpawnAndModifyMesh(sentNormals, sentUV, sentTriangles);
		
		
	}
	void TestMeshData() {
		Vector3[] sentNormals = ParseVector3(testData.GetField("normals").ToString());
		Vector3[] sentBoundsVectors = ParseVector3(testData.GetField("bounds").ToString());
		Bounds sentBounds = new Bounds();
		sentBounds.center = sentBoundsVectors[0];
		sentBounds.extents = sentBoundsVectors[1];
		int[] sentTriangles = ParseIntArray(testData.GetField("triangles").ToString());
		Vector2[] sentUV = ParseVector2(testData.GetField("uv").ToString());
		Vector3[] sentVertices = ParseVector3(testData.GetField("vertices").ToString());

		FindObjectOfType<MeshManager>().SpawnAndModifyMesh(sentVertices, sentUV, sentTriangles);
		

	}

	Vector3[] ParseVector3(string str) {
		char[] charSeparators = new char[] {':'};
		List<Vector3> vectors = new List<Vector3>();
		string[] split1 = str.Split(charSeparators, System.StringSplitOptions.RemoveEmptyEntries);
		foreach(string vector in split1) {
			string trimmedVector = vector.Trim(new char[] {'(',')','"'});
			charSeparators = new char[] {','};
			string[] split2 = trimmedVector.Split(charSeparators, System.StringSplitOptions.RemoveEmptyEntries);
			Vector3 newVector = new Vector3();
			for(int i = 0; i < split2.Length; i++) {
				float value = float.Parse(split2[i].Trim());
				switch(i) {
					case 0: newVector.x = value; break;
					case 1: newVector.y = value; break;
					case 2: newVector.z = value; break;
				}
			}
			vectors.Add(newVector);
		}
		return vectors.ToArray();
	}
	Vector2[] ParseVector2(string str) {
		char[] charSeparators = new char[] {':'};
		List<Vector2> vectors = new List<Vector2>();
		string[] split1 = str.Split(charSeparators, System.StringSplitOptions.RemoveEmptyEntries);
		foreach(string vector in split1) {
			string trimmedVector = vector.Trim(new char[] {'(',')', '"'});
			charSeparators = new char[] {','};
			string[] split2 = trimmedVector.Split(charSeparators, System.StringSplitOptions.RemoveEmptyEntries);
			Vector2 newVector = new Vector2();
			for(int i = 0; i < split2.Length; i++) {
				float value = float.Parse(split2[i].Trim());
				switch(i) {
					case 0: newVector.x = value; break;
					case 1: newVector.y = value; break;
				}
			}
			vectors.Add(newVector);
		}
		return vectors.ToArray();
	}
	int[] ParseIntArray(string str) {
		str = str.Trim(new char[] {'"'});
		char[] charSeparators = new char[] {','};
		List<int> intArr = new List<int>();
		string[] split1 = str.Split(charSeparators, System.StringSplitOptions.RemoveEmptyEntries);
		foreach(string val in split1) {
			Debug.LogWarning(val);
			int value = int.Parse(val);
			intArr.Add(value);
		}
		return intArr.ToArray();
	}
	public string Vector3ArrayToString(Vector3[] v) {
		string s = "";
		foreach(Vector3 vect in v) {
			s += vect.ToString();
			s += ":";
		}
		return s;
	}
	public string Vector2ArrayToString(Vector2[] v) {
		string s = "";
		foreach(Vector2 vect in v) {
			s += vect.ToString();
			s += ":";
		}
		return s;
	}
	public string IntArrayToString(int[] a) {
		string s = "";
		foreach(int i in a) {
			s += i.ToString();
			s += ",";
		}
		return s;
	}
	public string BoundsToString(Bounds b) {
		string s = "";
		s += b.center.ToString() + ":" + b.extents.ToString();
		return s;
	}
}
