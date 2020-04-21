using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private Character_Template data;
    [SerializeField] private EnumTeams teamColor;
    [SerializeField] private EnumUnit unitType;

    private UnityEvent UpdateHealthbar = new UnityEvent();

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
    }

    private void Start()
    {
        InitCharacter();

        HealthBar bar = this.GetComponentInChildren<HealthBar>();

        if (bar != null)
        {
            bar.SetMaxHealth(maxHealth);
            UpdateHealthbar.AddListener(() => bar.UpdateHealth(currentHealth));
        }
    }

    /// <summary>
    /// Substract damage from this character and destoys it on death
    /// </summary>
    /// <param name="amount">amount of damage to substract</param>
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;

        UpdateHealthbar.Invoke();
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
        Debug.Log("Dealt " + Damage + " damage to " + name + " (HpLeft:" + CurrentHealth,this);
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
