
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using SocketIOManager;
    using SocketIO;
    using UnityEngine.UI;

    public class TitleTagBehaviour : TagBehaviour
    {
        
        protected override JSONObject PrepareTagData(Dictionary<string, object> data)
        {
            JSONObject jsonData = base.PrepareTagData(data);
            JSONObject tagDetail = jsonData.GetField("detail");
            tagDetail.AddField("title", (string)data["title"]);
            return jsonData;
        }

        protected override void ConstructTag(JSONObject data)
        {
            base.ConstructTag(data);
            string title = data.GetField("tag").GetField("detail").GetField("title").str;
            GetComponentInChildren<Text>().text = title;
        }
    }

}
