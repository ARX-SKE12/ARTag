﻿
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using GoogleARCore;
    using ARCoreToolkit;
    using SocketIOManager;
    using SocketIO;

    public class PlaneEditor : MonoBehaviour
    {
        public GameObject scanButton, pauseButton, notification, errorNotification;
        Calibrator calibration;
        bool isScanning;

        // Use this for initialization
        void Start()
        {
            GameObject.FindObjectOfType<SocketManager>().On(EventsCollector.PLANE_UPGRADE, OnPlaneUpdate);
            GameObject.FindObjectOfType<SocketManager>().On(EventsCollector.PLANE_ERROR, OnPlaneUpdateError);
            calibration = GameObject.FindObjectOfType<Calibrator>();
        }

        public void ChangeScanningState()
        {
            isScanning = !isScanning;
            pauseButton.SetActive(isScanning);
            scanButton.SetActive(!isScanning);
            GameObject.FindObjectOfType<ARCoreSession>().SessionConfig.EnablePlaneFinding = isScanning;
            GameObject.FindObjectOfType<ARCoreSession>().OnEnable();
        }

        public void UpdatePlaneData()
        {
            string id = ((Place)GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace")).id;
            PlaneBehaviour[] planes = GameObject.FindObjectsOfType<PlaneBehaviour>();
            JSONObject data = new JSONObject();
            JSONObject[] planesData = new JSONObject[planes.Length];
            for (int i = 0; i < planes.Length; i++)
            {
                JSONObject planeData = new JSONObject();
                Mesh mesh = planes[i].GetComponent<MeshFilter>().mesh;
                int[] indices = mesh.GetIndices(0);
                JSONObject[] indicesData = new JSONObject[indices.Length];
                for (int j = 0; j < indices.Length; j++)
                {
                    indicesData[j] = new JSONObject(indices[j]);
                }
                planeData.AddField("indices", new JSONObject(indicesData));
                List<Vector3> vertices = new List<Vector3>();
                mesh.GetVertices(vertices);
                JSONObject[] verticesData = new JSONObject[vertices.Count];
                for (int j = 0; j < vertices.Count; j++)
                {
                    JSONObject verticeData = new JSONObject();
                    Vector3 vertice = vertices[j];
                    Vector3 realWorldVertices = GameObject.FindObjectOfType<Calibrator>().GetRealWorldPosition(vertices[j]);
                    verticeData.SetField("x", realWorldVertices.x);
                    verticeData.SetField("y", realWorldVertices.y);
                    verticeData.SetField("z", realWorldVertices.z);
                    verticesData[j] = verticeData;
                }
                planeData.AddField("vertices", new JSONObject(verticesData));
                planesData[i] = planeData;
            }
            data.AddField("id", id);
            data.SetField("data", new JSONObject(planesData));
            GameObject.FindObjectOfType<SocketManager>().Emit(EventsCollector.PLANE_UPDATE, data);
        }

        public void Disable()
        {
            isScanning = false;
            pauseButton.SetActive(false);
            scanButton.SetActive(true);
            GameObject.FindObjectOfType<ARCoreSession>().SessionConfig.EnablePlaneFinding = isScanning;
            GameObject.FindObjectOfType<ARCoreSession>().OnEnable();
        }

        public void OnPlaneUpdate(SocketIOEvent e)
        {
            JSONObject placeData = e.data.GetField("place");
            Place place = new Place(placeData);
            GameObject.FindObjectOfType<TemporaryDataManager>().Put("currentPlace", place);
            notification.GetComponentInChildren<Text>().text = "Plane has updated!";
            notification.SetActive(true);
        }

        public void OnPlaneUpdateError(SocketIOEvent e)
        {
            errorNotification.GetComponentInChildren<Text>().text = "Plane Updating Process is failure!";
            errorNotification.SetActive(true);
        }
    }

}
