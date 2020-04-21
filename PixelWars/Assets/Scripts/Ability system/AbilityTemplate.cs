using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityTemplate : MonoBehaviour
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract EnumAbilityType AbilityType { get; }

    public abstract bool CanHandle { get; }

    public abstract void HandleAbility();
    public abstract void HandleAbility(SmartTile smartTile, EnumDirection directionToMove, EnumTeams teamToMove);
}
