using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPCapsule : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("XP capsule eaten by player");
        Player player = other.gameObject.GetComponent<Player>(); //Finds The Player script from the object
        if(player)
        {
            player.XpCapsuleEaten(); // Calls the XpCapsuleEaten Method from player

        }
        gameObject.SetActive(false);
    }
}
