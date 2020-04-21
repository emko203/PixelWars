using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public void SetMaxHealth(float MaxHealthValue)
    {
        healthSlider.maxValue = MaxHealthValue;
        healthSlider.value = MaxHealthValue;

        fill.color = gradient.Evaluate(1f);
    }

    public void UpdateHealth(float currentHealth)
    {
        healthSlider.value = currentHealth;

        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}
