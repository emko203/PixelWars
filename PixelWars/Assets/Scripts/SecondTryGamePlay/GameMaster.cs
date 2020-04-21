using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [Header("Managers and Handlers")]
    [SerializeField] private GameObject battlefield;
    [SerializeField] private BattlefieldHandler battlefieldHandler;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private SelectorManager selectorManager;
    [SerializeField] private GUI_Handler guiHandler;
    
    [Space]
    [Header("AllCharacterPrefabs")]
    [SerializeField] private List<GameObject> RedCharacterPrefabs;
    [SerializeField] private List<GameObject> BlueCharacterPrefabs;
    [Space]
    [Header("ManaCrystals")]
    [SerializeField] private List<GameObject> blueCrystalsEmpty = new List<GameObject>();
    [SerializeField] private List<GameObject> blueCrystalsFull = new List<GameObject>();
    [SerializeField] private List<GameObject> redCrystalsFull = new List<GameObject>();
    [SerializeField] private List<GameObject> redCrystalsEmpty = new List<GameObject>();

    private Player Player1 = new Player();
    private Player Player2 = new Player();

    private List<SmartTile> mapping = new List<SmartTile>();
    private List<GameObject> AvailableRedCharacters = new List<GameObject>();
    private List<GameObject> AvailableBlueCharacters = new List<GameObject>();
    private TurnHandler turnHandler;

    private EnumUnit CurrentSelectedUnit = EnumUnit.NONE;

    #region Monobehaviour funtions
    private void Awake()
    {
        selectorManager.HideLaneSelector();
        LoadPlayerPrefs();
        turnHandler = new TurnHandler(guiHandler.RedArrow, guiHandler.BlueArrow, guiHandler.ArrowAnimator);
        turnHandler.SelectRandomStartPlayer();
    }

    private void LoadGuiMenu()
    {
        guiHandler.InitMenu(GetCharactersFromCurrentTeam());
    }

    private void LoadPlayerPrefs()
    {
        int AmountOfUnits = PlayerPrefs.GetInt("SelectionSize");

        for (int i = 0; i < AmountOfUnits; i++)
        {
            GameObject RedUnitToAddToPool = null;
            GameObject BlueUnitToAddToPool = null;

            EnumUnit SelectedUnit = IntToUnitEnum(PlayerPrefs.GetInt("Selection_" + i));

            RedUnitToAddToPool = GetUnitObjectFromEnum(SelectedUnit, EnumTeams.Red);
            BlueUnitToAddToPool = GetUnitObjectFromEnum(SelectedUnit, EnumTeams.Blue);

            AvailableBlueCharacters.Add(BlueUnitToAddToPool);
            AvailableRedCharacters.Add(RedUnitToAddToPool);
        }
    }

    private void Start()
    {
        turnHandler.SetNextState();
        turnHandler.SetTurnArrows();
        LoadGuiMenu();
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
        StopAllCoroutines();
    }

    #endregion

    #region Private functions

    private GameObject GetUnitObjectFromEnum(EnumUnit tempUnit, EnumTeams tempUnitTeam)
    {
        switch (tempUnitTeam)
        {
            case EnumTeams.Red:
                foreach (GameObject obUnit in RedCharacterPrefabs)
                {
                    if (obUnit.GetComponent<Character>().UnitType == tempUnit)
                    {
                        return obUnit;
                    }
                }
                break;
            case EnumTeams.Blue:
                foreach (GameObject obUnit in BlueCharacterPrefabs)
                {
                    if (obUnit.GetComponent<Character>().UnitType == tempUnit)
                    {
                        return obUnit;
                    }
                }
                break;
            default:
                break;
        }

        return null;
    }

    private EnumUnit IntToUnitEnum(int i)
    {
        return (EnumUnit)i;
    }

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
                return AvailableRedCharacters;
            case EnumTeams.Blue:
                return AvailableBlueCharacters;
            default:
                return null;
        }
    }

    private List<GameObject> GetCharactersFromOtherTeam()
    {
        switch (turnHandler.CurrentPlayerTurn)
        {
            case EnumTeams.Red:
                return AvailableBlueCharacters;
            case EnumTeams.Blue:
                return AvailableRedCharacters;
            default:
                return null;
        }
    }

    private void DeselectUnit()
    {
        //Deselect the unit and then make the character selector available again so we can select a different character
        selectorManager.HideLaneSelector();
        guiHandler.CharacterAnimator.SetBool("IsOrange", false);
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
            case EnumPressedKeyAction.OPEN_MENU:
                HandleMenu();
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

    private void HandleMenu()
    {
        guiHandler.FlipMenuState();
    }

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
                GetPlayerFromTurn().PayMana(selectorManager.GetCurrentHoveringCharacter().ManaCost);
                selectorManager.GrayOutCurrentHoveringCharacter();
                UpdateManaSprites();

                if (GetPlayerFromTurn().CurrentMana <= 0)
                {
                    turnHandler.SetNextState();
                }
            }
        }
        else
        {
            if (GetPlayerFromTurn().CurrentMana >= selectorManager.GetCurrentHoveringCharacter().ManaCost && !selectorManager.IsAlreadySelected(turnHandler.CurrentPlayerTurn))
            {
                CurrentSelectedUnit = selectorManager.SelectCharacter(turnHandler.CurrentPlayerTurn, battlefieldHandler);
                guiHandler.CharacterAnimator.SetBool("IsOrange", true);
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
