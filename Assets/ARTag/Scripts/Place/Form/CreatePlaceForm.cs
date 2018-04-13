
namespace ARTag
{
    using System.Collections;
    using System;
    using System.IO;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using TMPro;
    using SocketIOManager;
    using SocketIO;

    public class CreatePlaceForm : MonoBehaviour
    {

        SocketManager socketManager;
        TemporaryDataManager tempManager;
        
        const string FIELD_NAME = "name";
        const string FIELD_DESCRIPTOPN = "description";
        const string FIELD_THUMBNAIL = "thumbnail";
        const string FIELD_IS_PUBLIC = "isPublic";
        const string FIELD_WIDTH = "width";
        const string FIELD_HEIGHT = "height";
        const string FIELD_IMAGE = "image";

        public GameObject placeNameText, placeDescriptionText, publicToggle, thumbnailPicker, creatingPanel, preloader;

        void Start()
        {
            socketManager = GameObject.Find(ObjectsCollector.SOCKETIO_MANAGER_OBJECT).GetComponent<SocketManager>();
            tempManager = GameObject.FindObjectOfType<TemporaryDataManager>();
            socketManager.On(EventsCollector.PLACE_CREATE_SUCCESS, OnPlaceCreateSuccess);
            socketManager.On(EventsCollector.PLACE_CREATE_ERROR, OnPlaceCreateError);
        }

        #region Form
        public void SubmitForm()
        {
            StartCoroutine(CreatePlace());
        }

        IEnumerator CreatePlace()
        {
            creatingPanel.SetActive(true);
            yield return new WaitUntil(() => creatingPanel.activeSelf);
            string name = placeNameText.GetComponent<InputField>().text;
            string description = placeDescriptionText.GetComponent<TMP_InputField>().text;
            bool isPublic = publicToggle.GetComponent<Toggle>().isOn;
            ImageData thumbnail = thumbnailPicker.GetComponent<ImagePicker>().selectedImage;
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
        }

        public void OnPlaceCreateSuccess(SocketIOEvent e)
        {
            int timestamp = (int) e.data.GetField("place").GetField("timestamp").n;
            string name = e.data.GetField("place").GetField("name").str;
            string significant = timestamp + "-" + name;
            tempManager.Put("significant", significant);
            creatingPanel.GetComponentInChildren<Text>().text = "Place Creation Success!";
            preloader.SetActive(false);
        }

        public void OnPlaceCreateError(SocketIOEvent e)
        {
            creatingPanel.SetActive(false);
        }
#endregion
    }
}
