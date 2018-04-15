
namespace ARTag
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RangeTrigger : MonoBehaviour
    {

        public float range;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Camera.main.transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * Time.deltaTime * 5f);
            Camera.main.transform.Translate(Input.GetAxis("Vertical") * Vector3.up * Time.deltaTime * 5f);
            RangeDetect();
        }

        void RangeDetect()
        {
            Debug.Log(Vector3.Distance(transform.position, Camera.main.transform.position));
            if (Vector3.Distance(transform.position, Camera.main.transform.position) < range)
            {
                Debug.Log("In");
            }
        }
    }

}
