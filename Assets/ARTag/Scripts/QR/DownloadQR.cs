
namespace ARTag
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using System.IO;

    public class DownloadQR : MonoBehaviour
    {

        public void Download()
        {
            byte[] bytes = GameObject.FindObjectOfType<LoadQRFromServer>().source.EncodeToPNG();
            string fileName = GameObject.FindObjectOfType<TemporaryDataManager>().Get("significant") + ".png";
            NativeGallery.SaveImageToGallery(bytes, Application.productName, fileName);
        }

    }

}
