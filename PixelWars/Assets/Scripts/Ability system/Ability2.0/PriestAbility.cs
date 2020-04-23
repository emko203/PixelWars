using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestAbility : AbilityDefault
{
    private float healAmount = 1;

    public override EnumAbilityType AbilityType => EnumAbilityType.HEAL;
    public override string Name => "Heal";
    public override string Description => "Heal a friendly character by 1 instead of attacking";

    public override Character IsTargetInRange(SmartTile smartTile, EnumTeams teamToMove)
    {
        SmartTile TileInFrontOfMe = smartTile.GetSmartTileFromDirection(EnumDirection.UP, teamToMove);

        Character target = TileInFrontOfMe.GetCharacter(teamToMove);

        if (target == null)
        {
            return base.IsTargetInRange(smartTile, teamToMove);
        }
        else
        {
            return target;
        }
    }

    public override void HandleFight(SmartTile smarttile, EnumTeams teamToMove, Character target)
    {
        if (target.TeamColor == teamToMove)
        {
            target.healCharacter(healAmount);
        }
        else
        {
            base.HandleFight(smarttile,teamToMove,target);
        }
    }
}
