using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePostion
{
    Vector3 blueTeamPosition;
    Vector3 redTeamPosition;

    int locationX;
    int locationY;

    CharacterTemplate blueCharacter;
    CharacterTemplate redCharacter;

    public BattlePostion(Vector3 blueTeamPosition, Vector3 redTeamPosition, int locationX, int locationY)
    {
        this.blueTeamPosition = blueTeamPosition;
        this.redTeamPosition = redTeamPosition;
        this.locationX = locationX;
        this.locationY = locationY;
    }

    /// <summary>
    /// Checks if blue character is in this space
    /// </summary>
    /// <returns>Bool that is true if there is a blue character in this space</returns>
    public bool IsOcupiedBlue()
    {
        if (blueCharacter == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Checks if red character is in this space
    /// </summary>
    /// <returns>Bool that is true if there is a red character in this space</returns>
    public bool IsOcupiedRed()
    {
        if (redCharacter == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Saves character into this space
    /// </summary>
    /// <param name="character">this character gets saved to this space</param>
    public void AddCharacterToSpace(CharacterTemplate character)
    {
        character.PlaceCharacter(locationX, locationY);

        switch (character.TeamColor)
        {
            case EnumTeams.RED:
                redCharacter = character;
                break;

            case EnumTeams.BLUE:
                blueCharacter = character;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Removes a character from this space
    /// </summary>
    /// <param name="character">This character gets removed from this space</param>
    public void RemoveCharacterFromSpace(CharacterTemplate character)
    {
        switch (character.TeamColor)
        {
            case EnumTeams.RED:
                redCharacter = null;
                break;

            case EnumTeams.BLUE:
                blueCharacter = null;
                break;

            default:
                break;
        }
    }

    public Vector3 BlueTeamPosition { get => blueTeamPosition;}
    public Vector3 RedTeamPosition { get => redTeamPosition;}
    public CharacterTemplate BlueCharacter { get => blueCharacter;}
    public CharacterTemplate RedCharacter { get => redCharacter;}
    public int LocationX { get => locationX;}
    public int LocationY { get => locationY;}
}
