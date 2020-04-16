using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject battlefield;

    public BattlefieldHandler battlefieldHandler;

    private List<SmartTile> mapping = new List<SmartTile>();
    private TurnHandler turnHandler = new TurnHandler();

    private void Awake()
    {
        turnHandler.SelectRandomStartPlayer();
    }

    //Do on button click
    public void SpawnUnit(int laneNumber)
    {
        enumUnit unitToSpawn = 0;
        enumLane laneToSpawnIn = (enumLane)laneNumber;

        battlefieldHandler.SpawnUnit(laneToSpawnIn, turnHandler.CurrentPlayerTurn, unitToSpawn);
    }
    
    private void InitializeBattlefield()
    {
        SmartTile[] smartTiles = battlefield.GetComponentsInChildren<SmartTile>() as SmartTile[];

        foreach (SmartTile tile in smartTiles)
        {
            mapping.Add(tile);
        }
    }
}
