using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int currentMana = 0;
    private int currentHealth = 1;
    private int maxMana = 0;
    private int maxHealth = 0;

    public Player(int maxMana, int maxHealth)
    {
        this.maxMana = maxMana;
        this.maxHealth = maxHealth;

        this.currentHealth = maxHealth;
        this.currentMana = maxMana;
    }

    public void PayMana(int ManaCost)
    {
        currentMana -= ManaCost;
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

    public int Mana { get => currentMana;}
    public int Health { get => currentHealth;}
}
