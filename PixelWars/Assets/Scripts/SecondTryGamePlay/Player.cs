using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player:MonoBehaviour
{
    private float maxMana = 8;
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private float startMana = 2;

    private float ManaRegen = 2;

    private float currentMana = 0;
    private float currentHealth = 1;

    public Player()
    {
        this.currentHealth = maxHealth;
        this.currentMana = startMana;
    }

    public void UpdateManaSprites(List<GameObject> fullCrystals, List<GameObject> emptyCrystals)
    {
        foreach (GameObject emptyCrystal in emptyCrystals)
        {
            emptyCrystal.SetActive(true);
        }
        foreach (GameObject fullCrystal in fullCrystals)
        {
            fullCrystal.SetActive(false);
        }

        for (int i = 0; i < currentMana; i++)
        {
            fullCrystals[i].SetActive(true);
            emptyCrystals[i].SetActive(false);
        }
    }

    public void GainMana()
    {
        if (currentMana <= maxMana-ManaRegen)
        {
            this.currentMana += ManaRegen;
        }
    }

    public void PayMana(float ManaCost)
    {
        currentMana -= ManaCost;
    }

    public bool CanPlay(int ManaCost)
    {
        if (currentMana >= ManaCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
    }

    public bool IsDead()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float CurrentMana { get => currentMana;}
    public float CurrentHealth { get => currentHealth;}
}
