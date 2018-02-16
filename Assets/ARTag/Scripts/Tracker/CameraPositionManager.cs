
using UnityEngine;
using UnityEngine.UI;

public class CameraPositionManager : MonoBehaviour {
    public GameObject cube, cam,text;
    Vector3 location = Vector3.zero;
	// Use this for initialization
	void Start () {
        location = new Vector3(PlayerPrefs.GetFloat("x")/100, PlayerPrefs.GetFloat("z")/100, PlayerPrefs.GetFloat("y")/100);
        cube.transform.position = new Vector3(0,0,0) + location;

    }

    // Update is called once per frame
    void Update () {
        text.GetComponent<Text>().text = (cam.transform.position + location).ToString() + "\n" + cube.transform.position.ToString();

    }
}
