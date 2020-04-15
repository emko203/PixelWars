using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler
{
    EnumTeams[] validTeams = { EnumTeams.Blue, EnumTeams.Red };
    EnumTeams currentPlayerTurn;

    /// <summary>
    /// Set next player turn according to last turn
    /// </summary>
    public void SetNextTurn()
    {
        switch (currentPlayerTurn)
        {
            case EnumTeams.Red:
                currentPlayerTurn = EnumTeams.Blue;
                break;
            case EnumTeams.Blue:
                currentPlayerTurn = EnumTeams.Blue;
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
}
