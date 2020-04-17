using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObject battlefield;
    [SerializeField] private List<GameObject> RedCharacterPrefabs;
    [SerializeField] private List<GameObject> BlueCharacterPrefabs;
    [SerializeField] private BattlefieldHandler battlefieldHandler;
    [SerializeField] private SelectorManager selectorManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Animator CharacterAnimator;

    private Player Player1 = new Player();
    private Player Player2 = new Player();

    private List<SmartTile> mapping = new List<SmartTile>();
    private List<GameObject> AvailableCharacters = new List<GameObject>();
    private TurnHandler turnHandler = new TurnHandler();

    private EnumUnit CurrentSelectedUnit = EnumUnit.NONE;

    private void Awake()
    {
        turnHandler.SelectRandomStartPlayer();
        
        selectorManager.HideLaneSelector();
    }

    private void Start()
    {
        turnHandler.SetNextState();
    }

    private void Update()
    {
        switch (turnHandler.CurrentGameState)
        {
            
            case EnumState.START_OF_TURN:
                UpdateStartOfTurn();
                break;
            case EnumState.PLAYER_TURN:
                UpdatePlayerTurn();
                break;
            case EnumState.END_OF_TURN:
                UpdateEndOfTurn();
                break;

            case EnumState.WAIT:
            default:
                break;
        }
    }

    #region TURN UPDATES (MAIN GAME LOOP)

    //Do this at the start of turn
    private void UpdateStartOfTurn()
    {
        //TODO: fill availablecharacters with data from other scene and replace allcharacters with it.
        if (!battlefieldHandler.Busy)
        {
            selectorManager.HideLaneSelector();
            battlefieldHandler.MoveAllUnitsOnBattlefield(turnHandler.CurrentPlayerTurn);
        }

        if (battlefieldHandler.Done)
        {
            turnHandler.SetNextState();
            selectorManager.SetupNewRound(turnHandler.CurrentPlayerTurn, GetCharactersFromCurrentTeam());
            battlefieldHandler.Busy = false;
        }
        
    }

    //Do this during when a turn is bussy
    private void UpdatePlayerTurn()
    {
        HandleInput();
    }

    //Do this at the end of turn
    private void UpdateEndOfTurn()
    {
        turnHandler.SetNextState();
    }

    #endregion

    private List<GameObject> GetCharactersFromCurrentTeam()
    {
        switch (turnHandler.CurrentPlayerTurn)
        {
            case EnumTeams.Red:
                return RedCharacterPrefabs;
            case EnumTeams.Blue:
                return BlueCharacterPrefabs;
            default:
                return null;
        }
    }

    private void HandleInput()
    {
        //Check which input is pressed and then redirect to corresponding function of that key
        switch (inputManager.CheckKeyInput(turnHandler.CurrentPlayerTurn))
        {
            case EnumPressedKeyAction.UP:
                HandleUpKey();
                break;

            case EnumPressedKeyAction.DOWN:
                HandleDownKey();
                break;

            case EnumPressedKeyAction.SELECT:
                HandleSelectKey();
                break;

            case EnumPressedKeyAction.DESELECT:
                HandleDeselectKey();
                break;

            //Break Because no action is assigned
            case EnumPressedKeyAction.NO_ACTION:
            case EnumPressedKeyAction.LEFT:
            case EnumPressedKeyAction.RIGHT:
            default:
                break;
        }
    }

    private void DeselectUnit()
    {
        //Deselect the unit and then make the character selector available again so we can select a different character
        selectorManager.HideLaneSelector();
        CharacterAnimator.SetBool("IsOrange", false);
        CurrentSelectedUnit = EnumUnit.NONE;
    }

    #region HandelingKeys

    private void HandleDeselectKey()
    {
        //Deselect the unit and then make the character selector available again so we can select a different character
        DeselectUnit();
    }

    private void HandleSelectKey()
    {
        //do this if there is already a unit selected
        if (CurrentSelectedUnit != EnumUnit.NONE)
        {

            //We Spawn in the selected unit in the currently selected lane and then pass the turn to the other player
            battlefieldHandler.SpawnUnit(selectorManager.GetSelectedLane(), turnHandler.CurrentPlayerTurn, CurrentSelectedUnit);
            DeselectUnit();

            //TODO: Add mana to this so that we only end the turn if we dont have mana or if we end the turn by button
            turnHandler.SetNextState();
        }
        else
        {
            CurrentSelectedUnit = selectorManager.SelectCharacter(turnHandler.CurrentPlayerTurn, battlefieldHandler);
            CharacterAnimator.SetBool("IsOrange", true);
        }
    }

    private void HandleDownKey()
    {
        //do this if there is already a unit selected
        if (CurrentSelectedUnit != EnumUnit.NONE)
        {

            //we move the Lane selector according to the current player
            selectorManager.MoveLaneSelectorSpriteDown(turnHandler.CurrentPlayerTurn, battlefieldHandler);
        }
        //do this if there is no unit selected
        else
        {

            //we move the Character selector according to the current player
            selectorManager.MoveCharacterSelectorSpriteDown(turnHandler.CurrentPlayerTurn);
        }
    }

    private void HandleUpKey()
    {
        //do this if there is already a unit selected
        if (CurrentSelectedUnit != EnumUnit.NONE)
        {
            //we move the Lane selector according to the current player
            selectorManager.MoveLaneSelectorSpriteUp(turnHandler.CurrentPlayerTurn, battlefieldHandler);
        }
        //do this if there is no unit selected
        else
        {
            //we move the Character selector according to the current player
            selectorManager.MoveCharacterSelectorSpriteUp(turnHandler.CurrentPlayerTurn);
        }
    }

    #endregion

    private void InitializeBattlefield()
    {
        SmartTile[] smartTiles = battlefield.GetComponentsInChildren<SmartTile>() as SmartTile[];

        foreach (SmartTile tile in smartTiles)
        {
            mapping.Add(tile);
        }
    }
}
