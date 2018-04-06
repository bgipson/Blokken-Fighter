using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour {
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetInteger("WinningPlayer", RoundManager.currentWinnerPlayer);

        FighterSetup[] setups = FindObjectsOfType<FighterSetup>();
        if (setups.Length > 0)
        {
            foreach (FighterSetup setup in setups)
            {
                Destroy(setup.gameObject);
            }
        }
    }

    public bool done = false;

    public void finished() {
        done = true;
    }

	// Update is called once per frame
	void Update () {
		if (done && Input.anyKeyDown) {
            RoundManager.total_p1_combos = 0;
            RoundManager.total_p2_combos = 0;
            RoundManager.max_p1_combo = 0;
            RoundManager.max_p2_combo = 0;
            RoundManager.hits_taken_p1 = 0;
            RoundManager.hits_taken_p2 = 0;
            SceneManager.LoadScene(0);
        } else if (done) {
            processP1();
            processP2();
            set_P1_wins();
            set_P2_wins();
        }

        if (Input.anyKeyDown) {
            animator.SetTrigger("Skip");
        }
	}

    public Text p1_details;
    int p1_damage = 999;
    int p1_total_combo = 999;
    int p1_max_combo = 999;

    public void processP1() {
        int damage = RoundManager.hits_taken_p1;
        int max_combo = RoundManager.max_p1_combo;
        int total_combo = RoundManager.total_p1_combos;
        

        p1_damage = Mathf.Clamp(p1_damage - Random.Range(5, 10), damage, 999);
        p1_total_combo = Mathf.Clamp(p1_total_combo - Random.Range(5, 10), total_combo, 999);
        p1_max_combo = Mathf.Clamp(p1_max_combo - Random.Range(5, 10), max_combo, 999);

        p1_details.text = p1_damage.ToString() + "\n" + p1_total_combo.ToString() + "\n" + p1_max_combo.ToString();
    }

    public Text p2_details;
    int p2_damage = 999;
    int p2_total_combo = 999;
    int p2_max_combo = 999;
    public void processP2() {
        int damage = RoundManager.hits_taken_p2;
        int max_combo = RoundManager.max_p2_combo;
        int total_combo = RoundManager.total_p2_combos;

        p2_damage = Mathf.Clamp(p2_damage - Random.Range(5,10), damage, 999);
        p2_total_combo = Mathf.Clamp(p2_total_combo - Random.Range(5, 10), total_combo, 999);
        p2_max_combo = Mathf.Clamp(p2_max_combo - Random.Range(5, 10), max_combo, 999);

        p2_details.text = p2_damage.ToString() + "\n" + p2_total_combo.ToString() + "\n" + p2_max_combo.ToString();
    }

    public Text p1_wins;
    int tot_p1 = 999;
    public void set_P1_wins() {
        tot_p1 = Mathf.Clamp(tot_p1 - 10, RoundManager.total_p1_wins, tot_p1);
        p1_wins.text = tot_p1.ToString();
    }

    public Text p2_wins;
    int tot_p2 = 999;
    public void set_P2_wins() {
        tot_p2 = Mathf.Clamp(tot_p2 - 10, RoundManager.total_p2_wins, tot_p2);
        p2_wins.text = tot_p2.ToString();
    }

}
