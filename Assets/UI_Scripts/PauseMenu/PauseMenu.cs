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
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Start_1P")) {
            animator.SetTrigger("Start");
        }
	}

    public void resume() {
        animator.SetTrigger("Start");
    }

    public void returnToTitle() {
        animator.SetTrigger("Start");
        SceneManager.LoadScene(0);
    }

    public void restartMatch() {
        setEvent(resumeMatch);
        animator.SetTrigger("Start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public EventSystem eventSystem;
    public GameObject resumeMatch;
    public void setEvent(GameObject obj) {
        eventSystem.SetSelectedGameObject(obj);
    }
}
