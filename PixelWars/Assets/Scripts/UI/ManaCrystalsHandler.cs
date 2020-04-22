using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCrystalsHandler : MonoBehaviour
{
    [Space]
    [Header("ManaCrystals")]
    [SerializeField] private List<GameObject> blueCrystalsEmpty = new List<GameObject>();
    [SerializeField] private List<GameObject> blueCrystalsFull = new List<GameObject>();
    [SerializeField] private List<GameObject> redCrystalsFull = new List<GameObject>();
    [SerializeField] private List<GameObject> redCrystalsEmpty = new List<GameObject>();

    public void UpdateManaSprites(EnumTeams currentTeam, Player currentPlayer)
    {
        currentPlayer.UpdateManaSprites(GetCrystalsSpritesFromTeam(false, currentTeam), GetCrystalsSpritesFromTeam(true, currentTeam));
    }

    private List<GameObject> GetCrystalsSpritesFromTeam(bool emptyCrystals, EnumTeams currentTeam)
    {
        if (emptyCrystals)
        {
            return HandleEmptyCrystals(currentTeam);
        }
        else
        {
            return HandleFullCrystalls(currentTeam);
        }
    }

    private List<GameObject> HandleFullCrystalls(EnumTeams currentTeam)
    {
        switch (currentTeam)
        {
            case EnumTeams.Red:
                return redCrystalsFull;
            case EnumTeams.Blue:
                return blueCrystalsFull;
            default:
                return null;
        }
    }

    private List<GameObject> HandleEmptyCrystals(EnumTeams currentTeam)
    {
        switch (currentTeam)
        {
            case EnumTeams.Red:
                return redCrystalsEmpty;
            case EnumTeams.Blue:
                return blueCrystalsEmpty;
            default:
                return null;
        }
    }
}
