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
    float attackTime;
    float coolDown;
    float blockTime;
    int helper;
    bool canAttack;
    int dodgeRng;
	// Use this for initialization
	void Start () {
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
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(AI.position, AI.position + new Vector3(7, 0));
        Debug.DrawLine(AI.position, AI.position - new Vector3(7, 0));

        if (Math.Abs(AI.position.x - player.position.x) > 7.0f)
        {
            animator.SetBool("Guard", false);
            animator.SetBool("Duck", false);
            float boost = (!animator.GetBool("Back") && animator.GetBool("Sprint")) ? (-5 * transform.localScale.x + 15) : 0;
            //Debug.Log("The AI is too far from the player, move closer");
            if(AI.position.x < player.position.x && animator.GetBool("Moveable"))
            {
                animator.SetBool("Running", true);
                animator.SetBool("Back", control.getDirConstant() > 0);
                rig.velocity = new Vector3((26.37f * transform.localScale.x) + boost, rig.velocity.y, rig.velocity.z);
            }
            else if (AI.position.x > player.position.x && animator.GetBool("Moveable"))
            {
                animator.SetBool("Running", true);
                animator.SetBool("Back", control.getDirConstant() < 0);
                rig.velocity = new Vector3(-(26.37f * transform.localScale.x) - boost, rig.velocity.y, rig.velocity.z);
            }
        }
        else
        {
            animator.SetBool("Running", false);
            //int helper = UnityEngine.Random.Range(0, 10);
            float time = Time.time;
            //int dodgeRng;
            if (time >= coolDown)
            {
                helper = UnityEngine.Random.Range(0, difficulty);
                coolDown = time + attackTime;
                animator.SetBool("Guard", false);
                animator.SetBool("Attack", false);
                animator.SetBool("Duck", false);
                animator.SetBool("Dodge", false);
                dodgeRng = UnityEngine.Random.Range(0, 15);
                //Debug.Log(dodgeRng);
            }
            //Debug.Log(coolDown - time);
            if (helper < 2 && canAttack)
            {

                //float timer = Time.time + 2.0f;
                attack();
                //coolDown = time + attackTime;
            }
            else if (helper < 4)
            {
                block();
                //coolDown = time + blockTime;
            }
            else
            {
                animator.SetBool("Running", true);
                animator.SetBool("Running", false);
                //Debug.Log("Idle");
                canAttack = true;
            }
            if(Input.GetKey(KeyCode.X) && UnityEngine.Random.Range(0,10) <= 5) {
                animator.SetBool("Duck", true);
            }
            //int dodgeRng = UnityEngine.Random.Range(0, 15);
            if (Input.GetKey(KeyCode.Z) && dodgeRng <= 5) {
                animator.SetBool("Dodge", true);
                //Debug.Log(dodgeRng);
                dodgeRng = 6;

            }
        }
    }

    private void attack()
    {
        //Debug.Log("Attack");
        animator.SetBool("Guard", false);
        animator.SetBool("Attack", true);
        //canAttack = false;
    }

    private void block()
    {
        //Debug.Log("Block");
        animator.SetBool("Guard", true);
        canAttack = true;
    }
}
