/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    private int healthAmount;
    private int healthAmountMax;

    public HealthSystem(int healthAmount) {
        healthAmountMax = healthAmount;
        this.healthAmount = healthAmount; 
    }

    public void Damage(int amount) {
        healthAmount -= amount;
        if (healthAmount < 0) {
            healthAmount = 0;
        }
        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
    }

    public void Heal(int amount) {
        healthAmount += amount;
        if (healthAmount > healthAmountMax) {
            healthAmount = healthAmountMax;
        }
        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
    }

    public float GetHealthNormalized() {
        return (float)healthAmount / healthAmountMax;
    }

}
