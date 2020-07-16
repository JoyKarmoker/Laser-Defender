using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDecreasingCapsule : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Health Decreasing capsule eaten by player");
        gameObject.SetActive(false);
    }
}
