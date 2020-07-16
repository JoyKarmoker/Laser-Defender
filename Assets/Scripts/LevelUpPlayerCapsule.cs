using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPlayerCapsule : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player.HasNextSprite())
        {
            player.MoveToNextSprite();
        }
        gameObject.SetActive(false);
    }
}
