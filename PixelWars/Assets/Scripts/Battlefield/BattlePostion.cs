using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePostion : MonoBehaviour
{
    Vector3 blueTeamPosition;
    Vector3 redTeamPosition;

    int locationX;
    int locationY;

    CharacterTemplate blueCharacter;
    CharacterTemplate redCharacter;

    public BattlePostion(Vector3 blueTeamPosition, Vector3 redTeamPosition, int locationX, int locationY)
    {
        this.blueTeamPosition = blueTeamPosition;
        this.redTeamPosition = redTeamPosition;
        this.locationX = locationX;
        this.locationY = locationY;
    }

    public Vector3 BlueTeamPosition { get => blueTeamPosition;}
    public Vector3 RedTeamPosition { get => redTeamPosition;}
    public CharacterTemplate BlueCharacter { get => blueCharacter; set => blueCharacter = value; }
    public CharacterTemplate RedCharacter { get => redCharacter; set => redCharacter = value; }
    public int LocationX { get => locationX;}
    public int LocationY { get => locationY;}
}
