using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {
    public Transform startingPoint;
    public Transform endPoint;
    public GameObject[] roads;
    public Camera mainCamera;
    public Camera cinematicCamera;
    public float roadSpeed = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F2)) {
            switchCamera();
        }

		foreach (GameObject road in roads) {
            Transform rTrans = road.transform;
            rTrans.position = new Vector3(rTrans.position.x - roadSpeed, rTrans.position.y, rTrans.position.z);
            if (rTrans.position.x < endPoint.position.x) {
                rTrans.position = new Vector3(startingPoint.position.x, rTrans.position.y, rTrans.position.z);
                road.GetComponent<StreetGenerator>().purge();
                road.GetComponent<StreetGenerator>().swap();
            }
        }
	}

    void switchCamera() {
        if (mainCamera.enabled == true) {
            cinematicCamera.enabled = true;
            mainCamera.enabled = false;
        } else {
            cinematicCamera.enabled = false;
            mainCamera.enabled = true;
        }
    }
}
