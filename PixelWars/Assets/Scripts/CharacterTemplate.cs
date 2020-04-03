using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTemplate : MonoBehaviour
{
    float health;
    float manaCost;
    float damage;

    int xPosition;
    int yPosition;

    EnumTeams teamColor;

    string specialAbilityText = "";

    private GameObject character;

    #region constructors

    public CharacterTemplate(float health, float manaCost, float damage, string specialAbilityText, EnumTeams teamColor)
    {
        this.health = health;
        this.manaCost = manaCost;
        this.damage = damage;
        this.specialAbilityText = specialAbilityText;
        this.TeamColor = teamColor;
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
    public EnumTeams TeamColor { get => teamColor; set => teamColor = value; }

    #endregion

    /// <summary>
    /// Changes character position in the current map
    /// </summary>
    /// <param name="xpos">x</param>
    /// <param name="ypos">y</param>
    public void PlaceCharacter(int xpos, int ypos)
    {
        xPosition = xpos;
        yPosition = ypos;
    }

    /// <summary>
    /// Moves character forward on x axis
    /// </summary>
    /// <param name="speed">Amount of spaces to move</param>
    public void MoveCharacterX(int speed)
    {
        xPosition += speed;
    }

    /// <summary>
    /// Moves character forward on y axis
    /// </summary>
    /// <param name="speed">Amount of spaces to move</param>
    public void MoveCharacterY(int speed)
    {
        xPosition += speed;
    }

    /// <summary>
    /// Substract damage from this character and destoys it on death
    /// </summary>
    /// <param name="amount">amount of damage to substract</param>
    public void TakeDamage(int amount)
    {
        health -= amount;
        checkDeath();
    }

    /// <summary>
    /// Deal damage to character and destroys it on death
    /// </summary>
    /// <param name="target">Character to deal damage to</param>
    public void DealDamageTo(CharacterTemplate target)
    {
        //TODO play some ability animation

        target.Health -= Damage;
        target.checkDeath();
    }

    /// <summary>
    /// Checks if health is <= 0 if true destroys the character
    /// </summary>
    public void checkDeath()
    {
        if (health <= 0)
        {
            //TODO: play some death animation

            Destroy(Character);
        }
    }

    
}
