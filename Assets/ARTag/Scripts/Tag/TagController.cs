using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class TagController : MonoBehaviour {
    
    enum State
    {
        ACTIVE,
        TAGGING
    }

    State currentState;

    public GameObject activePanel, taggingPanel, switchButton, tagName, camera;

    public GameObject tagPrefab;
    
    void Awake () {
        currentState = State.ACTIVE;    	
	}

    public void SwitchToTag()
    {
        activePanel.SetActive(false);
        switchButton.SetActive(false);
        taggingPanel.SetActive(true);
        currentState = State.TAGGING;
    }

    public void PlaceTag()
    {
        GameObject tag = Instantiate(tagPrefab, camera.transform.position+camera.transform.forward, Quaternion.identity, transform);

        tag.GetComponentInChildren<TagBehaviour>().SetText(tagName.GetComponent<Text>().text);

        activePanel.SetActive(true);
        switchButton.SetActive(true);
        taggingPanel.SetActive(false);
        currentState = State.ACTIVE;
    }
}
