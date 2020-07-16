using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamCapsule : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Laser Beam capsule eaten by player");
        gameObject.SetActive(false);
    }
}
