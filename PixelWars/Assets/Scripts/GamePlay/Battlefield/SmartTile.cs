using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartTile : MonoBehaviour
{
    [SerializeField] private int positionNumberX;
    [SerializeField] private int positionNumberY;

    [SerializeField] private GameObject thisTileObject = null;

    [SerializeField] private GameObject tileLeft = null;
    [SerializeField] private GameObject tileRight = null;
    [SerializeField] private GameObject tileBottom = null;
    [SerializeField] private GameObject tileTop = null;

    [SerializeField] private Transform redTeamPlacement;
    [SerializeField] private Transform blueTeamPlacement;

    private Character blueCharacterOnTile = null;
    private Character redCharacterOnTile = null;

    private GameObject redCharacterGameObject = null;
    private GameObject blueCharacterGameObject = null;
    /// <summary>
    /// Link and move character to space in given direction Returns True if Succesfull and False if unsuccesfull
    /// </summary>
    /// <param name="directionToMove">Direction to move to</param>
    /// <param name="teamToMove">Team that moves from this tile</param>
    /// <returns>True if Succesfull and False if unsuccesfull</returns>
    public bool MoveDirection(EnumDirection directionToMove, EnumTeams teamToMove)
    {
        GameObject tileToMoveTo = GetTileFromDirection(directionToMove, teamToMove);
        SmartTile smartTileToMoveTo = GetSmartTileFromDirection(directionToMove, teamToMove);

        GameObject characterObjectToMove = GetCharacterObject(teamToMove);
        Character characterData = GetCharacter(teamToMove);

        Character enemy = FightHandler.IsFight(this, teamToMove);

        //if there is a friendly in front of us we handle healing ability
        if (characterData.AbilityTemplate.AbilityType == EnumAbilityType.HEAL)
        {
            if (smartTileToMoveTo != null)
            {
                characterData.AbilityTemplate.HandleHealAbility(smartTileToMoveTo.GetCharacter(teamToMove));
            }
        }

        if (characterData.AbilityTemplate.AbilityType == EnumAbilityType.MOVE)
        {
            characterData.AbilityTemplate.HandleMoveAbility(this, directionToMove, teamToMove);
        }

        if (!characterData.AbilityTemplate.CanHandle)
        {
            //if there is no fight on this tile withtin range then we move to next tile
            if (enemy == null)
            {

                //only move if the next tile excists
                if (tileToMoveTo != null && smartTileToMoveTo != null)
                {
                    if (smartTileToMoveTo.IsEmpty(teamToMove))
                    {
                        Debug.Log("Just moved " + characterData.TeamColor + characterData.CharacterName + " to space X-" + smartTileToMoveTo.positionNumberX + "_Y-" + smartTileToMoveTo.PositionNumberY);
                        smartTileToMoveTo.AddCharacterToSpace(characterData, characterObjectToMove);
                        this.RemoveCharacterFromSpace(characterData);
                        return true;
                    }
                }
                else
                {
                    //if no tile excists we are at the end and this team has won
                    TurnHandler.SetVictoryState();
                }
            }
            else
            {
                HandleFight(teamToMove, enemy);
            }
            return false;
        }
        

        //Check for victory
        switch ((teamToMove))
        {
            case EnumTeams.Red:
                if (this.positionNumberX == 5 && this.IsEmpty(EnumTeams.Blue))
                {
                    Debug.Log("RED TEAM WINS");
                }
                break;
            case EnumTeams.Blue:
                if (this.positionNumberX == 1 && this.IsEmpty(EnumTeams.Red))
                {
                    Debug.Log("BLUE TEAM WINS");
                }
                break;
        }


        return false;
    }

    private void HandleFight(EnumTeams teamToMove, Character enemy)
    {
        FightHandler.FightWithClosestEnemy(this, teamToMove, enemy);
    }

    /// <summary>
    /// Check if tile in given direction is empty/available for given team return true if empty / false if not empty
    /// </summary>
    /// <param name="team">Given team</param>
    /// <param name="direction">Given direction</param>
    /// <returns>true if empty / false if not empty</returns>
    public bool IsEmpty(EnumTeams team, EnumDirection direction)
    {
        return GetSmartTileFromDirection(direction,team).IsEmpty(team);
    }
    /// <summary>
    /// Check if tile is empty/available for given team return true if empty / false if not empty
    /// </summary>
    /// <param name="team">Given team</param>
    /// <returns>true if empty / false if not empty</returns>
    public bool IsEmpty(EnumTeams team)
    {
        if (team == EnumTeams.Blue)
        {
            if (blueCharacterOnTile == null)
                return true;
        }else if(team == EnumTeams.Red)
        {
            if (redCharacterOnTile == null)
                return true;
        }
        

        return false;
    }
    /// <summary>
    /// Draw character on tile and also link it to this tile
    /// </summary>
    /// <param name="cToAdd">character to link</param>
    /// <param name="goToAdd">object to draw</param>
    public void AddCharacterToSpace(Character cToAdd, GameObject goToAdd)
    {
        Transform tempPos = GetPlacement(cToAdd.TeamColor);
        Transform StartMarker = goToAdd.transform;
        //goToAdd.transform.position = new Vector3(tempPos.x,tempPos.y,-1);
        StartCoroutine(handleAnimation(StartMarker, tempPos,goToAdd.transform,cToAdd.TeamColor));
        
        switch (cToAdd.TeamColor)
        {
            case EnumTeams.Red:
                this.redCharacterGameObject = goToAdd;
                this.redCharacterOnTile = cToAdd;
                break;
            case EnumTeams.Blue:
                this.blueCharacterGameObject = goToAdd;
                this.blueCharacterOnTile = cToAdd;
                break;
            default:
                break;
        }
    }

    IEnumerator handleAnimation(Transform startpos, Transform endpos, Transform toMove, EnumTeams currentTeam)
    {
        // Transforms to act as start and end markers for the journey.
        Transform startMarker = startpos;
        Transform endMarker = endpos;

        endMarker.position = new Vector3(endMarker.position.x, endMarker.position.y, -2);
        startMarker.position = new Vector3(startMarker.position.x, startMarker.position.y, -2);
        toMove.position = new Vector3(toMove.position.x,toMove.position.y,-2);

        // Time when the movement started.
        float startTime = Time.time;
        float lerpTime = 2;

        bool notdone = true;

        while (notdone)
        {
            // Distance moved equals elapsed time times speed..
            float timeSinceStarted = Time.time - startTime;
            // Fraction of journey completed equals current distance divided by total distance.
            float precentageComplete = timeSinceStarted / lerpTime;

            // Set our position as a fraction of the distance between the markers.
            toMove.position = Vector3.Lerp(toMove.position, endMarker.position, precentageComplete);

            yield return null;
            if (currentTeam == EnumTeams.Red)
            {
                if (toMove.position.x >= endMarker.position.x)
                {
                    notdone = false;
                }
            }
            else
            {
                if (toMove.position.x <= endMarker.position.x)
                {
                    notdone = false;
                }
            }
        }

        toMove.position = endMarker.position;

        yield return null;
    }


    /// <summary>
    /// Destroys the game object and unlinks the character from this space this way fully deleting the character
    /// </summary>
    /// <param name="cToRemove">character to remove</param>
    /// <param name="goToRemove">character game object to destroy</param>
    public void DestroyCharacterFromSpace(Character cToRemove, GameObject goToRemove)
    {
        GameObject.Destroy(goToRemove);

        switch (cToRemove.TeamColor)
        {
            case EnumTeams.Red:
                this.redCharacterGameObject = null;
                this.redCharacterOnTile = null;
                break;
            case EnumTeams.Blue:
                this.blueCharacterGameObject = null;
                this.blueCharacterOnTile = null;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Unlink the character from this space
    /// </summary>
    /// <param name="cToRemove">Character to unlink</param>
    public void RemoveCharacterFromSpace(Character cToRemove)
    {
        switch (cToRemove.TeamColor)
        {
            case EnumTeams.Red:
                this.redCharacterGameObject = null;
                this.redCharacterOnTile = null;
                break;
            case EnumTeams.Blue:
                this.blueCharacterGameObject = null;
                this.blueCharacterOnTile = null;
                break;
            default:
                break;
        }
    }

    public GameObject GetTileFromDirection(EnumDirection direction, EnumTeams teamToMove)
    {
        switch (teamToMove)
        {
            case EnumTeams.Red:
                switch (direction)
                {
                    case EnumDirection.UP:
                        return TileTop;
                    case EnumDirection.DOWN:
                        return tileBottom;
                    case EnumDirection.LEFT:
                        return tileLeft;
                    case EnumDirection.RIGHT:
                        return tileRight;
                    default:
                        return null;
                }
            case EnumTeams.Blue:
                switch (direction)
                {
                    case EnumDirection.UP:
                        return tileBottom;
                    case EnumDirection.DOWN:
                        return tileTop;
                    case EnumDirection.LEFT:
                        return tileRight;
                    case EnumDirection.RIGHT:
                        return tileLeft;
                    default:
                        return null;
                }
            default:
                return null;
        }
        
    }

    /// <summary>
    /// get the drawable transform from tile according to given team
    /// </summary>
    /// <param name="team">Given team</param>
    /// <returns>Transform according to given team</returns>
    public Transform GetPlacement(EnumTeams team)
    {
        switch (team)
        {
            case EnumTeams.Red:
                return redTeamPlacement;
            case EnumTeams.Blue:
                return blueTeamPlacement;
            default:
                return null;
        }
    }
    /// <summary>
    /// Get the Character GameObject from tile according to given team
    /// </summary>
    /// <param name="team">Given team</param>
    /// <returns>Transform according to given team</returns>
    public GameObject GetCharacterObject(EnumTeams team)
    {
        switch (team)
        {
            case EnumTeams.Red:
                return redCharacterGameObject;
            case EnumTeams.Blue:
                return blueCharacterGameObject;
            default:
                return null;
        }
    }
    /// <summary>
    /// Get the Character object from tile according to given team
    /// </summary>
    /// <param name="team">Given team</param>
    /// <returns>Character object from tile according to given team</returns>
    public Character GetCharacter(EnumTeams team)
    {
        switch (team)
        {
            case EnumTeams.Red:
                return redCharacterOnTile;
            case EnumTeams.Blue:
                return blueCharacterOnTile;
            default:
                return null;
        }
    }

    public SmartTile GetSmartTileFromDirection(EnumDirection direction,EnumTeams currentTeam)
    {
        GameObject go = null;
        switch (currentTeam)
        {
            case EnumTeams.Red:
                switch (direction)
                {
                    case EnumDirection.UP:
                        go = tileTop;
                        break;
                    case EnumDirection.DOWN:
                        go = tileBottom;
                        break;
                    case EnumDirection.LEFT:
                        go = tileLeft;
                        break;
                    case EnumDirection.RIGHT:
                        go = TileRight;
                        break;
                    default:
                        return null;
                }
                break;
            case EnumTeams.Blue:
                switch (direction)
                {
                    case EnumDirection.UP:
                        go = TileBottom;
                        break;
                    case EnumDirection.DOWN:
                        go = TileTop;
                        break;
                    case EnumDirection.LEFT:
                        go = TileRight;
                        break;
                    case EnumDirection.RIGHT:
                        go = tileLeft;
                        break;
                    default:
                        return null;
                }
                break;
            default:
                break;
        }
        
        if (go != null)
        {
            return go.GetComponent<SmartTile>();
        }

        return null;
    }

    public int PositionNumberX { get => positionNumberX; set => positionNumberX = value; }
    public int PositionNumberY { get => positionNumberY; set => positionNumberY = value; }
    public GameObject TileLeft { get => tileLeft; set => tileLeft = value; }
    public GameObject TileRight { get => tileRight; set => tileRight = value; }
    public GameObject TileBottom { get => tileBottom; set => tileBottom = value; }
    public GameObject TileTop { get => tileTop; set => tileTop = value; }
    public Transform RedTeamPlacement { get => redTeamPlacement; set => redTeamPlacement = value; }
    public Transform BlueTeamPlacement { get => blueTeamPlacement; set => blueTeamPlacement = value; }
    public Character BlueCharacterOnTile { get => blueCharacterOnTile; set => blueCharacterOnTile = value; }
    public Character RedCharacterOnTile { get => redCharacterOnTile; set => redCharacterOnTile = value; }
    public GameObject RedCharacterGameObject { get; internal set; }
    public GameObject BlueCharacterGameObject { get; internal set; }
    public GameObject ThisTileObject { get => thisTileObject; set => thisTileObject = value; }
}
