using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : CharacterTemplate
{
    [SerializeField] private GameObject characterGraphics;
    [SerializeField] private List<Ability> abilities;

    [SerializeField] private float health;
    [SerializeField] private float manaCost;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private int xposition;
    private int yposition;
    private EnumTeams teamColor;
    private string specialAbilityText;
    private BattlePostion position;

    public Knight()
    {

    }

    /// <summary>
    /// Create a custom knight
    /// </summary>
    public Knight(float health, float manaCost, float damage, float speed, int xposition, int yposition, EnumTeams teamColor, string specialAbilityText, BattlePostion position)
    {
        this.health = health;
        this.manaCost = manaCost;
        this.damage = damage;
        this.speed = speed;
        this.xposition = xposition;
        this.yposition = yposition;
        this.teamColor = teamColor;
        this.specialAbilityText = specialAbilityText;
        this.position = position;
    }

    public void checkDeath()
    {
        throw new System.NotImplementedException();
    }

    public void DealDamageTo(CharacterTemplate target)
    {
        throw new System.NotImplementedException();
    }

    public void MoveCharacterX(int speed)
    {
        throw new System.NotImplementedException();
    }

    public void MoveCharacterY(int speed)
    {
        throw new System.NotImplementedException();
    }

    public void PlaceCharacter(int xpos, int ypos)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }

    public GameObject CharacterGraphics { get => characterGraphics; set => characterGraphics = value; }
    public float Health { get => health; set => health = value; }
    public float ManaCost { get => manaCost; set => manaCost = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public int xPosition { get => xposition; set => xposition = value; }
    public int yPosition { get => yposition; set => yposition = value; }
    public EnumTeams TeamColor { get => teamColor; set => teamColor = value; }
    public string SpecialAbilityText { get => specialAbilityText; set => specialAbilityText = value; }
    public BattlePostion Position { get => position; set => position = value; }
    public List<Ability> Abilities { get => abilities; set => abilities = value; }
}
