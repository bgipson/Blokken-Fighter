using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStarter : MonoBehaviour {
    void Start() {
        GameManager manager = FindObjectOfType<GameManager>();
        if (manager && manager.trainingMode) {
            Destroy(gameObject);
        }
    }
	public void disableFight() {
        GameManager manager = FindObjectOfType<GameManager>();
        if (!manager.trainingMode) {
            manager.waitFight();
        }
    }

    public void startFight() {
        GameManager manager = FindObjectOfType<GameManager>();
        if (!manager.trainingMode) {
            manager.startFight();
        }
    }

}
