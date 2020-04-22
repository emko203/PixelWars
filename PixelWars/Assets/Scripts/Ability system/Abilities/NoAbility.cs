using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAbility : AbilityTemplate
{
    public override string Name => "No Ability";

    public override string Description => "";

    public override EnumAbilityType AbilityType => EnumAbilityType.NONE;

    public override bool CanHandle => false;

    public override void HandleAbility()
    {
        return;
    }

    public override void HandleHealAbility(Character toHeal)
    {
        return;
    }

    public override void HandleMoveAbility(SmartTile smartTile, EnumDirection directionToMove, EnumTeams teamToMove)
    {
        return;
    }
}
