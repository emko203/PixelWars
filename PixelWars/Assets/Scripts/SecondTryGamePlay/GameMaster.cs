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

    private EnumUnit CurrentSelectedUnit;
    private bool UnitSelected = false;

    private void Awake()
    {
        turnHandler.SelectRandomStartPlayer();
        //TODO: fill availablecharacters and replace allcharacters with it.
        selectorManager.SetupNewRound(turnHandler.CurrentPlayerTurn, AllCharacterPrefabs);
        selectorManager.HideLaneSelector();
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
                if (UnitSelected)
                {
                    selectorManager.MoveLaneSelectorSpriteUp(turnHandler.CurrentPlayerTurn,battlefieldHandler);
                }
                else
                {
                    selectorManager.MoveCharacterSelectorSpriteUp(turnHandler.CurrentPlayerTurn);
                }
                break;

            case EnumPressedKeyAction.DOWN:
                if (UnitSelected)
                {
                    selectorManager.MoveLaneSelectorSpriteDown(turnHandler.CurrentPlayerTurn, battlefieldHandler);
                }
                else
                {
                    selectorManager.MoveCharacterSelectorSpriteDown(turnHandler.CurrentPlayerTurn);
                }
                break;

            case EnumPressedKeyAction.SELECT:
                if (UnitSelected)
                {
                        battlefieldHandler.SpawnUnit(selectorManager.GetSelectedLane(), turnHandler.CurrentPlayerTurn, CurrentSelectedUnit);
                }
                else
                {
                    CurrentSelectedUnit = selectorManager.SelectCharacter(turnHandler.CurrentPlayerTurn, battlefieldHandler);
                    CharacterAnimator.SetBool("IsOrange", true);
                    UnitSelected = true;
                }
                break;

            case EnumPressedKeyAction.DESELECT:
                selectorManager.HideLaneSelector();
                CharacterAnimator.SetBool("IsOrange", false);
                UnitSelected = false;
                break;

            case EnumPressedKeyAction.NO_ACTION:
            case EnumPressedKeyAction.LEFT:
            case EnumPressedKeyAction.RIGHT:
            default:
                break;
        }
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
