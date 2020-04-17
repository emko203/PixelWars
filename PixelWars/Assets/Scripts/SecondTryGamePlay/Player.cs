using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int maxMana = 10;
    private int maxHealth = 1;

    private int ManaRegen = 2;

    private int currentMana = 0;
    private int currentHealth = 1;

    public Player()
    {
        this.currentHealth = maxHealth;
        this.currentMana = maxMana;
    }

    public void GainMana()
    {
        if (currentMana <= maxMana-ManaRegen)
        {
            this.currentMana += ManaRegen;
        }
    }

    public void PayMana(int ManaCost)
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

    public int CurrentMana { get => currentMana;}
    public int CurrentHealth { get => currentHealth;}
}
