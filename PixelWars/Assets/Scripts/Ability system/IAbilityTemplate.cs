using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAbilityTemplate
{
    string Name { get; }
    EnumAbilityType AbilityType { get; }

    void HandleAbility();
}
