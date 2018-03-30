using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrainingMoveChanger : MonoBehaviour {
    public Slider firstOption;
    public FighterController player1;
    public FighterController player2;

    public OutlineText player2Status;

    Animator animator;

    string[] styles = { "STANDARD", "KICKER", "BERSERKER" };
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        player2Status.mainText.text = "Player 2: IDLE";
        sliders = GetComponentsInChildren<Slider>();
	}

    void toggleSlider(bool toggle) {
        foreach (Slider slider in sliders) {
            slider.interactable = toggle;
        }
    }

    public bool activated = false;
    public EventSystem eventSystem;
    Slider[] sliders;
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Select_1P")) {
            if (!activated) {
                activated = true;
                player1.controllable = false;
                toggleSlider(activated);
                eventSystem.SetSelectedGameObject(firstOption.gameObject);
            } else {
                activated = false;
                player1.controllable = true;
                toggleSlider(activated);
            }
            animator.SetBool("Active", activated);
        }

        if (Input.GetButtonDown("Start_1P")) {
            activated = false;
            player1.controllable = true;
            toggleSlider(activated);
            animator.SetBool("Active", activated);
        }
	}

    public void changeMoveText(Slider slider) {
        slider.GetComponentInChildren<Text>().text = styles[Mathf.FloorToInt(slider.value)];
    }

    public void changeMove(Slider s) {
        switch (s.name) {
            case "jab1":
                player1.jab_1_num = Mathf.FloorToInt(s.value);
                player1.getAnimator().SetInteger("Jab_1_Num", player1.jab_1_num);
                break;
            case "jab2":
                player1.jab_2_num = Mathf.FloorToInt(s.value);
                player1.getAnimator().SetInteger("Jab_2_Num", player1.jab_2_num);
                break;
            case "jab3":
                player1.jab_3_num = Mathf.FloorToInt(s.value);
                player1.getAnimator().SetInteger("Jab_3_Num", player1.jab_3_num);
                break;
            case "up tilt":
                player1.up_tilt_num = Mathf.FloorToInt(s.value);
                player1.getAnimator().SetInteger("Up_Tilt_Num", player1.up_tilt_num);
                break;
            case "up air":
                player1.up_air_num = Mathf.FloorToInt(s.value);
                player1.getAnimator().SetInteger("Up_Air_Num", player1.up_air_num);
                break;
            case "down air":
                player1.down_air_num = Mathf.FloorToInt(s.value);
                player1.getAnimator().SetInteger("Down_Air_Num", player1.down_air_num);
                break;
            default:
                break;
        }
    }
}
