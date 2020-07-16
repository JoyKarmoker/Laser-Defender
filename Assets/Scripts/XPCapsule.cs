using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPCapsule : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("XP capsule eaten by player");
        gameObject.SetActive(false);
    }
}
