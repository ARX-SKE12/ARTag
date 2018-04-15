
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;

    public struct Plane
    {
        public int[] indices;
        public List<Vector3> vertices;

        public Plane(JSONObject data)
        {
            JSONObject fieldIndices = data.GetField("indices");
            JSONObject fieldVertices = data.GetField("vertices");
            indices = new int[fieldIndices.list.Count];
            for (int i = 0; i < indices.Length; i++) indices[i] = (int) fieldIndices[i].n;
            vertices = new List<Vector3>();
            for (int i = 0; i < fieldVertices.list.Count; i++)
            {
                JSONObject vertexData = fieldVertices[i];
                float x = vertexData.GetField("x").f;
                float y = vertexData.GetField("y").f;
                float z = vertexData.GetField("z").f;
                vertices.Add(new Vector3(x, y, z));
            }
        }
    }

}
