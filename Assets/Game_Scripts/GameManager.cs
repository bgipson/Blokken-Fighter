using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    bool timerStarted;
    bool matchStarted = true;
    float timer = 60;

    public OutlineText timerText;
    public bool hitFreeze = false;          //Short pause after every hit (a la, Street Fighter)
    FighterController player1;
    FighterController player2;

    public Animator combo1Animator;
    public Animator combo2Animator;

    OutlineText combo1Text;
    OutlineText combo2Text;
    KOText koText;

    public Image player1Win_1;
    public Image player1Win_2;
    public Image player2Win_1;
    public Image player2Win_2;

    public bool trainingMode = false;

    // Use this for initialization
    void Start () {
        startTimer();
        FighterController[] fighters = FindObjectsOfType<FighterController>();
        player1 = fighters[0];
        player2 = fighters[1];
        if (fighters[1].playerID < player1.playerID) {
            player1 = fighters[1];
            player2 = fighters[0];
        }

        if (RoundManager.player1Wins == 1) {
            player1Win_1.fillAmount = 1;
            player1Win_2.fillAmount = 0;
        } else if (RoundManager.player1Wins == 0) {
            player1Win_1.fillAmount = 0;
            player1Win_2.fillAmount = 0;
        } else {
            player1Win_1.fillAmount = 0;
            player1Win_2.fillAmount = 0;
        }

        if (RoundManager.player2Wins == 1) {
            player2Win_1.fillAmount = 1;
            player2Win_2.fillAmount = 0;
        } else if (RoundManager.player2Wins == 0) {
            player2Win_1.fillAmount = 0;
            player2Win_2.fillAmount = 0;
        } else {
            player2Win_1.fillAmount = 0;
            player2Win_2.fillAmount = 0;
        }



        combo1Text = combo1Animator.gameObject.GetComponentInChildren<OutlineText>();
        combo2Text = combo2Animator.gameObject.GetComponentInChildren<OutlineText>();

        koText = FindObjectOfType<KOText>();
	}
	
	// Update is called once per frame
	void Update () {
        //Handles the match timer

        if (!trainingMode) {
            int time = Mathf.RoundToInt(timer);
            if (time <= 0 && matchStarted) {
                time = 0;
                timerStarted = false;
                timerText.mainText.text = 0.ToString();
                koText.switchGraphic();
                koText.display();
                matchStarted = false;
            } else {
                timerText.mainText.text = Mathf.RoundToInt(timer).ToString();
                if (timerStarted) {
                    timer = timer - (Time.deltaTime);
                }
            }
        }

        //Handles match reset
        //Check for if a player is dead
        if (matchStarted && (player1.health <= 0 || player2.health <= 0)) {
            koText.display();
            matchStarted = false;
            if (player1.health <= 0) {
                RoundManager.player2Wins += 1;
            }
            if (player2.health <= 0) {
                RoundManager.player1Wins += 1;
            }
            print("MATCH ENDED");


            if (RoundManager.player1Wins == 1) {
                player1Win_1.fillAmount = 1;
                player1Win_2.fillAmount = 0;
            } else if (RoundManager.player1Wins == 2) {
                player1Win_1.fillAmount = 1;
                player1Win_2.fillAmount = 1;
            }

            if (RoundManager.player2Wins == 1) {
                player2Win_1.fillAmount = 1;
                player2Win_2.fillAmount = 0;
            } else if (RoundManager.player2Wins == 1) {
                player2Win_1.fillAmount = 1;
                player2Win_2.fillAmount = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    //Handles event of Combo increments
    public void comboInc(int playerID, int comboValue) {
        if (playerID == player1.playerID) {
            combo2Text.mainText.text = comboValue.ToString() + " HIT\nCOMBO!";
            combo2Animator.SetTrigger("Appear");
        } else if (playerID == player2.playerID) {
            combo1Text.mainText.text = comboValue.ToString() + " HIT\nCOMBO!";
            combo1Animator.SetTrigger("Appear");
        }
    }

    public void endCombo(int playerID) {
        if (playerID == player1.playerID) {
            combo2Animator.SetTrigger("Disappear");
        } else if (playerID == player2.playerID) {
            combo1Animator.SetTrigger("Disappear");
        }
    }

    //Round-Timer Functionality
    public void startTimer() {
        timerStarted = true;
    }

    public void stopTimer() {
        timerStarted = false;
    }


    
}
