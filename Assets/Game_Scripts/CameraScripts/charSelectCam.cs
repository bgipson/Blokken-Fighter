using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charSelectCam : MonoBehaviour {
    public GameObject target;
    public float rotationSpeed = 0.01f;
    public bool reverseDir = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (reverseDir) {
            transform.RotateAround(target.transform.position, new Vector3(0, -1, 0), rotationSpeed);
        } else {
            transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), rotationSpeed);
        }
	}
}
