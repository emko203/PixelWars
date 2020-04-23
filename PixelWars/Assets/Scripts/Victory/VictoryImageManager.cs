using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryImageManager : MonoBehaviour
{
    [SerializeField] private GameObject RedTeamVictory;
    [SerializeField] private GameObject BlueTeamVictory;

    [HideInInspector] public EnumTeams winningTeam;

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerPrefWinner();
        ShowWinningTeamImage();
    }

    private void ShowWinningTeamImage()
    {
        switch (winningTeam)
        {
            case EnumTeams.Red:
                RedTeamVictory.SetActive(true);
                break;
            case EnumTeams.Blue:
                BlueTeamVictory.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void LoadPlayerPrefWinner()
    {
        //winningTeam = EnumTeams.Blue;
        //winningTeam = EnumTeams.Red;
        winningTeam = (EnumTeams)PlayerPrefs.GetInt("Victory");
    }
}
