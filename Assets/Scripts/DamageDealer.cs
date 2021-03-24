using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int GetDamage()
    {
        return damage;
    }

    public bool IsLaser()
    {
        return true;
    }
    public void Hit()
    {
        if(!(gameObject.tag=="LaserHead"))
        {
            gameObject.SetActive(false);
        }
        

    }
}
