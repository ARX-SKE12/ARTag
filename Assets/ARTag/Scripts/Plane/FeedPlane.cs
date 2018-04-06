
namespace ARTag
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using ARCoreToolkit;
    using SocketIOManager;

    public class FeedPlane : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdatePlaneData()
        {
            PlaneBehaviour[] planes = GameObject.FindObjectsOfType<PlaneBehaviour>();
            JSONObject data = new JSONObject();
            JSONObject[] planesData = new JSONObject[planes.Length];
            for (int i = 0;i< planes.Length; i++)
            {
                JSONObject planeData = new JSONObject();
                Mesh mesh = planes[i].GetComponent<MeshFilter>().mesh;
                int[] indices = mesh.GetIndices(0);
                JSONObject[] indicesData = new JSONObject[indices.Length];
                for (int j = 0;j < indices.Length;j++)
                {
                    indicesData[j] = new JSONObject(indices[j]);
                }
                planeData.AddField("indices", new JSONObject(indicesData));
                List<Vector3> vertices = new List<Vector3>();
                mesh.GetVertices(vertices);
                JSONObject[] verticesData = new JSONObject[vertices.Count];
                for (int j = 0; j < vertices.Count; j++)
                {
                    JSONObject verticeData = new JSONObject();
                    Vector3 vertice = vertices[j];
                    verticeData.SetField("x", vertice.x);
                    verticeData.SetField("y", vertice.y);
                    verticeData.SetField("z", vertice.z);
                    verticesData[j] = verticeData;
                }
                planeData.AddField("vertices", new JSONObject(verticesData));
                planesData[i] = planeData;
            }
            data.SetField("planes", new JSONObject(planesData));
            GameObject.FindObjectOfType<SocketManager>().Emit(EventsCollector.PLANE_UPDATE, data);
        }
        
    }

}
