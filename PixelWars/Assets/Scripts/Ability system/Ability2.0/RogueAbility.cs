using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAbility : AbilityDefault
{
    public override EnumAbilityType AbilityType => EnumAbilityType.MOVE;
    public override string Name => "Vanish";
    public override string Description => "First time i encounter a enemy i move through them 2 spaces";

    private bool DidNotUseAbility = true;

    public override SmartTile MoveCharacterOnTile(SmartTile smartTile, EnumDirection directionToMove, EnumTeams teamToMove)
    {
        SmartTile tileToMoveTo = smartTile.GetSmartTileFromDirection(directionToMove, teamToMove);
        SmartTile smartTileToMoveTo = smartTile.GetSmartTileFromDirection(directionToMove, teamToMove);
        Character thisRogue = smartTile.GetCharacter(teamToMove);
        thisRogue.Range = 1;

        if (FightHandler.IsFight(smartTile, teamToMove) != null && DidNotUseAbility)
        {
            base.MoveCharacterOnTile(smartTile, directionToMove, teamToMove);

            thisRogue.Range = 0;
            //move 2 spaces
            base.MoveCharacterOnTile(smartTileToMoveTo, directionToMove, teamToMove);
            DidNotUseAbility = false;
        }
        else
        {
            //do normal move
            thisRogue.Range = 0;
            base.MoveCharacterOnTile(smartTile, directionToMove, teamToMove);
        }

        return smartTileToMoveTo;
    }
}
