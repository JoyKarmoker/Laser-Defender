using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [Tooltip("Average number of seconds between this capsule is seen")]
    public float seenEverySeconds = 20f;

    /* 
     This method is called by the player it it hits the 
     Laser Beam Capsule, Here a vfx is needed to show
     the laser is emiting from player and destroying
     the enemy ships
     
     */
    public void LaserBeamCapuleHitProcess( GameObject laserBeamCapsule)
    {
        //Debug.Log("Laser Beam capsule eaten by player");
        laserBeamCapsule.SetActive(false);
    }
}
