using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void SetMaxHealth(float MaxHealthValue)
    {
        healthSlider.maxValue = MaxHealthValue;
    }

    public void UpdateHealth(float currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}
