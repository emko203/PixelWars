using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> blueTeam;
    [SerializeField] private List<SpriteRenderer> redTeam;

    [SerializeField] private float xCharacterOffset;
    [SerializeField] private float yCharacterOffset;

    [SerializeField] private float xLaneOffset;
    [SerializeField] private float yLaneOffset;

    [SerializeField] private GameObject characterSelector;
    [SerializeField] private GameObject LaneSelector;

    private int SelectorPosInCharacterSelect = 0;
    private EnumLane CurrentSelectedLane = EnumLane.LEFT_OUTER_LANE;

    private float zSpacing = -1;

    private List<GameObject> lstCurrentSelectedCharacters = new List<GameObject>();

    private List<int> lstLastRandoms = new List<int>();

    private void ResetSelectors(EnumTeams currentPlayerTurn)
    {
        SelectorPosInCharacterSelect = 0;
        CurrentSelectedLane = GetMinLaneAmount();

        UpdateCharacterSelectorPos(currentPlayerTurn);
    }

    /// <summary>
    /// Selects the current unit and return it so we can spawn him
    /// </summary>
    /// <returns>enumUnit</returns>
    public EnumUnit SelectCharacter(EnumTeams currentPlayerTurn, BattlefieldHandler battlefieldHandler)
    {
        ShowLaneSelector();
        UpdateLaneSelectorPos(currentPlayerTurn,battlefieldHandler);
        return lstCurrentSelectedCharacters[SelectorPosInCharacterSelect].GetComponent<Character>().UnitType;
    }

    private List<SpriteRenderer> GetSelectorTeam(EnumTeams currentPlayerTurn)
    {
        switch (currentPlayerTurn)
        {
            case EnumTeams.Red:
                return redTeam;

            case EnumTeams.Blue:
                return blueTeam;

            default:
                return null;
        }
    }

    private Vector3 UpdateOffsetLaneSelectorPositioning(Vector3 pos, EnumTeams team)
    {
        pos.z = zSpacing;
        switch (team)
        {
            case EnumTeams.Red:
                pos.x += xLaneOffset;
                pos.y += yLaneOffset;
                break;
            case EnumTeams.Blue:
                pos.x -= xLaneOffset;
                pos.y -= yLaneOffset;
                break;
            default:
                break;
        }
        
        return pos;
    }

    private Vector3 UpdateOffsetCharacterSelectorPositioning(Vector3 pos)
    {
        pos.z = zSpacing;
        pos.x += xCharacterOffset;
        pos.y += yCharacterOffset;
        return pos;
    }

    public EnumLane GetSelectedLane()
    {
        return CurrentSelectedLane;
    }

    private EnumLane GetMaxLaneAmount()
    {
        return EnumLane.RIGHT_OUTER_LANE;
    }

    private EnumLane GetNextLane()
    {
        switch (CurrentSelectedLane)
        {
            case EnumLane.LEFT_OUTER_LANE:
                return EnumLane.RIGHT_OUTER_LANE;

            case EnumLane.LEFT_CENTER_LANE:
                return EnumLane.LEFT_OUTER_LANE;

            case EnumLane.RIGHT_OUTER_LANE:
                return EnumLane.RIGHT_CENTER_LANE;

            case EnumLane.RIGHT_CENTER_LANE:
                return EnumLane.LEFT_CENTER_LANE;
            default:
                return EnumLane.LEFT_OUTER_LANE;
        }
    }

    private EnumLane GetPreviousLane()
    {
        switch (CurrentSelectedLane)
        {
            case EnumLane.LEFT_OUTER_LANE:
                return EnumLane.LEFT_CENTER_LANE;

            case EnumLane.LEFT_CENTER_LANE:
                return EnumLane.RIGHT_CENTER_LANE;

            case EnumLane.RIGHT_OUTER_LANE:
                return EnumLane.LEFT_OUTER_LANE;

            case EnumLane.RIGHT_CENTER_LANE:
                return EnumLane.RIGHT_OUTER_LANE;
            default:
                return EnumLane.LEFT_OUTER_LANE;
        }
    }

    private EnumLane GetMinLaneAmount()
    {
        return EnumLane.LEFT_OUTER_LANE;
    }

    private void UpdateLaneSelectorPos(EnumTeams currentPlayerTurn, BattlefieldHandler battlefieldHandler)
    {
        Vector3 pos = battlefieldHandler.GetSpawnFromLane(CurrentSelectedLane, currentPlayerTurn);
        LaneSelector.transform.position = UpdateOffsetLaneSelectorPositioning(pos,currentPlayerTurn);
    }

    private void UpdateCharacterSelectorPos(EnumTeams currentPlayerTurn)
    {
        Vector3 pos = GetSelectorTeam(currentPlayerTurn)[SelectorPosInCharacterSelect].transform.position;
        characterSelector.transform.position = UpdateOffsetCharacterSelectorPositioning(pos);
    }

    public void HideCharacterSelector()
    {
        characterSelector.SetActive(false);
    }

    public void ShowCharacterSelector()
    {
        characterSelector.SetActive(true);
    }

    public void HideLaneSelector()
    {
        LaneSelector.SetActive(false);
    }

    public void ShowLaneSelector()
    {
        LaneSelector.SetActive(true);
    }

    /// <summary>
    /// Moves the characterselector sprite up one space
    /// </summary>
    /// <param name="currentPlayerTurn">Current turns player</param>
    public void MoveLaneSelectorSpriteUp(EnumTeams currentPlayerTurn, BattlefieldHandler battlefieldHandler)
    {
        CurrentSelectedLane = GetNextLane();

        UpdateLaneSelectorPos(currentPlayerTurn, battlefieldHandler);
    }
    /// <summary>
    /// Moves the characterselector sprite down one space
    /// </summary>
    /// <param name="currentPlayerTurn">Current turns player</param>
    public void MoveLaneSelectorSpriteDown(EnumTeams currentPlayerTurn, BattlefieldHandler battlefieldHandler)
    {
        CurrentSelectedLane = GetPreviousLane();
        UpdateLaneSelectorPos(currentPlayerTurn,battlefieldHandler);
    }

    /// <summary>
    /// Moves the characterselector sprite up one space
    /// </summary>
    /// <param name="currentPlayerTurn">Current turns player</param>
    public void MoveCharacterSelectorSpriteUp(EnumTeams currentPlayerTurn)
    {
        if (SelectorPosInCharacterSelect > 0)
        {
            SelectorPosInCharacterSelect -= 1;
        }
        else
        {
            SelectorPosInCharacterSelect = GetSelectorTeam(currentPlayerTurn).Count - 1;
        }
        UpdateCharacterSelectorPos(currentPlayerTurn);
    }
    /// <summary>
    /// Moves the characterselector sprite down one space
    /// </summary>
    /// <param name="currentPlayerTurn">Current turns player</param>
    public void MoveCharacterSelectorSpriteDown(EnumTeams currentPlayerTurn)
    {
        if (SelectorPosInCharacterSelect < GetSelectorTeam(currentPlayerTurn).Count - 1)
        {
            SelectorPosInCharacterSelect += 1;
        }
        else
        {
            SelectorPosInCharacterSelect = 0;
        }
        UpdateCharacterSelectorPos(currentPlayerTurn);
    }

    public void SetupNewRound(EnumTeams currentPlayerTurn, List<GameObject> charactersToPickFrom)
    {
        ShowCharacterSelector();
        SpawnNewPlayerhand(currentPlayerTurn, charactersToPickFrom);
        ResetSelectors(currentPlayerTurn);
    }

    private List<GameObject> SpawnNewPlayerhand(EnumTeams currentPlayerTurn, List<GameObject> charactersToPickFrom)
    {
        switch (currentPlayerTurn)
        {
            case EnumTeams.Red:
                RenderRandomsForTeam(redTeam, charactersToPickFrom);
                break;
            case EnumTeams.Blue:
                RenderRandomsForTeam(blueTeam, charactersToPickFrom);
                break;
            default:
                break;
        }

        return lstCurrentSelectedCharacters;
    }

    private void RemoveSprites(EnumTeams currentTeam)
    {
        List<SpriteRenderer> ListToEmpty;

        switch (currentTeam)
        {
            case EnumTeams.Red:
                ListToEmpty = redTeam;
                break;
            case EnumTeams.Blue:
                ListToEmpty = blueTeam;
                break;
            default:
                ListToEmpty = null;
                break;
        }


        if (ListToEmpty != null)
        {
            foreach (SpriteRenderer renderer in ListToEmpty)
            {
                renderer.sprite = null;
            }
        }
    }

    private void RenderRandomsForTeam(List<SpriteRenderer> listToFill, List<GameObject> charactersToPickFrom)
    {
        lstCurrentSelectedCharacters = new List<GameObject>();

        foreach (SpriteRenderer renderer in listToFill)
        {
            GameObject tempOb = GetRandomUniqueCharacter(charactersToPickFrom);
            renderer.sprite = tempOb.GetComponent<SpriteRenderer>().sprite;
            lstCurrentSelectedCharacters.Add(tempOb);
        }

        lstLastRandoms.Clear();
    }

    private GameObject GetRandomUniqueCharacter(List<GameObject> charactersToPickFrom)
    {
        int randomInt = UnityEngine.Random.Range(0, charactersToPickFrom.Count);
        bool AlreadyUsed = false;
        bool FirstTry = true;

        while (AlreadyUsed || FirstTry)
        {
            if (AlreadyUsed)
            {
                AlreadyUsed = false;
            }

            randomInt = UnityEngine.Random.Range(0, charactersToPickFrom.Count);

            foreach (int i in lstLastRandoms)
            {
                if (i == randomInt)
                {
                    AlreadyUsed = true;
                    break;
                }
            }

            if (FirstTry)
            {
                FirstTry = false;
            }
        }

        lstLastRandoms.Add(randomInt);

        return charactersToPickFrom[randomInt];
    }
}
