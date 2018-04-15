
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ServerPlaneGenerator : MonoBehaviour
    {

        public GameObject planePrefab;

        public void GeneratePlane()
        {
            Place place = (Place) GameObject.FindObjectOfType<TemporaryDataManager>().Get("currentPlace");
            transform.localPosition = GameObject.FindObjectOfType<Calibration>().GetVirtualPosition(place.origin);
            transform.localRotation = GameObject.FindObjectOfType<Calibration>().GetVirtualRotation(place.originRotation);
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
        
    }

}