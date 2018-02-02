using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bone : MonoBehaviour {
    //Root
    public GameObject root;

    //Body
    public GameObject spine;
    public GameObject chest;

    //LeftArm
    public GameObject Left_Shoulder;
    public GameObject Left_Elbow;
    public GameObject Left_Hand;

    //Right Arm
    public GameObject Right_Shoulder;
    public GameObject Right_Elbow;
    public GameObject RightHand;

    //Left Leg
    public GameObject Left_Hip;
    public GameObject Left_Knee;
    public GameObject Left_Foot;

    //Right Leg
    public GameObject Right_Hip;
    public GameObject Right_Knee;
    public GameObject Right_Foot;


    public void Start() {

    }

    public Quaternion rot = Quaternion.identity;
    public void Update() {
        spine.transform.rotation = rot;
    }
}