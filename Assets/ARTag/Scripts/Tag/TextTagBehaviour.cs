﻿
namespace ARTag {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class TextTagBehaviour : TitleTagBehaviour
    {

        public GameObject description;

        protected override JSONObject PrepareTagData(Dictionary<string, object> data)
        {
            JSONObject jsonData = base.PrepareTagData(data);
            JSONObject detail = jsonData.GetField("detail");
            detail.AddField("description", TextProcessor.ConvertFromNewLine(((string) data["description"])));
            jsonData.SetField("detail", detail);
            return jsonData;
        }

        protected override void ConstructTag(JSONObject data)
        {
            base.ConstructTag(data);
            description.GetComponent<Text>().text = TextProcessor.ConvertToNewLine(data.GetField("tag").GetField("detail").GetField("description").str);
        }
    }

}