﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class FighterController : MonoBehaviour {

    public int health = 100;
    public float guardMeter = 100;
    public GameObject deathRagdoll;

    Animator animator;
    Rigidbody rig;
    public GameObject raycastPoint;
    public int playerID = 0;       //Determines if Player 1 or Player 2

    public GameObject target;       //The object that this fighter is fighting
    float dirConstant;

    EnemyDamage playerHitbox;       //The hitbox controller, manages what happens if an attack hits a player

    public int jumpHeight = 40;

    //Fighter Num Variables. These are used to swap out moves
    public int jab_1_num = 0; //The first jab in the three hit combo
    public int jab_2_num = 0; //The second jab in the three hit combo
    public int jab_3_num = 0; //The third jab in the three hit combo
    public int up_tilt_num = 0; //The Uppercut (Up-Arrow + Attack)
    public int up_air_num = 0; 
    public int down_air_num = 0; 

    public bool controllable = true;      //If deactivated, then fighting controls are disabled for the player

    bool guardBreak;

    PostProcessingBehaviour postProcessing;
    public PostProcessingProfile burstProfile;
    PostProcessingProfile prior;
    

    // Use this for initialization
    GameManager manager;
    void Start() {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        playerHitbox = GetComponent<EnemyDamage>();
        manager = FindObjectOfType<GameManager>();

        postProcessing = FindObjectOfType<PostProcessingBehaviour>();
        if (postProcessing) {
            prior = postProcessing.profile;
        }

        bool initialized = false;
        FighterSetup[] setups = FindObjectsOfType<FighterSetup>();
        if (setups.Length > 0)
        {
            foreach (FighterSetup setup in setups)
            {
                if ((playerID == 1 && setup.name == "Player1") || (playerID == 2 && setup.name == "Player2"))
                {
                    InitializeFromSetup(setup);
                    initialized = true;
                }
            }
        }

        if (!initialized)
        {
            Debug.Log("No setup for Player " + playerID + ". Using public variables.");
            animator.SetInteger("Jab_1_Num", jab_1_num);
            animator.SetInteger("Jab_2_Num", jab_2_num);
            animator.SetInteger("Jab_3_Num", jab_3_num);
            animator.SetInteger("Up_Air_Num", up_air_num);
            animator.SetInteger("Down_Air_Num", down_air_num);
            animator.SetInteger("Up_Tilt_Num", up_tilt_num);
        }

    }

    private void InitializeFromSetup(FighterSetup setup)
    {
        Debug.Log("Loaded setup for Player " + playerID);
        foreach (string key in setup.setup.Keys)
        {
            animator.SetInteger(key, setup.setup[key]);
        }
    }

    public Animator getAnimator() {
        return animator;
    }

    bool burst = false;
    bool canBurst = true;
    public bool getBurst() {
        return burst;
    }

    Vector3 tempPos;
    bool tempMoveable = false;

    public void reorient() {
        tempPos = target.transform.position;
    }

    // Update is called once per frame
    void Update() {
        //DEBUG CONTROLS - REMOVE FROM FULL VERSION
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F4)) {
            if (playerID == 2) {
                health = 0;
            }
        }
        #endif
        if (Input.GetKeyDown(KeyCode.Escape)) {
            RoundManager.exitGame();
        }
        dirConstant = Mathf.Sign(transform.position.x - target.transform.position.x);

        if (animator.GetBool("Moveable") != tempMoveable) {
            tempPos = target.transform.position;
            tempMoveable = animator.GetBool("Moveable");
        }

        //Rotates Self based on position to target.
        //Greater than
        if (animator.GetBool("Moveable")) {
            Vector3 eulers = transform.rotation.eulerAngles;
            float angle1 = Vector3.Angle(new Vector3(0, eulers.y, 0), Vector3.zero);
            float angle2 = Vector3.Angle(new Vector3(0, eulers.y, 0), new Vector3(0, 180, 0));

            if (transform.position.x - target.transform.position.x > 0) {      
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulers.x, 180, eulers.z), 0.1f);
            } else {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulers.x, 0, eulers.z), 0.1f);
            }
        } else {
            Vector3 eulers = transform.rotation.eulerAngles;
            float angle1 = Mathf.Abs(Vector3.Angle(new Vector3(0, Mathf.Abs(eulers.y), 0), Vector3.zero));
            float angle2 = Mathf.Abs(Vector3.Angle(new Vector3(0, Mathf.Abs(eulers.y), 0), new Vector3(0, 180, 0)));

            if (transform.position.x - tempPos.x > 0) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulers.x, 180, eulers.z), 0.2f);
            } else {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulers.x, 0, eulers.z), 0.2f);
            }
        }

        //Check for Touching Ground
        RaycastHit hit;
        bool under = Physics.Raycast(raycastPoint.transform.position, Vector3.down, out hit, 0.8f, 1 << 8);
        Debug.DrawLine(raycastPoint.transform.position, raycastPoint.transform.position + (Vector3.down * 1.5f));
        animator.SetBool("Grounded", under && hit.collider.gameObject.tag == "Ground" && rig.velocity.y < 10);
        if (animator.GetBool("Air") && animator.GetBool("Grounded") && Mathf.Abs(rig.velocity.y) > 30) {
            //animator.SetBool("Grounded", false);
            //print("BOUNCE");
            //rig.velocity = new Vector3(rig.velocity.x, Mathf.Clamp(Mathf.Abs(rig.velocity.y) / 2, 20, 40), rig.velocity.z);
        }
        if (animator.GetBool("Grounded")) {
            animator.SetBool("Jump", false);
        }

        //Checks if gravitation needed;
        gravitate();

        //Checks for Input Commands (Right now, through the keyboard)
        if (controllable) {
            keyCheck();
        }

        if (!guardBreak && !animator.GetBool("Guard")) guardMeter += 0.1f * Time.timeScale;
        if (!guardBreak && guardMeter <= 0) {
            guardBreak = true;
        }

        if (guardBreak) {
            animator.SetBool("Guard", false);
            if (burst) {
                guardMeter += 0.45f * Time.timeScale;
            } else {
                guardMeter += 0.15f * Time.timeScale;
            }
        }
        if (guardBreak && guardMeter >= 100) {
            guardMeter = 100;
            guardBreak = false;
            animator.speed = 1;
            if (burst) {
                CameraFunctions camfuncs = FindObjectOfType<CameraFunctions>();
                if (camfuncs) {
                    camfuncs.shake(0.2f, 0.3f);
                }
            }
            burst = false;
            if (manager) {
                manager.hitFreeze = false;
            }
            if (postProcessing) {
                postProcessing.profile = prior;
            }
        }
        if (guardMeter > 100) {
            guardMeter = 100;
        }
        if (guardMeter < 0) {
            guardMeter = 0;
        }


        if (animator.GetBool("Moveable") || animator.GetBool("DamageCheck")) gravitation = false;
        if (manager.trainingMode) canBurst = true;
    }

    public bool getGuardBreak() {
        return guardBreak;
    }

    void keyCheck() {
        bool p1 = (playerID == 1);
        bool p2 = (playerID == 2);
        if ((p1 && Input.GetButtonDown("Attack_1P")) || (p2 && Input.GetButtonDown(PlayerInput.attack_2p))) {
            animator.SetBool("Attack", true);
        }

        if (!guardBreak || burst) {
            if ((p1 && Input.GetButtonDown(PlayerInput.dodge_right_1p)) || (p2 && Input.GetButtonDown(PlayerInput.dodge_right_2p))) {
                animator.SetBool("Dodge", true);
                dodgeConstant = 1;
                if (dirConstant == 1) {
                    animator.SetBool("Back", true);
                } else {
                    animator.SetBool("Back", false);
                }
            }

            if ((p1 && Input.GetButtonDown(PlayerInput.dodge_left_1p)) || (p2 && Input.GetButtonDown(PlayerInput.dodge_left_2p))) {
                animator.SetBool("Dodge", true);
                dodgeConstant = -1;

                if (dirConstant == -1) {
                    animator.SetBool("Back", true);
                } else {
                    animator.SetBool("Back", false);
                }
            }
        }

        //For Up Attacks
        animator.SetBool("Up",  (p1 && Input.GetAxis("Vertical_1P") < -0.4f) || (p2 && Input.GetAxis("Vertical_2P") < -0.4f));

        //For Guarding
        animator.SetBool("Guard", (p1 && Input.GetButton(PlayerInput.guard_1p)) || (p2 && Input.GetButton(PlayerInput.guard_2p)));

        //For Sprinting
        animator.SetBool("Sprint", (p1 && Input.GetKey(KeyCode.LeftShift)) || (p1 && Mathf.Abs(Input.GetAxis("Run_1P")) > 0.3f) || (p2 && Mathf.Abs(Input.GetAxis("Run_2P")) > 0.3f));

        float boost = (!animator.GetBool("Back") && animator.GetBool("Sprint")) ? (-5 * transform.localScale.x + 15) : 0;

        //For moving
        bool leftHit = Physics.Raycast(transform.position, Vector3.left, 3f, 1 << 8);
        bool rightHit = Physics.Raycast(transform.position, Vector3.right, 3f, 1 << 8);

        if (!rightHit && animator.GetBool("Moveable") && ((p1 && Input.GetKey(KeyCode.RightArrow)) 
            || (p1 && Input.GetAxis("Horizontal_1P") > 0.5f) 
            || (p2 && Input.GetAxis("Horizontal_2P") > 0.5f))) {
            animator.SetBool("Running", true);
            animator.SetBool("Back", dirConstant > 0);
            rig.velocity = new Vector3((26.37f * transform.localScale.x) + boost, rig.velocity.y, rig.velocity.z);
        } else if (!leftHit && animator.GetBool("Moveable") && ((p1 && Input.GetKey(KeyCode.LeftArrow)) 
            || (playerID == 1 && Input.GetAxis("Horizontal_1P") < -0.3f)
            || (playerID == 2 && Input.GetAxis("Horizontal_2P") < -0.3f))) {
            animator.SetBool("Running", true);
            animator.SetBool("Back", dirConstant < 0);
            rig.velocity = new Vector3(-(26.37f * transform.localScale.x) - boost, rig.velocity.y, rig.velocity.z);
        } else {
            animator.SetBool("Running", false);
        }

        animator.SetBool("Duck", (p1 && Input.GetKey(KeyCode.DownArrow)) || (p1 && Input.GetAxis("Vertical_1P") > 0.4f) || (p2 && Input.GetAxis("Vertical_2P") > 0.4f));

        if ((p1 && Input.GetButtonDown(PlayerInput.jump_1p) || (p2 && Input.GetButtonDown(PlayerInput.jump_2p))) && animator.GetBool("Moveable") && animator.GetBool("Grounded")) {
            animator.SetBool("Jump", true);
            animator.SetBool("DamageCheck", false);
            rig.velocity = new Vector3(rig.velocity.x, jumpHeight, rig.velocity.z);
        }

        //For BURST
        if ((p1 && Input.GetKeyDown(PlayerInput.burst_1p)) || (p1 && Input.GetKeyDown(KeyCode.A)) || (p2 && Input.GetKeyDown(PlayerInput.burst_2p))) {
            if (canBurst && !guardBreak) {
                burst = true;
                canBurst = false;
                animator.SetBool("Burst", true);
                guardMeter = 0;
                health -= (health / 2);
                animator.speed = 2f;
                guardBreak = true;
                CameraFunctions camfuncs = FindObjectOfType<CameraFunctions>();
                if (camfuncs) camfuncs.shake(0.2f, 0.3f);
                
                if (manager) manager.hitFreeze = true;
                
                if (postProcessing) postProcessing.profile = burstProfile;
            }

        }
    }

    

    //Plays an Audio Clip for an Animation
    public void PlayAudio(AudioClip clip) {
        GameObject a = new GameObject("clip");
        AudioSource source = a.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();

        TimedDestroy destroyer = a.AddComponent<TimedDestroy>();
        destroyer.startOnAwake = true;
        destroyer.time = clip.length;
    }

    //Manual Hitbox Configuring
    Vector3 launch = Vector3.zero;
    public Vector3 getLaunch() {
        return launch;
    }

    public void setLaunch(string newLaunch) {
        string[] pars = newLaunch.Split(',');
        float x = int.Parse(pars[0]);
        float y = int.Parse(pars[1]);
        float z = 0;
        if (pars.Length >= 3) z = int.Parse(pars[2]);
        launch = new Vector3(x, y, z);
    }

    //A sort of "Lock-On" For attacks. Brings user closer to 
    public bool gravitation = false;
    public void toggleGravitation(int toggle) {
        if (toggle == 0) {
            gravitation = false;
        } else {
            gravitation = true;
            gravPos = target.transform.position;
        }
    }

    Vector3 gravPos;
    void gravitate() {
        if (gravitation && Vector3.Distance(transform.position, gravPos) < ((-5 * transform.localScale.x) + 15)) {
            Vector3 targetPos = new Vector3(gravPos.x + (dirConstant * 3f), transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, (transform.localScale.x) + 0.3f);
        }
    }

    int dodgeConstant = 1;
    public void dodge() {

        rig.velocity = new Vector3(dodgeConstant * 50, rig.velocity.y, rig.velocity.z);
        if (!burst) {
            guardMeter -= 10;
        }
        if (burst) {
            guardMeter += 2;
        }
    }

    public void airDodge() {
        rig.velocity = new Vector3(dodgeConstant * 25, 15, rig.velocity.z);
        guardMeter -= 10;
    }

    public void reverseRotation() {
        float dist = Mathf.Abs(transform.position.x - target.transform.position.x);
        if (dist < 5) {
            if (transform.position.x - target.transform.position.x < 0) {
                Vector3 eulers = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(eulers.x, 180, eulers.z);
            } else {
                Vector3 eulers = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(eulers.x, 0, eulers.z);
            }
        }

    }
    public void stopDodge() {
        //rig.velocity = new Vector3(-dirConstant * 10, rig.velocity.y, rig.velocity.z);
    }

    /**
     * This section is dedicated to functions for attacks.
     * To add an action to a specific attack animation, go into the Animation tab,
     * choose the animation, slide to the frame that you want the function to happen in,
     * and click the add event button
     * 
     * The function must be public for this to work. Only strings, bools, 
     * and ints are valid parameters for these functions.
     */

    public void uppercut() {
        if (rig)
        rig.velocity = new Vector3(-dirConstant * 5, 25, rig.velocity.z);
    }

    public void slide() {
        if (rig)
        rig.velocity = new Vector3(-dirConstant * 40, rig.velocity.y, rig.velocity.z);
    }

    //I've found that sometimes hitboxes won't hit as hard as they should
    //because the 'launch' variable of the hitbox needs time to interpolate.
    //Events happen without interlopation, so thhis is my basic fix for now.
    public void instantSlideCalculation(HitBox hitbox) {
        hitbox.launch = new Vector3(2, 30, 0);
    }

    public void flipKick() {
        if (rig)
        rig.velocity = new Vector3(rig.velocity.x / 2, 20, rig.velocity.z);
    }


    public void downAerialStomp(int i) {
        if (rig) {
            if (i == 0) {
                rig.velocity = new Vector3(0, 4, 0);
            } else {
                rig.velocity = new Vector3(0, 20, 0);
            }
        }
    }

    //Kick Style Moves
    public void kicker_up_kick() {
        if (rig)
        rig.velocity = new Vector3(0, 16, 0);
    }
    
    public void kick_jab_2() {
        if (rig)
        rig.velocity = new Vector3(-dirConstant * 20, 5, 0);
    }

    float skidDir = 1;
    public void kick_down_aerial(int i) {
        skidDir = dirConstant;
        if (rig) {
            if (i == 0) {
                rig.velocity = new Vector3(dirConstant * 20, 20);
            } else {
                rig.velocity = new Vector3(-dirConstant * 60, -40);
            }
        }
    }

    public void kick_down_landing() {
        if (rig)
        rig.velocity = new Vector3(-skidDir * 50, 0);
    }

    public void kick_up_tilt(int i) {
        if (rig) {
            if (i == 0) {
                skidDir = dirConstant;
            } else if (i == 1 && (animator.GetBool("Jump") == false)) {
                rig.velocity = new Vector3(skidDir * 25, rig.velocity.y);
            } else if (i == 2) {
                animator.SetBool("Moveable", true);
                animator.SetBool("DamageCheck", true);
            } else if (i == 3) {
                animator.SetBool("Moveable", false);
                animator.SetBool("DamageCheck", false);
            } else if (animator.GetBool("Jump") == false) {
                rig.velocity = new Vector3(skidDir * 20, rig.velocity.y, 0);
            }
        }
    }

    public void berserker_up_tilt(int i) {
        if (rig) {
            if (i == 0) {
                rig.velocity = new Vector3(-dirConstant * 50, 90, 0);

            }
            if (i == 1) {
                rig.velocity = new Vector3(dirConstant * 5, 10, 0);
            }

            if (i == 2) {
                rig.velocity = new Vector3(-Mathf.Sign(rig.velocity.x) * 5, 0, 0);
            }
        }
    }

    public void berserker_up_aerial(int i) {
        if (rig) {
            if (i == 0) {
                rig.velocity = new Vector3(0, 10, 0);
            } else if (i == 1) {
                rig.velocity = new Vector3(0, 20, 0);
            }
        }
    }

    public void shoulderPush(int i) {
        if (rig) {
            if (i == 0) {
                rig.velocity = new Vector3(-dirConstant * 55, 0);
            } else {
                rig.velocity = new Vector3(0, 0);
            }
        }
    }

    public int getComboCounter() {
        return playerHitbox.comboCounter;
    }
    
    public float getDirConstant() {
        return dirConstant;
    }

    
}
