using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetGenerator : MonoBehaviour {
    public Transform[] carPositions;
    GameObject[] cars;
    public GameObject[] carList;
    float trafficProbability = 3f;
	// Use this for initialization
	void Start () {
        cars = new GameObject[carPositions.Length];	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void swap() {
        foreach(Transform car in carPositions) {
            if (Random.Range(0, 100) < trafficProbability) {
                int pos = Random.Range(0, carPositions.Length);
                if (cars[pos] == null) {
                    GameObject newCar = Instantiate(carList[Random.Range(0, carList.Length)], carPositions[pos].position, Quaternion.Euler(0,90,0), gameObject.transform);
                    newCar.GetComponent<CarFling>().setID(pos);
                    cars[pos] = newCar;
                }
            }
        }
    }

    public void purge() {
        int i = 0;
        while (i < cars.Length) {
            Destroy(cars[i]);
            i++;
        }
    }

    public void removeCar(int id) {
        cars[id] = null;
    }
}
