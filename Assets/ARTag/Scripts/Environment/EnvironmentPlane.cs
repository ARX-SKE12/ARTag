using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class EnvironmentPlane : MonoBehaviour {

    TrackedPlane trackedPlane;
    List<Vector3> previousVertices = new List<Vector3>();
    List<Vector3> vertices = new List<Vector3>();
    Vector3 center = new Vector3();
    List<int> indices = new List<int>();
    Mesh mesh;
    MeshRenderer meshRenderer;

    // Use this for initialization
    void Awake () {
        mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!transform.parent.GetComponent<EnvironmentController>().isPause)
        {
            if (trackedPlane != null)
            {
                meshRenderer.enabled = true;

                UpdateMeshIfNeeded();
            }
            else if (trackedPlane.SubsumedBy != null)
            {
                Destroy(gameObject);
            }
            else if (Frame.TrackingState != TrackingState.Tracking)
            {
                meshRenderer.enabled = false;
            }
        }
    }

    public void Initialize(TrackedPlane plane)
    {
        trackedPlane = plane;
        Update();
    }

    void UpdateMeshIfNeeded()
    {
        trackedPlane.GetBoundaryPolygon(vertices);

        if (!isSameVertices(previousVertices, vertices))
        {
            previousVertices.Clear();
            previousVertices.AddRange(vertices);

            center = trackedPlane.Position;

            int planePolygonCount = vertices.Count;
            
            const float featherLength = 0.2f;
            const float featherScale = 0.2f;

            for (int i = 0; i < planePolygonCount; ++i)
            {
                Vector3 v = vertices[i];
                Vector3 d = v - center;

                float scale = 1.0f - Mathf.Min(featherLength / d.magnitude, featherScale);
                vertices.Add((scale * d) + center);
            }

            indices.Clear();
            int firstOuterVertex = 0;
            int firstInnerVertex = planePolygonCount;

            for (int i = 0; i < planePolygonCount - 2; ++i)
            {
                indices.Add(firstInnerVertex);
                indices.Add(firstInnerVertex + i + 1);
                indices.Add(firstInnerVertex + i + 2);
            }

            for (int i = 0; i < planePolygonCount; ++i)
            {
                int outerVertex1 = firstOuterVertex + i;
                int outerVertex2 = firstOuterVertex + ((i + 1) % planePolygonCount);
                int innerVertex1 = firstInnerVertex + i;
                int innerVertex2 = firstInnerVertex + ((i + 1) % planePolygonCount);

                indices.Add(outerVertex1);
                indices.Add(outerVertex2);
                indices.Add(innerVertex1);

                indices.Add(innerVertex1);
                indices.Add(outerVertex2);
                indices.Add(innerVertex2);
            }

            mesh.Clear();
            mesh.SetVertices(vertices);
            mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
        }
    }

    bool isSameVertices(List<Vector3> firstList, List<Vector3> secondList)
    {
        if (firstList.Count != secondList.Count) return false;
        for (int i = 0; i < firstList.Count; i++) 
            if (firstList[i] != secondList[i]) return false;
        return true;
    }
}
