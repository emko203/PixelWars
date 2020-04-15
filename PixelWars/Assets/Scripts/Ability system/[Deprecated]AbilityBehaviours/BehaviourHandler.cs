using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BehaviourHandler : MonoBehaviour
{
    [SerializeField] private List<AbilityBehaviour> lstAbilityBehaviours = new List<AbilityBehaviour>();

    public void HandleAbilities()
    {
        foreach (AbilityBehaviour behaviour in lstAbilityBehaviours)
        {
            
        }
    }
}
