
namespace ARTag
{
    using System.Collections.Generic;
    using System;
    using UnityEngine;
    public struct Place
    {
        public string id, name, description;
        public long timestamp;
        public bool isPublic, isActive;
        public List<Plane> planes;
        public User user;
        public List<string> users;
        
        public Place(JSONObject data)
        {
            id = data.GetField("id").str;
            name = data.GetField("name").str;
            description = data.GetField("description").str;
            isPublic = data.GetField("isPublic").b;
            isActive = data.GetField("isActive").b;
            timestamp = (long) Convert.ToInt64(data.GetField("timestamp").str);
            JSONObject planesField = data.GetField("planes");
            planes = new List<Plane>();
            for (int i = 0; i < planesField.list.Count; i++) planes.Add(new Plane(planesField[i]));
            user = new User(data.GetField("user"));
            users = new List<string>();
            JSONObject usersField = data.GetField("users");
            for (int i = 0; i < usersField.Count; i++) users.Add(usersField[i].str);
        }

    }

}
