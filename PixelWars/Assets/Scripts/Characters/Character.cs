using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Character_Template data;
    [SerializeField] private EnumTeams teamColor;
    [SerializeField] private EnumUnit unitType;
    [SerializeField] private HealthBar healthbar;

    private float maxHealth;
    private float manaCost;
    private float damage;
    private float speed;
    private float range;
    private string characterName;
    private float currentHealth;

    private AbilityTemplate abilityTemplate;
    
    public void InitCharacter()
    {
        this.maxHealth = data.MaxHealth;
        this.manaCost = data.ManaCost;
        this.damage = data.Damage;
        this.speed = data.Speed;
        this.range = data.Range;
        this.characterName = data.CharacterName;
        this.abilityTemplate = data.AbilityTemplate;
        currentHealth = maxHealth;

        if (healthbar != null)
        {
            healthbar.SetMaxHealth(maxHealth);
        }
    }

    private void Start()
    {
        InitCharacter();

        if (healthbar != null)
        {
            healthbar.SetMaxHealth(MaxHealth);
        }
    }

    /// <summary>
    /// Substract damage from this character and destoys it on death
    /// </summary>
    /// <param name="amount">amount of damage to substract</param>
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        healthbar.UpdateHealth(CurrentHealth);
        //healthbar.UpdateHealth(data.CurrentHealth);

        CheckDeath();
    }

    /// <summary>
    /// Deal damage to character and destroys it on death
    /// </summary>
    /// <param name="target">Character to deal damage to</param>
    public void DealDamageTo(Character target) 
    {
        target.TakeDamage(Damage);
        Debug.Log("Dealt " + Damage + " damage to " + name + " (HpLeft:" + CurrentHealth);
    }

    /// <summary>
    /// Checks if health is <= 0 if true destroys the character
    /// </summary>
    public void CheckDeath()
    {
        if (CurrentHealth <= 0)
        {
            //TODO: kill character
            GameObject.Destroy(gameObject);
        }
    }

    public EnumTeams TeamColor { get => teamColor; set => teamColor = value; }
    public EnumUnit UnitType { get => unitType; set => unitType = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float ManaCost { get => manaCost; set => manaCost = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Range { get => range; set => range = value; }
    public string CharacterName { get => characterName; set => characterName = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public AbilityTemplate AbilityTemplate { get => abilityTemplate; set => abilityTemplate = value; }
}
