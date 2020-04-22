using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestAbility : AbilityTemplate
{
    public override string Name => "Heal";

    public override string Description => "Heal an friendly unit instead of attacking";

    public override EnumAbilityType AbilityType => EnumAbilityType.HEAL;

    public override bool CanHandle => canHandle;

    private bool canHandle = false;
    private float healAmount = 1;

    public override void HandleAbility()
    {
        return;
    }

    public override void HandleMoveAbility(SmartTile smartTile, EnumDirection directionToMove, EnumTeams teamToMove)
    {
        return;
    }

    public override void HandleHealAbility(Character characterToHeal)
    {
        if (characterToHeal != null)
        {
            characterToHeal.healCharacter(healAmount);
            FloatingTextController.CreateFloatingText(healAmount.ToString(), characterToHeal.transform, true);
            canHandle = true;
        }
        else
        {
            canHandle = false;
        }
    }
}
