using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Sean McManus
public class EnemyAI : MonoBehaviour {

    public int difficulty = 6;

    Transform AI;
    public Transform player;
    FighterController control;
    Rigidbody rig;
    Animator animator;
    //floats
    float attackTime;
    float attackCD; //wait between attacks
    float coolDown;
    float blockTime;
    float dist;
    float JAWT; //Jump Attack Wait Time
    float AIYVal;
    float attackRNG;
    //ints
    int helper;
    int jumpAttackRng;
    int dodgeRng;
    //Bools
    bool canAttack;
    bool airAttack;
    bool stopThinking;
    bool jumped;

    // Use this for initialization
    void Start() {
        
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        AI = GetComponent<Transform>();
        control = GetComponent<FighterController>();
        attackTime = 1.2f;
        coolDown = 0.0f;
        blockTime = 2.0f;
        helper = UnityEngine.Random.Range(0, 10);
        canAttack = false;
        dodgeRng = 0;
        dist = 6.0f;
        JAWT = 0.12f;
        airAttack = true;
        jumpAttackRng = 0;
        AIYVal = 0f;
        stopThinking = false;
        attackCD = 0;
        attackRNG = 0;
        jumped = false;

        if (!RoundManager.player2AI) {
            control.controllable = true;
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update() {
        if (control.health <= 0) {
            this.enabled = false;
        }
        Debug.DrawLine(AI.position, AI.position + new Vector3(dist, 0));
        Debug.DrawLine(AI.position, AI.position - new Vector3(dist, 0));
        if (Math.Abs(AI.position.x - player.position.x) > dist) {
            animator.SetBool("Guard", false);
            animator.SetBool("Duck", false);
            float boost = (!animator.GetBool("Back") && animator.GetBool("Sprint")) ? (-5 * transform.localScale.x + 15) : 0;
            //Debug.Log("The AI is too far from the player, move closer");
            if (AI.position.x < player.position.x && animator.GetBool("Moveable")) {
                animator.SetBool("Running", true);
                animator.SetBool("Back", control.getDirConstant() > 0);
                rig.velocity = new Vector3((26.37f * transform.localScale.x) + boost, rig.velocity.y, rig.velocity.z);
            } else if (AI.position.x > player.position.x && animator.GetBool("Moveable")) {
                animator.SetBool("Running", true);
                animator.SetBool("Back", control.getDirConstant() < 0);
                rig.velocity = new Vector3(-(26.37f * transform.localScale.x) - boost, rig.velocity.y, rig.velocity.z);
            }
        } else {
            animator.SetBool("Running", false);
            //int helper = UnityEngine.Random.Range(0, 10);
            float time = Time.time;
            //int dodgeRng;
            if (time >= coolDown) {
                helper = UnityEngine.Random.Range(0, difficulty);
                coolDown = time + attackTime;
                resetAnims();
                //animator.SetBool("Down", false);
                dodgeRng = UnityEngine.Random.Range(0, 15);
                //Debug.Log(dodgeRng);
                attackRNG = UnityEngine.Random.Range(0, 4);
                JAWT = Time.time + 0.12f;
                jumpAttackRng = UnityEngine.Random.Range(0, 3);
                airAttack = true;
                stopThinking = false;
                attackCD = 0;
                if (rig.velocity.y == 0) {
                    jumped = false;
                }
            }
            //Debug.Log(coolDown - time);
            if (!stopThinking) {
                if (helper < 2 && canAttack) {

                    //float timer = Time.time + 2.0f;
                    if (attackRNG < 3) {
                        attack();
                    } else {
                        jumpAttack();
                    }
                    //coolDown = time + attackTime;
                } else if (helper < 4) {
                    block();
                    //coolDown = time + blockTime;
                } else {
                    resetAnims();
                    canAttack = true;
                }
            }
            if ((Input.GetButton("Guard_1P")) && UnityEngine.Random.Range(0, 10) <= 5) {
                animator.SetBool("Duck", true);
            }
            //int dodgeRng = UnityEngine.Random.Range(0, 15);
            if (Input.GetButton("Attack_1P") && dodgeRng <= 5) {
                animator.SetBool("Dodge", true);
                //Debug.Log(dodgeRng);
                dodgeRng = 6;
            }
        }
    }

    private void attack() {
        if (attackCD >= 30) {
            helper = 6;
        }
        animator.SetBool("Guard", false);
        if (attackRNG == 0) {
            animator.SetBool("Up", true);
            attackCD++;
        } else if (attackRNG == 1) {
            animator.SetBool("Duck", true);
            attackCD++;
            //helper = UnityEngine.Random.Range(4, 6);
        }
        animator.SetBool("Attack", true);

        //Debug.Log("Attack");
        /*if (attackCD < Time.time)
        {
            animator.SetBool("Guard", false);
            animator.SetBool("Attack", true);
            attackCD = Time.time + animator.runtimeAnimatorController.animationClips[0].length;
        }
        *///Wait(animator.runtimeAnimatorController.animationClips[0].length + .2f);
          //canAttack = false;

    }

    private void block() {
        //Debug.Log("Block");
        if (!control.getGuardBreak()) {
            animator.SetBool("Guard", true);
        }
        canAttack = true;
        airAttack = true;
    }

    private void jump() {
        if (animator.GetBool("Grounded")) {
            animator.SetBool("Jump", true);
            animator.SetBool("DamageCheck", false);
            rig.velocity = new Vector3(rig.velocity.x, control.jumpHeight, rig.velocity.z);
            if (rig.velocity.y < 0) {
                jumped = true;
            }
        }
    }

    private void jumpAttack() {
        jump();
        //canAttack = false;
        if (stopThinking) {
            helper = 6;
        }
        if (checkYVelocity() && airAttack) {
            if (jumpAttackRng == 0) {
                animator.SetBool("Up", true);
                animator.SetBool("Attack", true);
                //canAttack = false;
            } else if (jumpAttackRng == 2) {
                animator.SetBool("Duck", true);
                animator.SetBool("Attack", true);
            }
            //airAttack = false;
            //stopThinking = true;
            animator.SetBool("Attack", true);
            airAttack = false;
            //JAWT = Time.time + .20f;
        }
        if ((jumped || !airAttack) && rig.velocity.y >= 0) {
            Debug.Log("The air attack should be over");
            helper = 6;
        }
    }

    private bool checkYVelocity() {
        //Debug.Log(rig.velocity.y);
        return rig.velocity.y < 0f;
    }

    private IEnumerator Wait(float seconds) {
        yield return new WaitForSeconds(seconds);
    }


    private void resetAnims() {
        animator.SetBool("Guard", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Duck", false);
        animator.SetBool("Dodge", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Up", false);
    }
}
