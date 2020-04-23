using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDefault : MonoBehaviour
{
    private string name = "";
    private string description = "";
    private EnumAbilityType abilityType = EnumAbilityType.NONE;

    public virtual string Name { get => name; }
    public virtual string Description { get => description; }
    public virtual EnumAbilityType AbilityType  {get => abilityType; }

    public virtual void HandleFight(SmartTile smarttile, EnumTeams teamToMove, Character enemy)
    {
        FightHandler.FightWithClosestEnemy(smarttile, teamToMove, enemy);
    }

    public virtual Character IsTargetInRange(SmartTile smartTile, EnumTeams teamToMove)
    {
        Character target = FightHandler.IsFight(smartTile, teamToMove);

        return target;
    }

    public virtual void MoveCharacterOnTile(SmartTile smartTile, EnumDirection directionToMove, EnumTeams teamToMove)
    {
        GameObject tileToMoveTo = smartTile.GetTileFromDirection(directionToMove, teamToMove);
        SmartTile smartTileToMoveTo = smartTile.GetSmartTileFromDirection(directionToMove, teamToMove);

        GameObject characterObjectToMove = smartTile.GetCharacterObject(teamToMove);
        Character characterData = smartTile.GetCharacter(teamToMove);

        //only move if the next tile excists
        if (tileToMoveTo != null && smartTileToMoveTo != null)
        {
            if (smartTileToMoveTo.IsEmpty(teamToMove))
            {
                Debug.Log("Just moved " + characterData.TeamColor + characterData.CharacterName + " to space X-" + smartTileToMoveTo.PositionNumberX + "_Y-" + smartTileToMoveTo.PositionNumberY);
                smartTileToMoveTo.AddCharacterToSpace(characterData, characterObjectToMove);
                smartTile.RemoveCharacterFromSpace(characterData);
            }
        }
        else
        {
            //if no tile excists we are at the end and this team has won
            TurnHandler.SetVictoryState();
        }
    }
}
