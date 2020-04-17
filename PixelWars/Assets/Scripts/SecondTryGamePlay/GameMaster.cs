using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObject battlefield;
    [SerializeField] private List<GameObject> AllCharacterPrefabs;
    [SerializeField] private BattlefieldHandler battlefieldHandler;
    [SerializeField] private SelectorManager selectorManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Animator CharacterAnimator;

    private List<SmartTile> mapping = new List<SmartTile>();
    private List<GameObject> AvailableCharacters = new List<GameObject>();
    private TurnHandler turnHandler = new TurnHandler();

    private void Awake()
    {
        turnHandler.SelectRandomStartPlayer();
        //TODO: fill availablecharacters and replace allcharacters with it.
        selectorManager.SetupNewRound(turnHandler.CurrentPlayerTurn, AllCharacterPrefabs);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        switch (inputManager.CheckKeyInput(turnHandler.CurrentPlayerTurn))
        {
            case EnumPressedKeyAction.UP:
                selectorManager.MoveCharacterSelectorSpriteUp(turnHandler.CurrentPlayerTurn);
                break;

            case EnumPressedKeyAction.DOWN:
                //TODO: make sure to move right sprite according to Lane Selection or Character selection
                selectorManager.MoveCharacterSelectorSpriteDown(turnHandler.CurrentPlayerTurn);
                break;

            case EnumPressedKeyAction.SELECT:
                enumUnit SelectedUnit = selectorManager.SelectCharacter(turnHandler.CurrentPlayerTurn, battlefieldHandler);
                CharacterAnimator.SetBool("IsOrange", true);
                break;

            case EnumPressedKeyAction.DESELECT:
                break;

            case EnumPressedKeyAction.NO_ACTION:
            case EnumPressedKeyAction.LEFT:
            case EnumPressedKeyAction.RIGHT:
            default:
                break;
        }
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
