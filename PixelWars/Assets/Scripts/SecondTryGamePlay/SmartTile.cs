using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartTile : MonoBehaviour
{
    [SerializeField] private int positionNumberX;
    [SerializeField] private int positionNumberY;

    [SerializeField] private GameObject tileLeft = null;
    [SerializeField] private GameObject tileRight = null;
    [SerializeField] private GameObject tileBottom = null;
    [SerializeField] private GameObject tileTop = null;

    [SerializeField] private Transform redTeamPlacement;
    [SerializeField] private Transform blueTeamPlacement;

    public int PositionNumberX { get => positionNumberX; set => positionNumberX = value; }
    public int PositionNumberY { get => positionNumberY; set => positionNumberY = value; }
    public GameObject TileLeft { get => tileLeft; set => tileLeft = value; }
    public GameObject TileRight { get => tileRight; set => tileRight = value; }
    public GameObject TileBottom { get => tileBottom; set => tileBottom = value; }
    public GameObject TileTop { get => tileTop; set => tileTop = value; }
    public Transform RedTeamPlacement { get => redTeamPlacement; set => redTeamPlacement = value; }
    public Transform BlueTeamPlacement { get => blueTeamPlacement; set => blueTeamPlacement = value; }
}
