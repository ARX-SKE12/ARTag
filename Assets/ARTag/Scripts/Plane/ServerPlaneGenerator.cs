
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;
    using SocketIO;
    using SocketIOManager;

    public class ServerPlaneGenerator : MonoBehaviour
    {

        public GameObject planePrefab;

        void Start()
        {
            GameObject.FindObjectOfType<SocketManager>().On(EventsCollector.PLANE_UPGRADE, OnPlaneUpdate);
            GeneratePlane();
        }

        public void GeneratePlane()
        {
            Place place = (Place) GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace");
            RestorePlanes(place);
        }

        void RestorePlanes(Place place)
        {
            List<Plane> planes = place.planes;
            foreach (ServerPlaneBahviour plane in GameObject.FindObjectsOfType<ServerPlaneBahviour>()) Destroy(plane.gameObject);
            foreach (Plane plane in planes)
            {
                ServerPlaneBahviour planeObject = Instantiate(planePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<ServerPlaneBahviour>();
                planeObject.Initialize(plane);
            }
        }
        
        public void OnPlaneUpdate(SocketIOEvent e)
        {
            JSONObject placeData = e.data.GetField("place");
            Place place = new Place(placeData);
            GameObject.FindObjectOfType<TemporaryDataManager>().Put("currentPlace", place);
            GeneratePlane();
        }
    }

}