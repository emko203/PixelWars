using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityBehaviour))]
public class MyScriptEditor : Editor
{
    private int spacing = 5;
    private int bigSpace = 20;
    private int GroupSpace = 10;

    public override void OnInspectorGUI()
    {
        var myScript = target as AbilityBehaviour;

        GUILayout.Space(bigSpace);
        myScript.IsCloseCombat = GUILayout.Toggle(myScript.IsCloseCombat, "IsCloseCombat");
        GUILayout.Space(spacing);
        myScript.IsRanged = GUILayout.Toggle(myScript.IsRanged, "IsRanged");
        if (myScript.IsRanged)
        {
            myScript.AmountOfRange = EditorGUILayout.IntSlider("AmountOfRange", myScript.AmountOfRange, 0, 100);
        }
        GUILayout.Space(GroupSpace);
        GUILayout.Label("-------------------");
        GUILayout.Space(GroupSpace);
        myScript.IsDefensive = GUILayout.Toggle(myScript.IsDefensive, "IsDefensive");
        GUILayout.Space(spacing);
        myScript.IsMovement = GUILayout.Toggle(myScript.IsMovement, "IsMovement");
        GUILayout.Space(spacing);
        myScript.IsHealing = GUILayout.Toggle(myScript.IsHealing, "IsHealing");
        GUILayout.Space(spacing);
        if (myScript.IsHealing)
        {
            myScript.AmountOfHealing = EditorGUILayout.IntSlider("AmountOfHealing", myScript.AmountOfHealing, 0, 100);
            GUILayout.Space(spacing);
        }
        myScript.IsDamaging = GUILayout.Toggle(myScript.IsDamaging, "IsDamaging"); 
        GUILayout.Space(spacing);
        if (myScript.IsDamaging)
        {
            myScript.AmountOfDamage = EditorGUILayout.IntSlider("AmountOfDamage", myScript.AmountOfDamage, 0, 100);
            GUILayout.Space(spacing);
            myScript.IsAOE = GUILayout.Toggle(myScript.IsAOE, "IsAOE");
            GUILayout.Space(spacing);
            if (myScript.IsAOE)
            {
                myScript.AmountOfSpaces = EditorGUILayout.IntSlider("AmountOfSpaces", myScript.AmountOfSpaces, 0, 100);
                GUILayout.Space(spacing);
                myScript.IsNotAllDirections = GUILayout.Toggle(myScript.IsNotAllDirections, "IsNotAllDirections");
                GUILayout.Space(spacing);
                if (myScript.IsNotAllDirections)
                {
                    myScript.Direction = (EnumDirection)EditorGUILayout.EnumPopup("Direction", myScript.Direction);
                    GUILayout.Space(spacing);
                }
                GUILayout.Space(GroupSpace);
                GUILayout.Label("-------------------");
                GUILayout.Space(GroupSpace);
            }

            myScript.IsDOT = GUILayout.Toggle(myScript.IsDOT, "IsDOT");
            GUILayout.Space(spacing);
            if (myScript.IsDOT)
            {
                myScript.TimeBetweenTicks = EditorGUILayout.IntSlider("TimeBetweenTicks", myScript.TimeBetweenTicks, 0, 100);
                GUILayout.Space(spacing);
                myScript.DamagePerTick = EditorGUILayout.IntSlider("DamagePerTick", myScript.DamagePerTick, 0, 100);
                GUILayout.Space(spacing);
            }
        }
        GUILayout.Space(GroupSpace);
        GUILayout.Label("-------------------");
        GUILayout.Space(GroupSpace);
        myScript.CanCastOnSelf = GUILayout.Toggle(myScript.CanCastOnSelf, "CanCastOnSelf");
        GUILayout.Space(spacing);
        myScript.NeedsTarget = GUILayout.Toggle(myScript.NeedsTarget, "NeedsTarget");
        GUILayout.Space(spacing);
        if (myScript.NeedsTarget)
        {
            myScript.NeedsLineOfSight = GUILayout.Toggle(myScript.NeedsLineOfSight, "NeedsLineOfSight");
            GUILayout.Space(spacing);
        }
        GUILayout.Space(GroupSpace);
        GUILayout.Label("-------------------");
        GUILayout.Space(GroupSpace);
        myScript.HasCooldown = GUILayout.Toggle(myScript.HasCooldown, "HasCooldown");
        GUILayout.Space(spacing);
        if (myScript.HasCooldown)
        {
            myScript.AmountOfCooldown = EditorGUILayout.IntSlider("AmountOfCooldown", myScript.AmountOfCooldown, 0, 100);
            GUILayout.Space(spacing);
        }
        GUILayout.Space(GroupSpace);
        GUILayout.Label("-------------------");
        GUILayout.Space(GroupSpace);
        myScript.HasPull = GUILayout.Toggle(myScript.HasPull, "HasPull");
        GUILayout.Space(spacing);
        if (myScript.HasPull)
        {
            myScript.AmountOfPull = EditorGUILayout.IntSlider("AmountOfPull", myScript.AmountOfPull, 0, 100);
            GUILayout.Space(spacing);
        }
        myScript.HasPushBack = GUILayout.Toggle(myScript.HasPushBack, "HasPushBack");
        GUILayout.Space(spacing);
        if (myScript.HasPushBack)
        {
            myScript.AmountOfPush = EditorGUILayout.IntSlider("AmountOfPush", myScript.AmountOfPush, 0, 100);
            GUILayout.Space(spacing);
        }
    }
}
