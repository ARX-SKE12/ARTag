
namespace ARTag
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using SocketIOManager;
    using SocketIO;

    public class ServerPlaneGenerator : MonoBehaviour
    {

        public GameObject planePrefab;

        // Use this for initialization
        void Start()
        {
            GameObject.FindObjectOfType<SocketManager>().On(EventsCollector.PLACE_RESPONSE_SIGNIFICANT, RetrievePlace);
            StartCoroutine(WaitAndSend());
        }

        // Update is called once per frame
        void Update()
        {
            
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

        public void RetrievePlace(SocketIOEvent e)
        {
            Debug.Log("IN");
            Place place = new Place(e.data);
            RestorePlanes(place);
        }
        
        IEnumerator WaitAndSend()
        {
            yield return new WaitForSeconds(2);
            string hash = "MTUyMjkwMTMxMzA1Ny1oZGhkZQ==";
            JSONObject data = new JSONObject();
            data.AddField("encodedSignificant", hash);
            Debug.Log("send");
            GameObject.FindObjectOfType<SocketManager>().Emit(EventsCollector.PLACE_RETRIEVE_SIGNIFICANT, data);
        }
    }

}