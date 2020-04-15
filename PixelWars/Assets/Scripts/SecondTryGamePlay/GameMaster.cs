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

    public void SpawnUnit(enumUnit unit)
    {
        
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
