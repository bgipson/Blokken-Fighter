using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FullscreenMoving : MonoBehaviour {

    public GameObject dropdownGameObject;
    public Dropdown fullscreenDropdown;
    public GameObject fullscreenButton;
    public EventSystem eventSystem;

	// Use this for initialization
	void Start () {
        fullscreenDropdown = GetComponent<Dropdown>();
        if (Screen.fullScreen == true) { fullscreenDropdown.value = 0; }
        else { fullscreenDropdown.value = 1; }
	}
	
	// Update is called once per frame
	void Update () {
		if (eventSystem.currentSelectedGameObject == dropdownGameObject)
        {
            if (Input.GetAxis("Horizontal_1P") != 0 || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (fullscreenDropdown.value == 1) { fullscreenDropdown.value = 0; }
                else { fullscreenDropdown.value = 1; }
            }
            if (Input.GetAxis("Guard_1P") > 0)
            {
                eventSystem.SetSelectedGameObject(fullscreenButton);
            }
        }
	}
}
