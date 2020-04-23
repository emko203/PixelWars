using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Header("Cheats")]
    [Tooltip("When this is not set we will spawn only that unit")]
    [SerializeField] private EnumUnit unitToSpawn = EnumUnit.NONE;

    private PlayerHandler playerHandler = new PlayerHandler();
    private TurnHandler turnHandler;
    private KeyHandler keyHandler = new KeyHandler();

    private List<SmartTile> mapping = new List<SmartTile>();
    private List<GameObject> AvailableRedCharacters = new List<GameObject>();
    private List<GameObject> AvailableBlueCharacters = new List<GameObject>();

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
        guiHandler.InitUI(GetCharactersFromCurrentTeam());
    }

    private void LoadPlayerPrefs()
    {
        AvailableBlueCharacters.Clear();
        AvailableRedCharacters.Clear();

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
        FloatingTextController.Initialize();
        
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

            case EnumState.VICTORY:
                UpdateVictory();
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
        if (!battlefieldHandler.Busy)
        {
            selectorManager.HideLaneSelector();
            battlefieldHandler.MoveAllUnitsOnBattlefield(turnHandler.CurrentPlayerTurn);
        }

        if (battlefieldHandler.Done)
        {
            EnumTeams currentTeam = turnHandler.CurrentPlayerTurn;
            turnHandler.SetNextState();
            selectorManager.SetupNewRound(currentTeam, GetCharactersFromCurrentTeam());
            playerHandler.GetCurrentPlayer(currentTeam).GainMana();
            guiHandler.ManaCrystalsHandler.UpdateManaSprites(currentTeam, playerHandler.GetCurrentPlayer(currentTeam));
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

    private void UpdateVictory()
    {
        //TODO: Load victoryScene
        PlayerPrefs.SetInt("Victory", (int)turnHandler.CurrentPlayerTurn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    #endregion

    #region InputHandlers

    private void HandleInput()
    {
        EnumTeams CurrentTeam = turnHandler.CurrentPlayerTurn;
        //Check which input is pressed and then redirect to corresponding function of that key
        switch (inputManager.CheckKeyInput(turnHandler.CurrentPlayerTurn))
        {
            case EnumPressedKeyAction.UP:
                {
                    keyHandler.HandleUpKey(CurrentSelectedUnit, turnHandler, battlefieldHandler, selectorManager);
                    break;
                }

            case EnumPressedKeyAction.DOWN:
                {
                    keyHandler.HandleDownKey(CurrentSelectedUnit, turnHandler, battlefieldHandler, selectorManager);
                    break;
                }

            case EnumPressedKeyAction.SELECT:
                {
                    CurrentSelectedUnit = keyHandler.HandleSelectKey(CurrentTeam, playerHandler.GetCurrentPlayer(CurrentTeam), CurrentSelectedUnit,
                        selectorManager, battlefieldHandler, guiHandler, turnHandler);

                    if (Application.isEditor && unitToSpawn != EnumUnit.NONE)
                    {
                        CurrentSelectedUnit = unitToSpawn;
                    }

                    break;
                }

            case EnumPressedKeyAction.DESELECT:
                {
                    CurrentSelectedUnit = keyHandler.HandleDeselectKey(CurrentSelectedUnit, selectorManager, guiHandler);
                    break;
                }

            case EnumPressedKeyAction.END_TURN:
                {
                    CurrentSelectedUnit = keyHandler.HandleEndTurn(turnHandler, CurrentSelectedUnit);
                    break;
                }
            case EnumPressedKeyAction.OPEN_MENU:
                {
                    keyHandler.HandleMenu(guiHandler);
                    break;
                }

            //Break Because no action is assigned
            case EnumPressedKeyAction.NO_ACTION:
            case EnumPressedKeyAction.LEFT:
            case EnumPressedKeyAction.RIGHT:
            default:
                break;
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
