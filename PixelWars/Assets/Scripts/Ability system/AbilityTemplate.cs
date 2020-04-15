using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityTemplate : MonoBehaviour
{
    public abstract string Name { get; }
    public abstract EnumAbilityType AbilityType { get; }

    public abstract void HandleAbility();
}
