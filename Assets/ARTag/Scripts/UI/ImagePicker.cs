﻿
namespace ARTag
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using PublisherKit;

    public class ImageData
    {
        public int width, height;
        public string data;
        public ImageData(int width, int height, string data)
        {
            this.width = width;
            this.height = height;
            this.data = data;
        }
    }

    public class ImagePicker : Publisher
    {
        public ImageData selectedImage;
        public GameObject thumbnail;
        readonly Vector2 MIDDLE_POSITION = new Vector2(0.5f, 0.5f);
        Thread uploadThread;
        byte[] rawFile;
        bool isUploaded;

        void Update()
        {
            if (isUploaded) UpdateImage();
        }

        public void PickImage()
        {
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery(OnPickImage);
        }

        void OnPickImage(string path)
        {
            GetComponentInChildren<Text>().text = "Uploading...";
            uploadThread = new Thread(new ThreadStart(() => Upload(path)));
            uploadThread.IsBackground = true;
            uploadThread.Start();
        }

        void UpdateImage()
        {
            Texture2D image = new Texture2D(1, 1);
            image.LoadImage(rawFile);
            AssignImage(image);
            GetComponentInChildren<Text>().text = "Upload Thumbnail";
            isUploaded = false;
            uploadThread.Abort();
        }

        void Upload(string path)
        {
            rawFile = File.ReadAllBytes(path);
            isUploaded = true;
        }

        public void SetValue(string url)
        {
            StartCoroutine(LoadImageFromURL(url));
        }

        IEnumerator LoadImageFromURL(string url)
        {
            WWW req = new WWW(url);
            yield return req;
            Texture2D image = req.texture;
            AssignImage(image);    
        }

        void AssignImage(Texture2D image)
        {
            string data = Convert.ToBase64String(image.EncodeToPNG());
            selectedImage = new ImageData(image.width, image.height, data);
            Rect spriteRect = new Rect(Vector2.zero, new Vector2(image.width, image.height));
            Sprite imageSprite = Sprite.Create(image, spriteRect, MIDDLE_POSITION);
            thumbnail.GetComponent<Image>().sprite = imageSprite;
            Broadcast("OnImageLoaded", imageSprite);
        }
    }

}
