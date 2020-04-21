using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAbility : AbilityTemplate
{
    private EnumAbilityType abilityType = EnumAbilityType.MOVE;
    private string abilityName = "Vanish";
    private string abilityDescription = "Move through the first enemy unit you come across";

    private bool HasAlreadyUsed = false;

    private bool canHandle = true;

    private bool DoNormalMove = true;



    public override void HandleAbility()
    {
        
    }

    public override void HandleAbility(SmartTile smartTile, EnumDirection directionToMove, EnumTeams teamToMove)
    {
        if (HasAlreadyUsed)
        {
            //cant handle because we already used rogue ability
            canHandle = false;
            DoNormalMove = true;
        }
        else
        {
            Character characterData = smartTile.GetCharacter(teamToMove);

            characterData.Range = 1;

            Character enemy = FightHandler.IsFight(smartTile, teamToMove);

            if (enemy != null)
            {
                //enemy in front of us so we skip the unit
                SmartTile tileToMoveTo = smartTile.GetSmartTileFromDirection(directionToMove, teamToMove);
                GameObject actualTileToMoveTo = tileToMoveTo.GetTileFromDirection(directionToMove, teamToMove);

                SmartTile smartTileToMoveTo = smartTile.GetSmartTileFromDirection(directionToMove, teamToMove);
                SmartTile actualSmartTileToMoveTo = smartTileToMoveTo.GetSmartTileFromDirection(directionToMove, teamToMove);

                GameObject characterObjectToMove = smartTile.GetCharacterObject(teamToMove);

                if (actualTileToMoveTo != null && actualSmartTileToMoveTo != null)
                {
                    if (actualSmartTileToMoveTo.IsEmpty(teamToMove))
                    {
                        Debug.Log("Just moved " + characterData.TeamColor + characterData.CharacterName + " to space X-" + actualSmartTileToMoveTo.PositionNumberX + "_Y-" + actualSmartTileToMoveTo.PositionNumberY);
                        actualSmartTileToMoveTo.AddCharacterToSpace(characterData, characterObjectToMove);
                        smartTile.RemoveCharacterFromSpace(characterData);
                    }
                }

                DoNormalMove = false;

                HasAlreadyUsed = true;

                characterData.Range = 0;
            }
            else
            {
                //no enemy so we cant handle movement with this ability
                DoNormalMove = true;
                canHandle = false;
            }
        }
    }

    public override string Name { get => abilityName; }

    public override string Description { get => abilityDescription; }

    public override EnumAbilityType AbilityType { get => abilityType; }
    public override bool CanHandle { get => canHandle; }
}
