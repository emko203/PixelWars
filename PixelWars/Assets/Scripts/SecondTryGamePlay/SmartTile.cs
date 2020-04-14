using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartTile : MonoBehaviour
{
    public int positionNumberX;
    public int positionNumberY;

    public GameObject TileLeft = null;
    public GameObject TileRight = null;
    public GameObject TileBottom = null;
    public GameObject TileTop = null;

    public Transform RedTeamPlacement;
    public Transform BlueTeamPlacement;
}
