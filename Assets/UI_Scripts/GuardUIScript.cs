using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardUIScript : MonoBehaviour {
public int playerID = 0;
    FighterController targetPlayer;
    public Image guardBar;
    public Sprite changing;
    public Sprite regular;
    public Color regularColor;
    public Color breakColor;

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
        guardBar.fillAmount = Mathf.Lerp(guardBar.fillAmount, targetPlayer.guardMeter / 100f, 0.1f);

        if (Mathf.Abs(guardBar.fillAmount - (targetPlayer.guardMeter / 100f)) > 0.1f) {
            guardBar.sprite = changing;
        } else {
            guardBar.sprite = regular;
        }

        if (targetPlayer.getGuardBreak()) {
            guardBar.color = breakColor;
        } else {
            guardBar.color = regularColor;
        }
	}
}
