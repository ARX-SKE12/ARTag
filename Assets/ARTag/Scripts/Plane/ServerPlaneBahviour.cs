
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using ARCoreToolkit;

    public class ServerPlaneBahviour : MonoBehaviour
    {

        public void Initialize(Plane plane)
        {
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            List<Vector3> vertices = new List<Vector3>();
            foreach (Vector3 vertex in plane.vertices) vertices.Add(GameObject.FindObjectOfType<Calibrator>().GetVirtualPosition(vertex));
            mesh.SetIndices(plane.indices, MeshTopology.Triangles, 0);
            mesh.SetVertices(vertices);
            GetComponent<PlaneColliderBehaviour>().BroadcastMessage("OnUpdateMesh", mesh);
        }
    }

}
