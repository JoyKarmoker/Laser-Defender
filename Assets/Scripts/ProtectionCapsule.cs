using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionCapsule : MonoBehaviour
{
    [SerializeField] float minSecCapsuleLasts = 1f; //Minimum time that the player will be protected after eating this Protection Capsule
    [SerializeField] float maxSecCapsuleLasts = 10f; //Maximum time that the player will be protected after eating this Protection Capsule
    float secCapsuleLasts;

    private void OnTriggerEnter2D(Collider2D other)
    {

        /*
        Turn the ProcessHit Method off of the player script for random time and 
        create a vfx to show that the the player is under some kind of protection so it will not take any a damage;
        */

        Player player = other.gameObject.GetComponent<Player>();
        if(player)
        {
            Debug.Log("Protection capsule eaten by player");
            secCapsuleLasts = Random.Range(minSecCapsuleLasts, maxSecCapsuleLasts); //Ranrom Time The Capsule Effect will last
            Debug.Log("Protected for " + secCapsuleLasts.ToString());
            player.SafeForSeconds(secCapsuleLasts);
        }
        
        
        gameObject.SetActive(false);
    }
}
