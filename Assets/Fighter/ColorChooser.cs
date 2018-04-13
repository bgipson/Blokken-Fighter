using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChooser : MonoBehaviour {
    Material mat;
    public Color color;
    FighterController controller;
    public bool customizable = false;

    public Material normalMat;
    public Material burstMat;
    MeshRenderer ren;
	// Use this for initialization
	void Start () {
        controller = transform.root.GetComponent<FighterController>();
        if (controller) {
            ren = GetComponent<MeshRenderer>();


            if (!customizable) {
                if (controller.playerID == 1) {
                    ren.material.color = Color.red;
                } else {
                    ren.material.color = Color.blue;
                }
            }
        }
	}

    void Update() {
        if (controller) {
            if (controller.getBurst()) {
                ren.material = burstMat;

                if (controller.playerID == 1) {
                    ren.material.SetColor("_TintColor", Color.red);
                } else {
                    ren.material.SetColor("_TintColor", Color.blue);
                }

            } else {
                ren.material = normalMat;
                if (controller.playerID == 1) {
                    ren.material.color = Color.red;
                } else {
                    ren.material.color = Color.blue;
                }
            }
        }
    }

}
