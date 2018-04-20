
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine.UI;

    public class ImageCaptionTagBehaviour : ImageTagBehaviour
    {

        protected override JSONObject PrepareTagData(Dictionary<string, object> data)
        {
            JSONObject jsonData = base.PrepareTagData(data);
            jsonData.GetField("detail").AddField("title", (string)data["title"]);
            return jsonData;
        }

        public override void ConstructTag(JSONObject data)
        {
            base.ConstructTag(data);
            title.SetActive(true);
            title.GetComponent<Text>().text = GetTagData(data).GetField("detail").GetField("title").str;
        }
    }
}
