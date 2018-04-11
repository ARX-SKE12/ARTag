
namespace ARTag
{
    using System.Collections;
    using System;
    using System.IO;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using SocketIOManager;
    using SocketIO;

    public class CreatePlaceForm : MonoBehaviour
    {

        SocketManager socketManager;
        TemporaryDataManager tempManager;

        string imageType;

        ImageStruct marker, thumbnail;

        const string PERMISSION_GRANTED = "Granted";
        const string MARKER = "Marker";
        const string THUMBNAIL = "Thumbnail";

        const string FIELD_NAME = "name";
        const string FIELD_DESCRIPTOPN = "description";
        const string FIELD_MARKER = "marker";
        const string FIELD_SIZE = "size";
        const string FIELD_THUMBNAIL = "thumbnail";
        const string FIELD_IS_PUBLIC = "isPublic";
        const string FIELD_WIDTH = "width";
        const string FIELD_HEIGHT = "height";
        const string FIELD_IMAGE = "image";

        const int WIDTH_TEXTURE = 800;
        const double MAX_UPLOAD_FILE_SIZE = 2e+6;

        readonly Vector2 MIDDLE_POSITION = new Vector2(0.5f, 0.5f);

        struct ImageStruct
        {
            public string data;
            public int width;
            public int height;
            public ImageStruct(string data,int width,int height)
            {
                this.data = data;
                this.width = width;
                this.height = height;
            }
        }

#region Unity Behavior
        void Start()
        {
            socketManager = GameObject.Find(ObjectsCollector.SOCKETIO_MANAGER_OBJECT).GetComponent<SocketManager>();
            tempManager = GameObject.FindObjectOfType<TemporaryDataManager>();
            socketManager.On(EventsCollector.PLACE_CREATE_SUCCESS, OnPlaceCreateSuccess);
            socketManager.On(EventsCollector.PLACE_CREATE_ERROR, OnPlaceCreateError);
        }
#endregion

        #region Image Picker
        public void PickImage(string type)
        {
            imageType = type;
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery(OnPickImage);
        }

        void OnPickImage(string path)
        {
            StartCoroutine(LoadImage(path));
        }

        IEnumerator LoadImage(string path)
        {
            string url = path;
            byte[] rawFile = File.ReadAllBytes(path);
            yield return rawFile;
            Texture2D image = new Texture2D(1,1);
            image.LoadImage(rawFile);
            image.Compress(false);
            Rect spriteRect = new Rect(Vector2.zero, new Vector2(image.width, image.height));
            Sprite imageSprite = Sprite.Create(image, spriteRect, MIDDLE_POSITION);
            if (image)
            {
                string imageBase64 = Convert.ToBase64String(image.EncodeToPNG());
                ImageStruct imgStruct = new ImageStruct(imageBase64, image.width, image.height);
                switch (imageType)
                {
                    case THUMBNAIL:
                        thumbnail = imgStruct;
                        GameObject.Find(ObjectsCollector.THUMBNAIL_IMAGE).GetComponent<Image>().sprite = imageSprite;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion


#region Form
        public void SubmitForm()
        {
            string name = GameObject.Find(ObjectsCollector.PLACE_NAME_FIELD).GetComponent<InputField>().text;
            string description = GameObject.Find(ObjectsCollector.PLACE_DESCRIPTION_FIELD).GetComponent<InputField>().text;
            bool isPublic = GameObject.Find(ObjectsCollector.PUBLIC_CHECK_BOX).GetComponent<Toggle>().isOn;
            JSONObject data = new JSONObject();
            data.AddField(FIELD_NAME, name);
            data.AddField(FIELD_DESCRIPTOPN, description);
            data.AddField(FIELD_IS_PUBLIC, isPublic);
            JSONObject thumbnailData = new JSONObject();
            thumbnailData.SetField(FIELD_WIDTH, thumbnail.width);
            thumbnailData.SetField(FIELD_HEIGHT, thumbnail.height);
            thumbnailData.AddField(FIELD_IMAGE, thumbnail.data);
            data.AddField(FIELD_THUMBNAIL, thumbnailData);
            socketManager.Emit(EventsCollector.PLACE_CREATE, data);
            GameObject.Find(ObjectsCollector.FORM_STATUS_TEXT).GetComponent<Text>().text = "Emitted";
        }

        public void OnPlaceCreateSuccess(SocketIOEvent e)
        {
            GameObject.Find(ObjectsCollector.ERROR_TEXT).GetComponent<Text>().color = new Color(1, 0, 0, 0);
            int timestamp = (int) e.data.GetField("place").GetField("timestamp").n;
            string name = e.data.GetField("place").GetField("name").str;
            string significant = timestamp + "-" + name;
            tempManager.Put("significant", significant);
            SceneManager.LoadScene("Draft QR Loader");
        }

        public void OnPlaceCreateError(SocketIOEvent e)
        {
            GameObject.Find(ObjectsCollector.ERROR_TEXT).GetComponent<Text>().text = "Error";
            GameObject.Find(ObjectsCollector.ERROR_TEXT).GetComponent<Text>().color = new Color(1, 0, 0, 1);
        }
#endregion
    }
}
