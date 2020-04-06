using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public BattlefieldMapping Mapping;
    EnumTeams WhoIsPlaying;
    EnumBattleState CurrentState;

    void Start()
    {
        DetermineStartPlayer();
        CurrentState = EnumBattleState.START_TURN;
    }

    void Update()
    {
        if (CurrentState == EnumBattleState.START_TURN)
        {
            CurrentState = EnumBattleState.MOVEMENT;

            switch (WhoIsPlaying)
            {
                case EnumTeams.RED:
                    break;
                case EnumTeams.BLUE:
                    break;
                default:
                    break;
            }
        }
    }

    void DetermineStartPlayer()
    {
        int iStartPlayer = Random.Range(1, 2);

        if (iStartPlayer == 1)
        {
            WhoIsPlaying = EnumTeams.RED;
        }
        else
        {
            WhoIsPlaying = EnumTeams.BLUE;
        }
    }
}