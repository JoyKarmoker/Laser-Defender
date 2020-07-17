using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPlayerCapsule : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>(); //Finds The Player script from the object
        if(player)
        {
            if(player.HasNextSprite()) //If there is a next level of player
            {
                player.MoveToNextSprite(); //Move to Next Sprite
            }
        }

        gameObject.SetActive(false);
    }
}
