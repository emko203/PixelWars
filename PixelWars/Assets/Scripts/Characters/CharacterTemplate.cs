using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTemplate : MonoBehaviour
{
    [SerializeField] private GameObject characterGraphics;
    [SerializeField] private List<IAbility_Template> abilities;

    [SerializeField] private float health;
    [SerializeField] private float manaCost;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private int xposition;
    private int yposition;
    private EnumTeams teamColor;
    private string specialAbilityText;
    private BattlePostion position;

    public CharacterTemplate(GameObject characterGraphics, List<IAbility_Template> abilities, float health, float manaCost, float damage, float speed, int xposition, int yposition, EnumTeams teamColor, string specialAbilityText, BattlePostion position)
    {
        this.characterGraphics = characterGraphics;
        this.abilities = abilities;
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

    public CharacterTemplate()
    {

    }

    /// <summary>
    /// Changes character position in the current map
    /// </summary>
    /// <param name="xpos">x</param>
    /// <param name="ypos">y</param>
    public void PlaceCharacter(int xpos, int ypos) { }

    /// <summary>
    /// Moves character an amount of spaces in an direction
    /// </summary>
    /// <param name="amountOfSpaces">Amount of spaces to move if 0 move default character speed</param>
    public void MoveCharacter(int amountOfSpaces = 0, EnumDirection direction = EnumDirection.UP)
    {
        Position.RemoveCharacterFromSpace(this);

        //Move default character speed
        if (amountOfSpaces == 0)
        {
            for (int i = 0; i < Speed; i++)
            {
                Position = GetPositionAccordingToDirection(direction);
            }
        }
        //Move given amount
        else
        {
            for (int i = 0; i < amountOfSpaces; i++)
            {
                Position = GetPositionAccordingToDirection(direction);
            }
        }

        Position.AddCharacterToSpace(this);
    }

    private BattlePostion GetPositionAccordingToDirection(EnumDirection direction)
    {
        switch (direction)
        {
            case EnumDirection.UP:
               return Position.TileFront;
            case EnumDirection.DOWN:
                return Position.TileBehind;
            case EnumDirection.LEFT:
                return Position.TileLeft;
            case EnumDirection.RIGHT:
                return Position.TileRight;
            default:
                return Position.TileFront;
        }
    }

    /// <summary>
    /// Substract damage from this character and destoys it on death
    /// </summary>
    /// <param name="amount">amount of damage to substract</param>
    public void TakeDamage(float amount)
    {
        Health -= amount;

        checkDeath();
    }

    /// <summary>
    /// Deal damage to character and destroys it on death
    /// </summary>
    /// <param name="target">Character to deal damage to</param>
    public void DealDamageTo(CharacterTemplate target) { target.TakeDamage(Damage); }

    /// <summary>
    /// Checks if health is <= 0 if true destroys the character
    /// </summary>
    public void checkDeath()
    {
        if (Health <= 0)
        {
            this.DestroyCharacter();
        } 
    }

    public void DestroyCharacter()
    {
        GameObject.Destroy(this.CharacterGraphics);
        Position.RemoveCharacterFromSpace(this);
    }

    public GameObject CharacterGraphics { get => characterGraphics; set => characterGraphics = value; }
    public List<IAbility_Template> Abilities { get => abilities; set => abilities = value; }
    public float Health { get => health; set => health = value; }
    public float ManaCost { get => manaCost; set => manaCost = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Xposition { get => xposition; set => xposition = value; }
    public int Yposition { get => yposition; set => yposition = value; }
    public EnumTeams TeamColor { get => teamColor; set => teamColor = value; }
    public string SpecialAbilityText { get => specialAbilityText; set => specialAbilityText = value; }
    public BattlePostion Position { get => position; set => position = value; }
}
