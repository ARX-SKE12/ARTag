
namespace ARTag
{
    using UnityEngine;
    using Pathfinding;

    public class NavigationPather : MonoBehaviour
    {

        public float speed;
        Path path;
        int currentWaypoint;
        public float nextWaypointDistance = 1;
        Seeker seeker;
        public GameObject target;
        public GameObject origin;
        // Use this for initialization
        void Start()
        {
            seeker = GetComponent<Seeker>();
            initPath();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void initPath()
        {
            if (target == null)
            {
                Debug.Log("No target");
            }
            CalculatePath();
        }
        void CalculatePath()
        {
            AstarPath.active.Scan();
            Debug.Log(target.name);
            path = seeker.StartPath(transform.position, target.transform.position);
        }
        public void OnPathEnd()
        {
            Debug.Log("Found");
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            if (ps.isEmitting)
            {
                ps.Stop();
            }
            else if (ps.particleCount == 0)
            {
                Destroy(gameObject);
            }
            //Destroy(gameObject);
        }
        void FixedUpdate()
        {


            if (path == null)
            {
                //We have no path to move after yet
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                //Debug.Log("End Of Path Reached");
                OnPathEnd();
                return;
            }

            //Direction to the next waypoint
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= speed * Time.fixedDeltaTime;
            transform.Translate(dir);

            //Check if we are close enough to the next waypoint
            //If we are, proceed to follow the next waypoint
            if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }
        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        // /// <param name="other">The Collision data associated with this collision.</param>
        // void OnCollisionEnter(Collision other)
        // {
        // 	if(other.gameObject == target) Destroy(gameObject);	
        // }

    }

}
