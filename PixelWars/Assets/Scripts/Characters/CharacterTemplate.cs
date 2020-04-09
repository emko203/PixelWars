using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTemplate : MonoBehaviour
{
    GameObject CharacterGraphics { get; set; }

    float Health { get; set; }
    float ManaCost { get; set; }
    float Damage { get; set; }
    float Speed { get; set; }

    int xPosition { get; set; }
    int yPosition { get; set; }

    EnumTeams TeamColor { get; set; }
    
    string SpecialAbilityText { get; set; }

    BattlePostion Position { get; set; }

    List<Ability> Abilities { get; set; }

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

    
}
