
namespace ARTag
{

    public struct User
    {
        public string id, name, profileURL;

        public User(JSONObject data)
        {
            id = data.GetField("id").str;
            name = data.GetField("name").str;
            profileURL = data.GetField("profilePictureURL").str;
        }

    }
}
