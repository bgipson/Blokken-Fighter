using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour {
    public GameObject centerStage;
    Vector3 originalPosition;

    public GameObject Player1;
    public GameObject Player2;

    public int zoomOutDist = 12;

    Camera cam;
    bool shaking = false;
    GameObject target;      //The object to focus on
    bool focusing = false;  //If set, focus on target object

    Vector3 orig;           //The position to return to after focusing is done
    float origOrthoSize;    //The default Ortho size

    float yFactor;          //Centers the y of the camera, so it doesn't go below plane

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        origOrthoSize = cam.orthographicSize;
        originalPosition = transform.position;
        focus(GameObject.FindGameObjectWithTag("Player"));

    }

    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F5)) {
            focus(GameObject.FindGameObjectWithTag("Player"));
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            print(Screen.width + " , " + Screen.height);
        }
        //Calculate the Target:
        if (Player1 && Player2) {
            if (focusing) {
                float dist = Mathf.Abs(Player1.transform.position.x - Player2.transform.position.x);

                if (dist > zoomOutDist) {
                    transform.position = Vector3.Slerp(transform.position, new Vector3(centerStage.transform.position.x, target.transform.position.y + yFactor, centerStage.transform.position.z), 0.02f);
                } else {
                    transform.position = Vector3.Slerp(transform.position, new Vector3((Player1.transform.position.x + Player2.transform.position.x) / 2, Mathf.Min(originalPosition.y + 2.5f, target.transform.position.y + yFactor), originalPosition.z), 0.02f);
                }
            }
        }
	}

    public void shake(float time, float intensity = 1, float timeGap = 0.01f) {
        if (!shaking) {
            StartCoroutine(shakeIE(time, intensity, timeGap));
        }
    }

    IEnumerator shakeIE(float time, float intensity, float timeGap) {
        shaking = true;
        Vector3 originalPosition = transform.position;
        float timer = 0;
        while (timer < time) {
            timer += Time.deltaTime;
            transform.position = new Vector3(originalPosition.x + Random.Range(-intensity, intensity), originalPosition.y + Random.Range(-intensity, intensity), originalPosition.z);
            yield return new WaitForSeconds(timeGap);
        }
        shaking = false;
        transform.position = originalPosition;
    }

    public void focus(GameObject tar) {
        if (focusing == true) {
            focusing = false;
            transform.position = orig;
            cam.orthographicSize = origOrthoSize;
        } else {
            orig = transform.position;
            target = tar;
            focusing = true;
            yFactor = originalPosition.y - target.transform.position.y;
            cam.orthographicSize = 13f;
        }
    }
}
