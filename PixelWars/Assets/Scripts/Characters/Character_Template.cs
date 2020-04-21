using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(fileName ="Characters",menuName ="New Character")]
public class Character_Template : ScriptableObject
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float manaCost;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private string characterName;

    [SerializeField] private AbilityTemplate abilityTemplates;

    private float currentHealth;

    public void SetNewCharacter()
    {
        currentHealth = maxHealth;
    }


    public float CurrentHealth { get => currentHealth; }
    public float ManaCost { get => manaCost; }
    public float Damage { get => damage;}
    public float Speed { get => speed;}  
    public float Range { get => range;}

    public AbilityTemplate AbilityTemplate { get => abilityTemplates;}
    public string CharacterName { get => characterName;}
    public float MaxHealth { get => maxHealth;}
}
