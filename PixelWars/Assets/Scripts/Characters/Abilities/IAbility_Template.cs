using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAbility_Template
{
    string name;

    public string Name { get => name; set => name = value; }

    /// <summary>
    /// Checks if we are able to use an ability
    /// </summary>
    /// <returns>bool to show if we should fire of an ability</returns>
    public abstract bool CheckIfAbilityIsPossible();
    /// <summary>
    /// Preform set ability
    /// </summary>
    void HandleAbility()
    {

    }
}
