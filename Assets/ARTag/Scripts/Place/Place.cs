
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public struct Place
    {
        public string id, name, description;
        public bool isPublic, isActive;
        public List<Plane> planes;
        
        public Place(JSONObject data)
        {
            id = data.GetField("id").str;
            name = data.GetField("name").str;
            description = data.GetField("description").str;
            isPublic = data.GetField("isPublic").b;
            isActive = data.GetField("isActive").b;
            JSONObject planesField = data.GetField("planes");
            planes = new List<Plane>();
            for (int i = 0; i < planesField.list.Count; i++) planes.Add(new Plane(planesField[i]));
        }

    }

}
