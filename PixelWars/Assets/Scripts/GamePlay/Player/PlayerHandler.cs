using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler
{
    private Player Player1 = new Player();
    private Player Player2 = new Player();

    public Player GetCurrentPlayer(EnumTeams currentTeam)
    {
        if (currentTeam == EnumTeams.Blue)
        {
            return Player2;
        }
        else
        {
            return Player1;
        }
    }
}
