using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(fileName ="Characters",menuName ="New Character")]
public class Character_Template : ScriptableObject
{
    [SerializeField] private float health;
    [SerializeField] private float manaCost;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private string characterName;

    [SerializeField] private AbilityTemplate abilityTemplates;

    
    public float Health { get => health; set => health = value; }
    public float ManaCost { get => manaCost; set => manaCost = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }  
    public float Range { get => range; set => range = value; }

    public AbilityTemplate AbilityTemplate { get => abilityTemplates; set => abilityTemplates = value; }
    public string CharacterName { get => characterName; set => characterName = value; }
}
