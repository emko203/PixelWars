using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHandler
{
    public void HandleMenu(GUI_Handler guiHandler)
    {
        guiHandler.FlipMenuState();
    }

    public EnumUnit HandleEndTurn(TurnHandler turnHandler, EnumUnit CurrentSelectedUnit)
    {
        turnHandler.SetNextState();
        CurrentSelectedUnit = EnumUnit.NONE;
        return CurrentSelectedUnit;
    }

    public EnumUnit HandleDeselectKey(EnumUnit CurrentSelectedUnit, SelectorManager selectorManager, GUI_Handler guihandler)
    {
        //Deselect the unit and then make the character selector available again so we can select a different character
        CurrentSelectedUnit = DeselectUnit(selectorManager, guihandler);
        return CurrentSelectedUnit;
    }

    private static EnumUnit DeselectUnit(SelectorManager selectorManager, GUI_Handler gui_handler)
    {
        EnumUnit CurrentSelectedUnit;
        selectorManager.HideLaneSelector();
        gui_handler.FlipSelectorState();
        CurrentSelectedUnit = EnumUnit.NONE;
        return CurrentSelectedUnit;
    }

    public EnumUnit HandleSelectKey(EnumTeams currentTeam, Player currentPlayer, EnumUnit currentSelectedUnit, SelectorManager selectorManager, BattlefieldHandler battlefieldHandler, GUI_Handler guihandler, TurnHandler turnHandler)
    {
        //do this if there is already a unit selected
        if (currentSelectedUnit != EnumUnit.NONE)
        {
            return SpawnUnit(currentTeam, currentPlayer, currentSelectedUnit, selectorManager, battlefieldHandler, guihandler,turnHandler);
        }
        else
        {
            return SelectUnit(currentTeam, currentPlayer, currentSelectedUnit, selectorManager, battlefieldHandler, guihandler);
        }
    }

    private EnumUnit SelectUnit(EnumTeams currentTeam, Player currentPlayer, EnumUnit currentSelectedUnit, SelectorManager selectorManager, BattlefieldHandler battlefieldHandler, GUI_Handler guihandler)
    {
        float HoveringManaCost = selectorManager.GetCurrentHoveringCharacter().ManaCost;

        if (currentPlayer.CurrentMana >= HoveringManaCost)
        {
            if (selectorManager.IsAlreadySelected(currentTeam) == false)
            {
                currentSelectedUnit = selectorManager.SelectCharacter(currentTeam, battlefieldHandler);
                guihandler.FlipSelectorState();
                return currentSelectedUnit;
            }
        }
        return currentSelectedUnit;
    }

    private EnumUnit SpawnUnit(EnumTeams currentTeam, Player currentPlayer, EnumUnit currentSelectedUnit, SelectorManager selectorManager, BattlefieldHandler battlefieldHandler, GUI_Handler guihandler, TurnHandler turnHandler)
    {
        EnumUnit UnitToReturn = currentSelectedUnit;
        //If no friendly unit is in spawn we can spawn a unit
        if (battlefieldHandler.CanSpawn(selectorManager.GetSelectedLane(), currentTeam))
        {
            //We Spawn in the selected unit in the currently selected lane and then pass the turn to the other player
            battlefieldHandler.SpawnUnit(selectorManager.GetSelectedLane(), currentTeam, currentSelectedUnit);
            UnitToReturn = DeselectUnit(selectorManager, guihandler);
            selectorManager.GrayOutCurrentHoveringCharacter();
            

            currentPlayer.PayMana(selectorManager.GetCurrentHoveringCharacter().ManaCost);
            guihandler.ManaCrystalsHandler.UpdateManaSprites(currentTeam, currentPlayer);
            if (currentPlayer.CurrentMana <= 0)
            {
                turnHandler.SetNextState();
            }
        }

        return UnitToReturn;
    }

    public void HandleDownKey(EnumUnit currentSelectedUnit, TurnHandler turnHandler, BattlefieldHandler battlefieldHandler, SelectorManager selectorManager)
    {
        //do this if there is already a unit selected
        if (currentSelectedUnit != EnumUnit.NONE)
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

    public void HandleUpKey(EnumUnit currentSelectedUnit, TurnHandler turnHandler, BattlefieldHandler battlefieldHandler, SelectorManager selectorManager)
    {
        //do this if there is already a unit selected
        if (currentSelectedUnit != EnumUnit.NONE)
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
}
