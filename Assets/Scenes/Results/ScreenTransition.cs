using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenTransition : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if (RoundManager.getScreen() != null) {
            GetComponent<RawImage>().enabled = true;
            GetComponent<RawImage>().texture = RoundManager.getScreen();
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    
}
