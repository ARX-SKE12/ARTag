
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using Wikitude;

    public class MarkerRecognizer : MonoBehaviour
    {
        bool isTracking;
        List<Vector3> positions;
        ImageTarget currentTarget;
        TrackingMode mode = TrackingMode.ANY;
        string targetName;

        public enum TrackingMode
        {
            ANY = 0,
            EDITOR = 1,
            VISITOR = 2
        }

        const float MAXIMUM_ERROR = 0.1f;

        void Awake()
        {
            positions = new List<Vector3>();
            if (PlayerPrefs.HasKey("mode")) mode = (TrackingMode)PlayerPrefs.GetInt("mode");
            if (PlayerPrefs.HasKey("target")) targetName = PlayerPrefs.GetString("target");
        }

        void Update()
        {
            if (isTracking) TrackQuality();
        }

        public void OnTargetFound(ImageTarget target)
        {
            currentTarget = target;
            GameObject.Find(ObjectsCollector.CLOUD_STATUS_TEXT).GetComponent<Text>().text = target.Name+" Found!";
            if (mode == TrackingMode.ANY || targetName == target.Name) StartQualityTracking();
        }

        public void OnTargetLost(ImageTarget target)
        {
            GameObject.Find(ObjectsCollector.CLOUD_STATUS_TEXT).GetComponent<Text>().text = target.Name + " Lost!";
            currentTarget = null;
            StopQualityTracking();
        }

        void StartQualityTracking()
        {
            isTracking = true;
            positions.Clear();
        }

        void StopQualityTracking()
        {
            isTracking = false;
            GameObject.Find(ObjectsCollector.QUALITY_TRACKING_TEXT).GetComponent<Text>().text = "0";
        }

        void TrackQuality()
        {
            Vector3 position = Camera.main.transform.position;
            if (positions.Count == 0) positions.Add(position);
            if (isNotMuchDiff(position))
            {
                positions.Add(position);
                if (positions.Count > 20)
                {
                    // Note: Position of Wikitude is different from ARCore
                    PlayerPrefs.SetFloat("x", position.x / 100f);
                    PlayerPrefs.SetFloat("y", position.y / 100f);
                    PlayerPrefs.SetFloat("z", position.z / 100f);
                    SceneManager.LoadScene("HelloAR");
                }
                GameObject.Find(ObjectsCollector.QUALITY_TRACKING_TEXT).GetComponent<Text>().text = position.ToString()+"\n"+currentTarget.Name + "\n" + positions.Count;
            }
            else positions.Clear();
        }

        Vector3 GetAverage()
        {
            float x = 0;
            float y = 0;
            float z = 0;
            int length = positions.Count;
            foreach (Vector3 position in positions)
            {
                x += position.x / length;
                y += position.y / length;
                z += position.z / length;
            }
            return new Vector3(x, y, z);
        }

        bool isNotMuchDiff(Vector3 position)
        {
            Vector3 diff = position - GetAverage();
            return Mathf.Abs(diff.x) <= MAXIMUM_ERROR && Mathf.Abs(diff.y) <= MAXIMUM_ERROR && Mathf.Abs(diff.z) <= MAXIMUM_ERROR;
        }

    }

}
