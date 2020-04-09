using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility_Template
{
    string Name { get; set; }

    /// <summary>
    /// Checks if we are able to use an ability
    /// </summary>
    /// <returns>bool to show if we should fire of an ability</returns>
    bool CheckIfAbilityIsPossible();
    /// <summary>
    /// Preform set ability
    /// </summary>
    void HandleAbility();
}
