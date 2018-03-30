using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChooser : MonoBehaviour {
    Material mat;
    public Color color;
    FighterController controller;
    public bool customizable = false;
	// Use this for initialization
	void Start () {
        controller = transform.root.GetComponent<FighterController>();
        if (controller) {
            MeshRenderer ren = GetComponent<MeshRenderer>();
            if (!customizable) {
                if (controller.playerID == 1) {
                    ren.material.color = Color.red;
                } else {
                    ren.material.color = Color.blue;
                }
            }
        }
	}

}
