using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Spell")]
public class Spell : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;
    public Sprite cardBack;

    public int manaCost;


}
