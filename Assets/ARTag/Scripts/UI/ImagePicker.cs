
namespace ARTag
{
    using System;
    using System.IO;
    using System.Collections;
    using System.Threading;
    using UnityEngine;
    using UnityEngine.UI;

    public class ImagePicker : MonoBehaviour
    {
        public string selectedImage;
        public GameObject thumbnail, background;
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
            selectedImage = Convert.ToBase64String(image.EncodeToPNG());
            Rect spriteRect = new Rect(Vector2.zero, new Vector2(image.width, image.height));
            Sprite imageSprite = Sprite.Create(image, spriteRect, MIDDLE_POSITION);
            thumbnail.GetComponent<Image>().sprite = imageSprite;
            background.GetComponent<Image>().sprite = imageSprite;
            GetComponentInChildren<Text>().text = "Upload Thumbnail";
            isUploaded = false;
            uploadThread.Abort();
        }

        void Upload(string path)
        {
            rawFile = File.ReadAllBytes(path);
            isUploaded = true;
        }
    }

}
