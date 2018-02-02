using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {
    public float time = 0.5f;
    public bool startOnAwake = true;

	// Use this for initialization
	void Start () {
		if (startOnAwake) { triggerDestroy(); }
	}

    public void triggerDestroy() {
        StartCoroutine(waitDestroy(time));
    }
    IEnumerator waitDestroy(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
