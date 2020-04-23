using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory
{
    public static AbilityDefault GetAbilityFromCharacter(EnumUnit unitType)
    {
        switch (unitType)
        {

            case EnumUnit.ROGUE:
                return new RogueAbility();
            case EnumUnit.PRIEST:
                return new PriestAbility();
            case EnumUnit.GHOST:
            case EnumUnit.NONE:
            case EnumUnit.KNIGHT:
            case EnumUnit.ARCHER:
            case EnumUnit.MAGE:
            default:
                return new AbilityDefault();
        }
    }
}
