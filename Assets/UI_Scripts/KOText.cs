using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KOText : MonoBehaviour {
    CameraFunctions camFuncs;
    Animator animator;

    public Sprite timeOutText;
    public Sprite koText;

    bool koGraphic = true;

	// Use this for initialization
	void Start () {
        camFuncs = FindObjectOfType<CameraFunctions>();
        animator = GetComponent<Animator>();
	}
	
    public void shakeCamera() {
        camFuncs.shake(0.2f, 0.1f);
    }
	// Update is called once per frame
	void Update () {
		
	}

    //Switches between a ko or a time-out graphic
    public void switchGraphic() {
        if (koGraphic) {
            GetComponent<Image>().sprite = timeOutText;
            koGraphic = false;
        } else {
            GetComponent<Image>().sprite = koText;
            koGraphic = true;
        }
    }
   

    //Shows the KO Text
    public void display() {
        animator.SetTrigger("Appear");
        //print("TIME SCALE SLOWED");
        if (Random.value < 0) {
            Time.timeScale = 0.5f;
        } else {
            Time.timeScale = 0;
        }
    }

    public void endPause() {
        Time.timeScale = 1;
        //print("TIME SCALE Returned");
        StartCoroutine(endRound(3));
    }

    //Change this behaviour depending on if this is first round/second round/tie-breaker
    IEnumerator endRound(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if (RoundManager.player1Wins == 2 || RoundManager.player2Wins == 2) {
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
