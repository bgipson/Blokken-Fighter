using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFling : MonoBehaviour {
    bool flung = false;
    public int carId = 0;

    public AudioClip[] crashAudioClips;

    public void setID(int id) {
        carId = id;
    }

    float waitTime = 4f;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Truck" && !flung) {
            fling();
            flung = true;
        }
    }

    public void fling() {
        GetComponentInParent<StreetGenerator>().removeCar(carId);
        gameObject.transform.parent = null;
        Rigidbody rig = gameObject.AddComponent<Rigidbody>();
        ConstantForce force = gameObject.AddComponent<ConstantForce>();
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = crashAudioClips[Random.Range(0, crashAudioClips.Length)];
        audio.Play();

        force.force = new Vector3(0, -100, 0);
        rig.velocity = new Vector3(Random.Range(-40, 40), Random.Range(90, 130), 50);
        rig.angularVelocity = new Vector3(Random.Range(50, 100), Random.Range(0, 10));
        StartCoroutine(timedDestructor());
    }

    IEnumerator timedDestructor() {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
