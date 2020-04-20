using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Character_Template data;
    [SerializeField] private EnumTeams teamColor;
    [SerializeField] private EnumUnit unitType;
    [SerializeField] private HealthBar healthbar;


    /// <summary>
    /// Substract damage from this character and destoys it on death
    /// </summary>
    /// <param name="amount">amount of damage to substract</param>
    public void TakeDamage(float amount)
    {
        data.CurrentHealth -= amount;
        //healthbar.UpdateHealth(data.CurrentHealth);

        CheckDeath();
    }

    /// <summary>
    /// Deal damage to character and destroys it on death
    /// </summary>
    /// <param name="target">Character to deal damage to</param>
    public void DealDamageTo(Character target) 
    {
        target.TakeDamage(data.Damage);
        Debug.Log("Dealt " + data.Damage + " damage to " + target.Data.Name + " (HpLeft:" + target.data.CurrentHealth);
    }

    /// <summary>
    /// Checks if health is <= 0 if true destroys the character
    /// </summary>
    public void CheckDeath()
    {
        if (data.CurrentHealth <= 0)
        {
            //TODO: kill character
            GameObject.Destroy(gameObject);
        }
    }

    public EnumTeams TeamColor { get => teamColor; set => teamColor = value; }
    public Character_Template Data { get => data; set => data = value; }
    public EnumUnit UnitType { get => unitType; set => unitType = value; }
}
