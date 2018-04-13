
namespace ARTag
{
    using System;
    using System.IO;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class ImagePicker : MonoBehaviour
    {
        public string selectedImage;
        public GameObject thumbnail, background;
        readonly Vector2 MIDDLE_POSITION = new Vector2(0.5f, 0.5f);

        public void PickImage()
        {
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
            Texture2D image = new Texture2D(1, 1);
            image.LoadImage(rawFile);
            image.Compress(false);
            selectedImage = Convert.ToBase64String(image.EncodeToPNG());
            Rect spriteRect = new Rect(Vector2.zero, new Vector2(image.width, image.height));
            Sprite imageSprite = Sprite.Create(image, spriteRect, MIDDLE_POSITION);
            thumbnail.GetComponent<Image>().sprite = imageSprite;
            background.GetComponent<Image>().sprite = imageSprite;
        }
    }

}
