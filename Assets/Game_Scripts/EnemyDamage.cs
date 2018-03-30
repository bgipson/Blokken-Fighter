using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {
    CameraFunctions camFuncs;       //Manipulates the camera (i.e, shaking the camera)
    FighterController controller;
    Rigidbody rig;
    public GameObject hitfx;               //Particles when you are hit.
    public int playerID = 0;        //If ID is '-1', then the object is a training dummy and not a player
    public bool invincible = false;      //Toggle this for invincibility frames
    Animator animator;

    public bool debugLaunch = false;

    public bool training = false;
    

	// Use this for initialization
	void Start () {
        camFuncs = FindObjectOfType<CameraFunctions>();
        controller = GetComponent<FighterController>();
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        manager = FindObjectOfType<GameManager>();

        if (controller != null) {
            playerID = controller.playerID;
        } else {
            playerID = -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1)) {
            print("SHAKE");
            camFuncs.shake(0.1f, 0.08f);
        }
        
        deathCheck();
        comboTime();
	}

    void deathCheck() {
        if (controller.health <= 0) {
            animator.SetBool("Dead", true);
        }
    }

    //Handles when pressed against a wall
    public bool touchingWall = false;
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Wall") {
            //touchingWall = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Wall") {
            //touchingWall = false;
        }
    }

    //When a player is hit by another player's hitbox.
    void OnTriggerEnter(Collider collider) {
        GameObject other = collider.gameObject;
        if (other.tag == "Wall") {
            touchingWall = true;
        }
        if (other.tag == "hitbox" && !invincible) {
            //For objects that aren't players that get hurt
            if (animator == null) {
                HitBox hitbox = other.GetComponent<HitBox>();
                if (hitbox.player != playerID) {
                    float dirConstant = Mathf.Sign(transform.position.x - hitbox.gameObject.transform.root.position.x);
                    rig.velocity = new Vector3(hitbox.getLaunch().x * dirConstant, hitbox.getLaunch().y, rig.velocity.z);
                    Instantiate(hitfx, collider.ClosestPointOnBounds(transform.position), Quaternion.identity);
                    camFuncs.shake(0.1f, 0.08f);
                }
            }

            //For other player-bot-things that get hurt
            if (animator != null) {
                HitBox hitbox = other.GetComponent<HitBox>();
                //print("Collider Player ID: " + hitbox.player + ", Current Player: " + playerID);
                if (hitbox.player != playerID) {
                    if (animator.GetBool("Guard")) {
                        animator.SetBool("GuardHurt", true);
                    } else {
                        if (hitbox.getLaunch().y < 25) {
                            animator.SetBool("Damage", true);
                        } else {
                            if (hitbox.getLaunch().y > 30 && Random.Range(0f, 1f) < 0.75f) {
                                animator.SetBool("Spin_Damage", true);
                            } else if (hitbox.getLaunch().y > 12 && hitbox.getLaunch().x > 15 && Random.Range(0f, 1f) < 0.5f) {
                                animator.SetBool("Spin_Damage", true);
                            }
                            animator.SetBool("AirDamage", true);
                        }
                    }
                    float dirConstant = Mathf.Sign(transform.position.x - hitbox.gameObject.transform.root.position.x);
                    if (touchingWall) dirConstant = -dirConstant * 1.1f;

                    //Knockback
                    if (!animator.GetBool("Guard")) rig.velocity = new Vector3(hitbox.getLaunch().x * dirConstant, hitbox.getLaunch().y, rig.velocity.z);
                    else rig.velocity = new Vector3(Mathf.Clamp((hitbox.getLaunch().x / 2), 10, 40) * dirConstant, hitbox.getLaunch().y / 2, rig.velocity.z);

                    if (debugLaunch) {
                        print("Launch:" + hitbox.getLaunch());
                    }
                    //Hit Effects
                    Instantiate(hitfx, collider.ClosestPointOnBounds(transform.position), Quaternion.identity);
                    camFuncs.shake(0.1f, 0.08f + (Mathf.Abs(Vector3.Magnitude(hitbox.getLaunch())) * 0.0025f ) );
                }
                if (manager && manager.hitFreeze) {
                    hitFreeze();
                }

                if (!animator.GetBool("Guard")) {
                    if (!training) {
                        controller.health = Mathf.Clamp(controller.health - (Mathf.RoundToInt(hitbox.getLaunch().magnitude) / 5), 0, 100);
                    } else {
                        controller.health = Mathf.Clamp(controller.health - (Mathf.RoundToInt(hitbox.getLaunch().magnitude) / 5), 1, 100);
                    }
                    incComboCounter();
                } else {
                    if (!training) {
                        controller.health = Mathf.Clamp(controller.health - (Mathf.RoundToInt(hitbox.getLaunch().magnitude) / 25), 0, 100);
                    } else {
                        controller.health = Mathf.Clamp(controller.health - (Mathf.RoundToInt(hitbox.getLaunch().magnitude) / 25), 0, 100);
                    }
                    controller.guardMeter = Mathf.Clamp(controller.guardMeter - (Mathf.RoundToInt(hitbox.getLaunch().magnitude) / 2), 0, 100);
                }

                if (playerID == 1) {
                    RoundManager.hits_taken_p1 += 1;
                }
                if (playerID == 2) {
                    RoundManager.hits_taken_p2 += 1;
                }
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Wall") {
            touchingWall = false;
        }
    }

    //Just the camera shake when the player dies
    public void deathCameraShake() {
        camFuncs.shake(0.2f, 0.12f);
    }


    //Combo Counting Functions and stuff
    public int comboCounter = 0;
    float comboTimer = 0;
    float timeout = 1f;
    GameManager manager;

    void comboTime() {
        if (comboCounter > 0) {
            comboTimer += Time.deltaTime;
            comboTimeout();
        }
    }
    public void incComboCounter() {
        if (comboCounter <= 0) {
            comboCounter += 1;
            comboTimer = 0;
            //print("STARTED COMBO");
        } else if (comboCounter >= 1 && comboTimer < timeout) {
            comboCounter += 1;
            comboTimer = 0;
        }

        if (manager) {
            manager.comboInc(playerID, comboCounter);
        }
    }

    public void comboTimeout() {
        if (!animator.GetComponent("Air") && comboTimer > timeout) {
            if (playerID == 1) {
                RoundManager.max_p2_combo = Mathf.Max(comboCounter, RoundManager.max_p2_combo);
                RoundManager.total_p2_combos += comboCounter;
            }

            if (playerID == 2) {
                RoundManager.max_p1_combo = Mathf.Max(comboCounter, RoundManager.max_p1_combo);
                RoundManager.total_p1_combos += comboCounter;
            }

            comboCounter = 0;
            comboTimer = 0;

            if (manager) {
                manager.endCombo(playerID);
            }

            if (training) {
                controller.health = 100;
            }
            //print("COMBO TIMED OUT");
        }
    }

    public void hitFreeze() {
        StartCoroutine(wait());
    }

    IEnumerator wait() {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.006f);
        Time.timeScale = 1;
    }

    HitBox currentBox;
    public void setHitbox(HitBox box) {
        currentBox = box;
    }



}
