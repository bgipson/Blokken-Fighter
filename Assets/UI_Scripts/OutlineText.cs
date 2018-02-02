using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineText : MonoBehaviour {
    public Text mainText;
	// Use this for initialization
	void Start () {
        mainText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        Text[] textLayers = transform.GetComponentsInChildren<Text>();
        foreach (Text layer in textLayers) {
            layer.text = mainText.text;
        }
	}
}
