using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour {
    public int playerID = 0;
    FighterController targetPlayer;
    public Image healthBar;

	// Use this for initialization
	void Start () {
        FighterController[] fighters = FindObjectsOfType<FighterController>();
        foreach (FighterController fighter in fighters) {
            if (playerID == fighter.playerID) {
                targetPlayer = fighter;
            }
        }

        if (targetPlayer == null) {
            enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetPlayer.health / 100f, 0.1f);
	}
}
