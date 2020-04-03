using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTemplate : MonoBehaviour
{
    float health;
    float manaCost;
    float damage;
    
    string specialAbilityText = "";

    private GameObject character;

    #region 

    public CharacterTemplate(float health, float manaCost, float damage, string specialAbilityText)
    {
        this.health = health;
        this.manaCost = manaCost;
        this.damage = damage;
        this.specialAbilityText = specialAbilityText;
    }

    public CharacterTemplate(float health, float manaCost, float damage)
    {
        this.health = health;
        this.manaCost = manaCost;
        this.damage = damage;
    }

    public CharacterTemplate()
    {
        health = 0;
        manaCost = 0;
        damage = 0;

        specialAbilityText = "";
    }

    #endregion

    #region getters setters

    public float Health { get => health; set => health = value; }
    public float ManaCost { get => manaCost; set => manaCost = value; }
    public float Damage { get => damage; set => damage = value; }
    public string SpecialAbilityText { get => specialAbilityText; set => specialAbilityText = value; }
    public GameObject Character { get => character; }

    #endregion

    public void DealDamageTo(CharacterTemplate temp)
    {
        //TODO play some ability animation

        temp.Health -= Damage;
        temp.checkDeath();
    }

    public void checkDeath()
    {
        if (health <= 0)
        {
            //TODO: play some death animation

            Destroy(Character);
        }
    }

    
}
