using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> BlueTeam;
    [SerializeField] private List<SpriteRenderer> RedTeam;

    private List<int> LastRandoms = new List<int>();

    public void SpawnSelectableCharacterSet(EnumTeams currentPlayerTurn, List<GameObject> charactersToPickFrom)
    {
        List<Sprite> spriteList = GetSpritesFromGameObjects(charactersToPickFrom);

        switch (currentPlayerTurn)
        {
            case EnumTeams.Red:
                RenderRandomsForTeam(BlueTeam, spriteList);
                break;
            case EnumTeams.Blue:
                RenderRandomsForTeam(RedTeam, spriteList);
                break;
            default:
                break;
        }
        
    }

    public List<Sprite> GetSpritesFromGameObjects(List<GameObject> lstObjects)
    {
        List<Sprite> toreturn = new List<Sprite>();

        foreach (GameObject gameObject in lstObjects)
        {
            toreturn.Add(gameObject.GetComponent<SpriteRenderer>().sprite);
        }

        return toreturn;
    }

    private void RenderRandomsForTeam(List<SpriteRenderer> listToFill,List<Sprite> charactersToPickFrom)
    {
        foreach (SpriteRenderer renderer in listToFill)
        {
            renderer.sprite = GetRandomCharacterSprite(charactersToPickFrom);
        }

        LastRandoms.Clear();
    }

    private Sprite GetRandomCharacterSprite(List<Sprite> charactersToPickFrom)
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

            foreach (int i in LastRandoms)
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

        LastRandoms.Add(randomInt);

        return charactersToPickFrom[randomInt];
    }
}
