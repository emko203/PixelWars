using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler
{
    private  EnumTeams[] validTeams = { EnumTeams.Blue, EnumTeams.Red };
    private static EnumTeams  currentPlayerTurn;

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

    public static EnumTeams GetOtherTeam(EnumTeams currentTeam)
    {
        switch (currentTeam)
        {
            case EnumTeams.Red:
                return EnumTeams.Blue;
            case EnumTeams.Blue:
                return EnumTeams.Red;
            default:
                return currentTeam;
        }
    }

    public static EnumDirection GetDirectionFromTeam(EnumDirection directionToGet)
    {
        switch (currentPlayerTurn)
        {
            case EnumTeams.Red:
                return directionToGet;
            case EnumTeams.Blue:
                switch (directionToGet)
                {
                    case EnumDirection.UP:
                        return EnumDirection.DOWN;
                    case EnumDirection.DOWN:
                        return EnumDirection.UP;
                    case EnumDirection.LEFT:
                        return EnumDirection.RIGHT;
                    case EnumDirection.RIGHT:
                        return EnumDirection.LEFT;
                    default:
                        return EnumDirection.DOWN;
                }
            default:
                return directionToGet;
        }
    }

    public EnumTeams CurrentPlayerTurn { get => currentPlayerTurn; }
    public EnumState CurrentGameState { get => currentGameState;}
}
