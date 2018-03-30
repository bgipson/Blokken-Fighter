using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderMoving : MonoBehaviour {

    public GameObject volume;
    public GameObject volumeButton;
    public Slider volumeSlider;
    public EventSystem eventSystem;
    //public GameObject currentSelected;

	// Use this for initialization
	void Start () {
        volumeSlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        //currentSelected = eventSystem.currentSelectedGameObject;
        if (eventSystem.currentSelectedGameObject == volume)
        {
            if (Input.GetAxis("Horizontal_1P") < 0 || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                volumeSlider.value -= .2f;
            }
            if (Input.GetAxis("Horizontal_1P") > 0 || Input.GetKeyDown(KeyCode.RightArrow))
            {
                volumeSlider.value += .2f;
            }
            if (Input.GetAxis("Guard_1P") > 0)
            {
                eventSystem.SetSelectedGameObject(volumeButton);
            }
        }
	}
}
