using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler
{
    private  EnumTeams[] validTeams = { EnumTeams.Blue, EnumTeams.Red };
    private EnumTeams currentPlayerTurn;

    private EnumState currentGameState = EnumState.WAIT;

    public void SetNextState()
    {
        switch (currentGameState)
        {
            
            case EnumState.START_OF_TURN:
                currentGameState = EnumState.PLAYER_TURN;
                break;
            case EnumState.PLAYER_TURN:
                currentGameState = EnumState.END_OF_TURN;
                break;
            case EnumState.END_OF_TURN:
                currentGameState = EnumState.START_OF_TURN;
                SetNextTurn();
                break;

            case EnumState.WAIT:
                currentGameState = EnumState.START_OF_TURN;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Set next player turn according to last turn
    /// </summary>
    private void SetNextTurn()
    {
        switch (currentPlayerTurn)
        {
            case EnumTeams.Red:
                currentPlayerTurn = EnumTeams.Blue;
                break;
            case EnumTeams.Blue:
                currentPlayerTurn = EnumTeams.Red;
                break;
            default:
                return;
        }
    }

    public bool IsPlayerTurn(EnumTeams team)
    {
        if (team == currentPlayerTurn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectRandomStartPlayer()
    { 
        currentPlayerTurn = validTeams[Random.Range(0, validTeams.Length)];
    }

    public EnumTeams CurrentPlayerTurn { get => currentPlayerTurn; }
    public EnumState CurrentGameState { get => currentGameState;}
}
