using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Character_Template data;
    [SerializeField] private EnumTeams teamColor;

    private BattlePostion position;

    /// <summary>
    /// Returns true if enemy is in the given space
    /// </summary>
    /// <returns>True if there is an enemy</returns>
    public bool CheckForEnemyInFront()
    {
        return position.TileFront.IsOcupiedEnemy(teamColor);
    }

    /// <summary>
    /// Moves character an amount of spaces in an direction
    /// </summary>
    /// <param name="amountOfSpaces">Amount of spaces to move if 0 move default character speed</param>
    public void MoveCharacter(int amountOfSpaces = 0, EnumDirection direction = EnumDirection.UP)
    {
        position.RemoveCharacterFromSpace(this);

        //Move default character speed
        if (amountOfSpaces == 0)
        {
            for (int i = 0; i < data.Speed; i++)
            {
                position = GetPositionAccordingToDirection(direction);
            }
        }
        //Move given amount
        else
        {
            for (int i = 0; i < amountOfSpaces; i++)
            {
                position = GetPositionAccordingToDirection(direction);
            }
        }

        Position.AddCharacterToSpace(this);
    }

    private BattlePostion GetPositionAccordingToDirection(EnumDirection direction)
    {
        switch (direction)
        {
            case EnumDirection.UP:
                return position.TileFront;
            case EnumDirection.DOWN:
                return position.TileBehind;
            case EnumDirection.LEFT:
                return position.TileLeft;
            case EnumDirection.RIGHT:
                return position.TileRight;
            default:
                return position.TileFront;
        }
    }

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
            this.DestroyCharacter();
        }
    }

    public void DestroyCharacter()
    {
        GameObject.Destroy(this);
        position.RemoveCharacterFromSpace(this);
    }

    public EnumTeams TeamColor { get => teamColor; set => teamColor = value; }
    public Character_Template Data { get => data; set => data = value; }
    public BattlePostion Position { get => position; set => position = value; }
}
