
namespace ARTag
{
    
    using UnityEngine;

    public class ServerPlaneBahviour : MonoBehaviour
    {

        public void Initialize(Plane plane)
        {
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            mesh.SetIndices(plane.indices, MeshTopology.Triangles, 0);
            mesh.SetVertices(plane.vertices);

        }
    }

}
