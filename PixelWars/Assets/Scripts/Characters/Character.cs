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
    private HealthBar bar;

    private float maxHealth;
    private float manaCost;
    private float damage;
    private float speed;
    private float range;
    private string characterName;
    private float currentHealth;

    private AbilityTemplate abilityTemplate = new NoAbility();
    
    public void InitCharacter()
    {
        this.maxHealth = data.MaxHealth;
        this.manaCost = data.ManaCost;
        this.damage = data.Damage;
        this.speed = data.Speed;
        this.range = data.Range;
        this.characterName = data.CharacterName;
        currentHealth = maxHealth;

        InitAbility();
    }

    //Set ability according to class
    private void InitAbility()
    {
        switch (unitType)
        {
            case EnumUnit.KNIGHT:
                abilityTemplate = new NoAbility();
                break;
            case EnumUnit.ARCHER:
                abilityTemplate = new NoAbility();
                break;
            case EnumUnit.MAGE:
                abilityTemplate = new NoAbility();
                break;
            case EnumUnit.ROGUE:
                abilityTemplate = new RogueAbility();
                break;
            case EnumUnit.NONE:
                abilityTemplate = new NoAbility();
                break;
            case EnumUnit.PRIEST:
                abilityTemplate = new PriestAbility();
                break;
            case EnumUnit.GHOST:
                abilityTemplate = new NoAbility();
                break;
            default:
                abilityTemplate = new NoAbility();
                break;
        }
    }

    private void Start()
    {
        InitCharacter();

        SetupHealthBar();
    }

    private void SetupHealthBar()
    {
        bar = this.GetComponentInChildren<HealthBar>();

        if (bar != null)
        {
            bar.SetMaxHealth(maxHealth);
            UpdateHealthbar.AddListener(() => bar.UpdateHealth(currentHealth));
        }
    }

    public void healCharacter(float amount)
    {
        if (currentHealth + amount <= maxHealth)
        {
            currentHealth += amount;
            UpdateHealthbar.Invoke();
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

        CheckDeath();
    }

    /// <summary>
    /// Deal damage to character and destroys it on death
    /// </summary>
    /// <param name="target">Character to deal damage to</param>
    public void DealDamageTo(Character target) 
    {
        FloatingTextController.CreateFloatingText(Damage.ToString(),target.transform);
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
            UpdateHealthbar.RemoveListener(() => bar.UpdateHealth(currentHealth));
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
    public AbilityTemplate AbilityTemplate { get => abilityTemplate; }
}
