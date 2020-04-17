using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Character_Template data;
    [SerializeField] private EnumTeams teamColor;
    [SerializeField] private EnumUnit unitType;

    /// <summary>
    /// Substract damage from this character and destoys it on death
    /// </summary>
    /// <param name="amount">amount of damage to substract</param>
    public void TakeDamage(float amount)
    {
        data.Health -= amount;

        CheckDeath();
    }

    /// <summary>
    /// Deal damage to character and destroys it on death
    /// </summary>
    /// <param name="target">Character to deal damage to</param>
    public void DealDamageTo(Character target) { target.TakeDamage(data.Damage); }

    /// <summary>
    /// Checks if health is <= 0 if true destroys the character
    /// </summary>
    public void CheckDeath()
    {
        if (data.Health <= 0)
        {
            //TODO: kill character
        }
    }

    public EnumTeams TeamColor { get => teamColor; set => teamColor = value; }
    public Character_Template Data { get => data; set => data = value; }
    public EnumUnit UnitType { get => unitType; set => unitType = value; }
}
