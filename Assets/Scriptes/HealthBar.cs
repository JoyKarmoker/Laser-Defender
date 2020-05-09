using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Gradient gradient;
    public Image fill;
    public void setMaxHealth(int value)
    {
        healthBar.maxValue = value;
        healthBar.value = value;
        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }
    public void setHealth(int value)
    {
        healthBar.value = value;
        Debug.Log(healthBar.normalizedValue);
        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }
}
