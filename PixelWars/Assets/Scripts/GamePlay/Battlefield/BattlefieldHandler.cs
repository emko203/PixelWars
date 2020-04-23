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

    private float TimeBetweenMoves = 1f;

    private bool done = false;
    private bool busy = false;

    #region Monobehaviour funtions

    private void Awake()
    {
        InitMap();
        InitAvailableUnits();
    }

    #endregion

    #region public Functions
    public void SpawnUnit(EnumLane laneToSpawnIn, EnumTeams teamToSpawnFor, EnumUnit unitToSpawn)
    {
        Vector3 spawnPos = GetSpawnFromLane(laneToSpawnIn, teamToSpawnFor);
        spawnPos.z = -2;

        foreach (GameObject character in characterPool)
        {
            if (character.GetComponent<Character>().UnitType == unitToSpawn && character.GetComponent<Character>().TeamColor == teamToSpawnFor)
            {

                GameObject go = Instantiate(character);

                go.transform.position = spawnPos;

                go.GetComponent<Character>().InitCharacter();
                
                //Connect character to smart tile
                bool placed = PlaceUnitOnTile(GetXSpawn(go.GetComponent<Character>().TeamColor), GetYSpawn(laneToSpawnIn), go);
            }
        }
    }

    public Vector3 GetSpawnFromLane(EnumLane lane, EnumTeams team)
    {
        //Set pos y according to lane
        int posY = GetYSpawn(lane);

        //set xpos according to team
        int posX = GetXSpawn(team);

        return GetDrawPosWithCoordinates(posX, posY, team);
    }

    public bool CanSpawn(EnumLane laneToCheck, EnumTeams currentTeam)
    {
        //Set pos y according to lane
        int posY = GetYSpawn(laneToCheck);

        //set xpos according to team
        int posX = GetXSpawn(currentTeam);

        SmartTile tempTile = GetSmartTileWithCoordinates(posX,posY,currentTeam);

        if (tempTile.IsEmpty(currentTeam))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public SmartTile GetSmartTileWithCoordinates(int x, int y, EnumTeams team)
    {
        foreach (SmartTile tile in smartTiles)
        {
            if (tile.PositionNumberX == x && tile.PositionNumberY == y)
            {
                return tile;
            }
        }

        return null;
    }

    public Vector3 GetDrawPosWithCoordinates(int x, int y, EnumTeams team)
    {
        foreach (SmartTile tile in smartTiles)
        {
            if (tile.PositionNumberX == x && tile.PositionNumberY == y)
            {
                return tile.GetPlacement(team).position;
            }
        }

        return Vector3.zero;
    }

    public void MoveAllUnitsOnBattlefield(EnumTeams teamToMove)
    {
        switch (teamToMove)
        {
            case EnumTeams.Red:
                StartCoroutine(RedMove(teamToMove));
                break;
            case EnumTeams.Blue:
                StartCoroutine(BlueMove(teamToMove));
                break;
            default:
                break;
        }
    }

    IEnumerator BlueMove(EnumTeams teamToMove)
    {
        IsBusy();
        for (int i = smartTiles.Count -1; i >= 0; i--)
        {
            if (!smartTiles[i].IsEmpty(teamToMove))
            {
                Color cRevert = new Color();
                cRevert = smartTiles[i].ThisTileObject.GetComponent<SpriteRenderer>().color ;
                smartTiles[i].ThisTileObject.GetComponent<SpriteRenderer>().color = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, Color.cyan.a);

                yield return new WaitForSeconds(TimeBetweenMoves);
                smartTiles[i].CallMove(EnumDirection.UP, teamToMove);

                smartTiles[i].ThisTileObject.GetComponent<SpriteRenderer>().color = cRevert;

            }
        }
        IsDone();
    }

    IEnumerator RedMove(EnumTeams teamToMove)
    {

        IsBusy();
        foreach (SmartTile tile in smartTiles)
        {
            if (!tile.IsEmpty(teamToMove))
            {
                Color cRevert = new Color();
                cRevert = tile.ThisTileObject.GetComponent<SpriteRenderer>().color;
                tile.ThisTileObject.GetComponent<SpriteRenderer>().color = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, Color.cyan.a);
                
                yield return new WaitForSeconds(TimeBetweenMoves);

                tile.CallMove(EnumDirection.UP, teamToMove);

                tile.ThisTileObject.GetComponent<SpriteRenderer>().color = cRevert;
            }
        }
        IsDone();
    }

    private void IsDone()
    {
        Done = true;
    }

    private void IsBusy()
    {
        Busy = true;
        Done = false;
    }

    #endregion

    #region Private funtions
    /// <summary>
    /// Place unit on tile if possible and return false if it is occupied
    /// </summary>
    /// <param name="x">x coordinates of tile</param>
    /// <param name="y">y coordinates of tile</param>
    /// <param name="characterObject">GameObject with character prefab to connect to tile</param>
    /// <returns>True if character has been placed, false if unit is already there</returns>
    private bool PlaceUnitOnTile(int x, int y, GameObject characterObject)
    {
        Character characterToPlace = characterObject.GetComponent<Character>();

        foreach (SmartTile tile in smartTiles)
        {
            if (tile.PositionNumberX == x && tile.PositionNumberY == y)
            {
                if (tile.IsEmpty(characterToPlace.TeamColor))
                {
                    tile.AddCharacterToSpace(characterToPlace, characterObject);
                }

                return false;
            }
        }

        return false;
    }

    /// <summary>
    /// Remove a unit from a tile
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="characterToRemove"></param>
    /// <returns></returns>
    private void RemoveUnitOnTile(int x, int y, Character characterToRemove)
    {
        foreach (SmartTile tile in smartTiles)
        {
            if (!tile.IsEmpty(characterToRemove.TeamColor))
            {
                if (tile.PositionNumberX == x && tile.PositionNumberY == y)
                {
                    tile.RemoveCharacterFromSpace(characterToRemove);
                }
            }
        }
    }

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
            else if (tile.PositionNumberY > maximumY)
            {
                maximumY = tile.PositionNumberY;
            }
        }

        int[] maxArray = { maximumX, maximumY };
        return maxArray;
    }

    private int GetYSpawn(EnumLane lane)
    {
        switch (lane)
        {
            case EnumLane.LEFT_OUTER_LANE:
                return GetMaximum()[1];
            case EnumLane.LEFT_CENTER_LANE:
                return GetMaximum()[1] - 1;
            case EnumLane.RIGHT_OUTER_LANE:
                return GetMaximum()[1] - 3;
            case EnumLane.RIGHT_CENTER_LANE:
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

    public static SmartTile GetTileFromDirectionAhead(int amountToLookAhead, SmartTile whereToStart, EnumTeams currentTeam, EnumDirection directionToLookIn)
    {
        SmartTile TileToReturn = whereToStart;

        if (amountToLookAhead != 0)
        {
            for (int i = 1; i <= amountToLookAhead; i++)
            {
                if (TileToReturn != null)
                {
                    TileToReturn = TileToReturn.GetSmartTileFromDirection(directionToLookIn, currentTeam);
                }
            }
        }

        return TileToReturn;
    }

    public bool Done { get => done; set => done = value; }
    public bool Busy { get => busy; set => busy = value; }
}
