using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Level Up Player capsule eaten by player");
        gameObject.SetActive(false);
    }
}
