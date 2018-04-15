
namespace JustAQRScanner
{

    using System;
    using System.Collections;
    using UnityEngine;
    using ZXing;
    using UnityEngine.UI;
    using PublisherKit;

    public class QRReader : Publisher
    {

        WebCamTexture camTexture;

        IEnumerator Start()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (Application.HasUserAuthorization(UserAuthorization.WebCam)) InitializeCamera();
        }

        private void Update()
        {
            UpdateCamera();
        }

        void OnGUI()
        {
            ApplyCameraTexture();
            ReadQR();
        }

        void InitializeCamera()
        {
            camTexture = new WebCamTexture();
            camTexture.filterMode = FilterMode.Bilinear;
            if (camTexture != null)
            {
                camTexture.Play();
            }
        }

        void UpdateCamera()
        {
            /** Credit https://bit.ly/2ImTiPm **/
            if (camTexture.width < 100) return;

            RectTransform rectTransform = GetComponent<RectTransform>();
            AspectRatioFitter aspectRatioFitter = GetComponent<AspectRatioFitter>();
            RawImage rawImage = GetComponent<RawImage>();

            int cwNeeded = camTexture.videoRotationAngle;
            int ccwNeeded = -cwNeeded;
            if (camTexture.videoVerticallyMirrored) ccwNeeded += 180;

            rectTransform.localEulerAngles = new Vector3(0f, 0f, ccwNeeded);

            float videoRatio = (float)camTexture.width / (float)camTexture.height;

            aspectRatioFitter.aspectRatio = videoRatio;

            if (camTexture.videoVerticallyMirrored) rawImage.uvRect = new Rect(1, 0, -1, 1);
            else rawImage.uvRect = new Rect(0, 0, 1, 1);
        }

        void ApplyCameraTexture()
        {
            GameObject.FindObjectOfType<RawImage>().texture = camTexture;
        }

        void ReadQR()
        {
            /** Credit https://bit.ly/2GSQYCE **/
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();

                Result result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
                if (result != null)
                {
                    Broadcast("OnQRDetect", result.Text);
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
        }
    }

}
