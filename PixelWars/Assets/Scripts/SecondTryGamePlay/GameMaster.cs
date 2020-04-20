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
    [SerializeField] private GameObject blueArrow;
    [SerializeField] private GameObject redArrow;
    [SerializeField] private Animator arrowAnimator;

    [SerializeField] private List<GameObject> blueCrystalsEmpty = new List<GameObject>();
    [SerializeField] private List<GameObject> blueCrystalsFull = new List<GameObject>();
    [SerializeField] private List<GameObject> redCrystalsFull = new List<GameObject>();
    [SerializeField] private List<GameObject> redCrystalsEmpty = new List<GameObject>();

    private Player Player1 = new Player();
    private Player Player2 = new Player();

    private List<SmartTile> mapping = new List<SmartTile>();
    private List<GameObject> AvailableCharacters = new List<GameObject>();
    private TurnHandler turnHandler;

    private EnumUnit CurrentSelectedUnit = EnumUnit.NONE;

    #region Monobehaviour funtions
    private void Awake()
    {
        selectorManager.HideLaneSelector();

        turnHandler = new TurnHandler(redArrow,blueArrow, arrowAnimator);
        turnHandler.SelectRandomStartPlayer();
    }

    private void Start()
    {
        turnHandler.SetNextState();
        turnHandler.SetTurnArrows();
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

    #endregion

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
            GetPlayerFromTurn().GainMana();
            UpdateManaSprites();
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
        selectorManager.HideLaneSelector();
        selectorManager.HideCharacterSelector();
        turnHandler.SetNextState();
    }

    #endregion

    #region Private functions
    private void UpdateManaSprites()
    {
        GetPlayerFromTurn().UpdateManaSprites(GetCrystalsFromTeam(false), GetCrystalsFromTeam(true));
    }

    private List<GameObject> GetCrystalsFromTeam( bool emptyCrystals)
    {
        if (emptyCrystals)
        {
            switch (turnHandler.CurrentPlayerTurn)
            {
                case EnumTeams.Red:
                    return redCrystalsEmpty;
                case EnumTeams.Blue:
                    return blueCrystalsEmpty;
                default:
                    return null;
            }
        }
        else
        {
            switch (turnHandler.CurrentPlayerTurn)
            {
                case EnumTeams.Red:
                    return redCrystalsFull;
                case EnumTeams.Blue:
                    return blueCrystalsFull;
                default:
                    return null;
            }
        }
    }

    private Player GetPlayerFromTurn()
    {
        if (turnHandler.CurrentPlayerTurn == EnumTeams.Blue)
        {
            return Player2;
        }
        else
        {
            return Player1;
        }
    }

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

    private void DeselectUnit()
    {
        //Deselect the unit and then make the character selector available again so we can select a different character
        selectorManager.HideLaneSelector();
        CharacterAnimator.SetBool("IsOrange", false);
        CurrentSelectedUnit = EnumUnit.NONE;
    }

    #endregion

    #region InputHandlers

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

            case EnumPressedKeyAction.END_TURN:
                HandleEndTurn();
                break;

            //Break Because no action is assigned
            case EnumPressedKeyAction.NO_ACTION:
            case EnumPressedKeyAction.LEFT:
            case EnumPressedKeyAction.RIGHT:
            default:
                break;
        }
    }

    #endregion

    #region HandelingKeys

    private void HandleEndTurn()
    {
        turnHandler.SetNextState();
    }

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
            //If no friendly unit is in spawn we can spawn a unit
            if (battlefieldHandler.CanSpawn(selectorManager.GetSelectedLane(), turnHandler.CurrentPlayerTurn))
            {
                //We Spawn in the selected unit in the currently selected lane and then pass the turn to the other player
                battlefieldHandler.SpawnUnit(selectorManager.GetSelectedLane(), turnHandler.CurrentPlayerTurn, CurrentSelectedUnit);
                DeselectUnit();
                GetPlayerFromTurn().PayMana(selectorManager.GetCurrentHoveringCharacter().Data.ManaCost);
                UpdateManaSprites();

                if (GetPlayerFromTurn().CurrentMana <= 0)
                {
                    turnHandler.SetNextState();
                }
            }
        }
        else
        {
            if (GetPlayerFromTurn().CurrentMana >= selectorManager.GetCurrentHoveringCharacter().Data.ManaCost)
            {
                
                CurrentSelectedUnit = selectorManager.SelectCharacter(turnHandler.CurrentPlayerTurn, battlefieldHandler);
                CharacterAnimator.SetBool("IsOrange", true);
            }
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

    #region Initialization

    private void InitializeBattlefield()
    {
        SmartTile[] smartTiles = battlefield.GetComponentsInChildren<SmartTile>() as SmartTile[];

        foreach (SmartTile tile in smartTiles)
        {
            mapping.Add(tile);
        }
    }

    #endregion
}
