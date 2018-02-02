using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents the HitBox of the collider
public class HitBox : MonoBehaviour {
    public Vector3 launch; //The direction that the player will launch when getting hit by the hitbox
    public int player = 0;
    FighterController fighter;

    //If True, use Unity Animation Interlopation for setting hitbox launch. This causes issues due to transitions.
    //If False, default to Fighter's hitbox. Allows for instant setting of hitbox launch. Downside is no sweet-spotting
    bool manualHitbox = true; 
    
    public void Start() {
        fighter = transform.root.gameObject.GetComponent<FighterController>();
        if (fighter != null) {
            player = fighter.playerID;
        }
    }

    public Vector3 getLaunch() {
        if (manualHitbox && fighter != null) {
            return fighter.getLaunch();
        } else {
            return launch;
        }
    }

}
