using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> blueTeam;
    [SerializeField] private List<SpriteRenderer> redTeam;

    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    [SerializeField] private GameObject characterSelector;
    [SerializeField] private GameObject LineSelector;

    private int SelectorPosInCharacterSelect = 0;
    private int SelectorPosInLaneSelect = 0;

    private float zSpacing = -1;

    private List<GameObject> lstCurrentSelectedCharacters = new List<GameObject>();

    private List<int> lstLastRandoms = new List<int>();

    private void ResetSelector(EnumTeams currentPlayerTurn)
    {
        SelectorPosInCharacterSelect = 0;
        SelectorPosInLaneSelect = 0;

        UpdateCharacterSelectorPos(currentPlayerTurn);
    }

    /// <summary>
    /// Selects the current unit and return it so we can spawn him
    /// </summary>
    /// <returns>enumUnit</returns>
    public enumUnit SelectCharacter(EnumTeams currentPlayerTurn, BattlefieldHandler battlefieldHandler)
    {
        //TODO: Show LaneSelector

        //TODO: update character SelectorColor
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

    private Vector3 UpdateOffsetSelectorPositioning(Vector3 pos)
    {
        pos.z = zSpacing;
        pos.x += xOffset;
        pos.y += yOffset;
        return pos;
    }

    private void UpdateCharacterSelectorPos(EnumTeams currentPlayerTurn)
    {
        Vector3 pos = GetSelectorTeam(currentPlayerTurn)[SelectorPosInCharacterSelect].transform.position;
        characterSelector.transform.position = UpdateOffsetSelectorPositioning(pos);
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
        SpawnNewPlayerhand(currentPlayerTurn, charactersToPickFrom);
        ResetSelector(currentPlayerTurn);
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
        int randomInt = Random.Range(0, charactersToPickFrom.Count);
        bool AlreadyUsed = false;
        bool FirstTry = true;

        while (AlreadyUsed || FirstTry)
        {
            if (AlreadyUsed)
            {
                AlreadyUsed = false;
            }

            randomInt = Random.Range(0, charactersToPickFrom.Count);

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
