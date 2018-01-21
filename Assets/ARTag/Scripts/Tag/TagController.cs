using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class TagController : MonoBehaviour {
    
    public GameObject activePanel, taggingPanel, switchButton, tagName, firstPersonCamera;

    public GameObject tagPrefab;

    public void SwitchToTag()
    {
        activePanel.SetActive(false);
        switchButton.SetActive(false);
        taggingPanel.SetActive(true);
    }

    public void PlaceTag()
    {
        GameObject tag = Instantiate(tagPrefab, firstPersonCamera.transform.position+ firstPersonCamera.transform.forward, Quaternion.identity, transform);

        tag.GetComponentInChildren<TagBehaviour>().SetText(tagName.GetComponent<Text>().text);
        tagName.GetComponent<Text>().text = "";

        activePanel.SetActive(true);
        switchButton.SetActive(true);
        taggingPanel.SetActive(false);
    }
}
