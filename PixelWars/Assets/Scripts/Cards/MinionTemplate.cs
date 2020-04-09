using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Card", menuName = "Cards/Minion")]
public class Minion : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;
    public Sprite cardBack;

    public float range;
    public int manaCost;
    public int attack;
    public int health;
}
