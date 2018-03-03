using LoggingClient;
using UnityEngine;

public class ExampleScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        LogClient.instance.Log(LogClient.CLIENT_STATUS_TAG, "Test");		
	}
}
