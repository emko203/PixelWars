using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldHandler : MonoBehaviour
{
    [SerializeField] private GameObject battlefield;
    [SerializeField] private List<GameObject> characterPool;

    private List<Character> availableCharacters = new List<Character>();
    private List<SmartTile> smartTiles = new List<SmartTile>();

    #region Monobehaviour funtions

    private void Awake()
    {
        InitMap();
        InitAvailableUnits();
    }

    #endregion

    #region public Functions
    public void SpawnUnit(enumLane laneToSpawnIn, EnumTeams teamToSpawnFor, enumUnit unitToSpawn)
    {
        Vector3 spawnPos = GetSpawnFromLane(laneToSpawnIn, teamToSpawnFor);
        spawnPos.z = -1;

        foreach (GameObject character in characterPool)
        {
            if (character.GetComponent<Character>().UnitType == unitToSpawn)
            {
                GameObject go = Instantiate(character);

                go.transform.position = spawnPos;
            }
        }
    }

    public Vector3 GetSpawnFromLane(enumLane lane, EnumTeams team)
    {
        //Set pos y according to lane
        int posY = GetYSpawn(lane);

        //set xpos according to team
        int posX = GetXSpawn(team);

        return GetDrawPosWithCoordinates(posX, posY, team);
    }

    public Vector3 GetDrawPosWithCoordinates(int x, int y, EnumTeams team)
    {
        foreach (SmartTile tile in smartTiles)
        {
            if (tile.PositionNumberX == x && tile.PositionNumberY == y)
            {
                switch (team)
                {
                    case EnumTeams.Red:
                        return tile.RedTeamPlacement.position;
                    case EnumTeams.Blue:
                        return tile.BlueTeamPlacement.position;
                    default:
                       break;
                }
            }
        }

        return Vector3.zero;
    }

    #endregion

    #region Private funtions

    /// <summary>
    /// Initialize map
    /// </summary>
    private void InitMap()
    {
        SmartTile[] tempTiles = battlefield.GetComponentsInChildren<SmartTile>() as SmartTile[];

        foreach (SmartTile tile in tempTiles)
        {
            smartTiles.Add(tile);
        }
    }

    private void InitAvailableUnits()
    {
        foreach (GameObject character in characterPool)
        {
            availableCharacters.Add(character.GetComponent<Character>());
        }
    }

    /// <summary>
    /// Get Maximum amount on [0=X,1=Y]
    /// </summary>
    /// <returns>Maximum amount on [X,Y]</returns>
    private int[] GetMaximum()
    {
        int maximumX = 0;
        int maximumY = 0;

        foreach (SmartTile tile in smartTiles)
        {
            if (tile.PositionNumberX > maximumX)
            {
                maximumX = tile.PositionNumberX;
            }
            else if(tile.PositionNumberY > maximumY)
            {
                maximumY = tile.PositionNumberY;
            }
        }

        int[] maxArray = { maximumX, maximumY };
        return maxArray;
    }

    private int GetYSpawn(enumLane lane)
    {
        switch (lane)
        {
            case enumLane.LEFT_OUTER_LANE:
                return GetMaximum()[1];
            case enumLane.LEFT_CENTER_LANE:
                return GetMaximum()[1] - 1;
            case enumLane.RIGHT_OUTER_LANE:
                return GetMaximum()[1] - 3;
            case enumLane.RIGHT_CENTER_LANE:
                return GetMaximum()[1] - 2;
            default:
                return 0;
        }
    }

    private int GetXSpawn(EnumTeams team)
    {
        switch (team)
        {
            case EnumTeams.Red:
                return 1;
            case EnumTeams.Blue:
                return GetMaximum()[0];
            default:
                return 0;
        }
    }

    #endregion
}
