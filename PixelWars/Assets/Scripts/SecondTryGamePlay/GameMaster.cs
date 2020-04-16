using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObject battlefield;
    [SerializeField] private List<GameObject> AllCharacterPrefabs;
    [SerializeField] private BattlefieldHandler battlefieldHandler;
    [SerializeField] private SelectorManager selectorManager;


    private List<SmartTile> mapping = new List<SmartTile>();
    private List<GameObject> AvailableCharacters = new List<GameObject>();
    private TurnHandler turnHandler = new TurnHandler();

    private void Awake()
    {
        turnHandler.SelectRandomStartPlayer();
        //TODO: fill availablecharacters and replace allcharacters with it.
        selectorManager.SpawnSelectableCharacterSet(turnHandler.CurrentPlayerTurn, AllCharacterPrefabs);
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
