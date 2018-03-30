using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        if (eventSystem == null) {
            eventSystem = FindObjectOfType<EventSystem>();
        }
        eventSystem.SetSelectedGameObject(resumeMatch);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Start_1P")) {
            animator.SetBool("Start", !animator.GetBool("Start"));
            eventSystem.SetSelectedGameObject(resumeMatch);
        }

        if (Input.GetButton("Select_1P")) {
            print(eventSystem.currentSelectedGameObject);
        }


	}

    public void resume() {
        animator.SetBool("Start", false);
        Time.timeScale = 1f;
    }

    public void returnToTitle() {
        animator.SetBool("Start", false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void restartMatch() {
        setEvent(resumeMatch);
        animator.SetBool("Start", false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        RoundManager.player1Wins = 0;
        RoundManager.player2Wins = 0;
    }

    public EventSystem eventSystem;
    public GameObject resumeMatch;
    public void setEvent(GameObject obj) {
        eventSystem.SetSelectedGameObject(obj);
    }
}
