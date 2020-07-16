using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionCapsule : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Protection capsule eaten by player");
       gameObject.SetActive(false);
    }
}
