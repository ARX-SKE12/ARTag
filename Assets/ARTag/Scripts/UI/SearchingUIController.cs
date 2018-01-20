using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingUIController : MonoBehaviour {

    public GameObject searchingUI, environmentController;

    void Start()
    {
        environmentController.GetComponent<EnvironmentController>().register(gameObject);
    }

	void OnSearchingStateChange(bool isSearching)
    {
        searchingUI.SetActive(isSearching);
    }
}
