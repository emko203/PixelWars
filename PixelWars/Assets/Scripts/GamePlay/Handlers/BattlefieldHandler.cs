using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldHandler : MonoBehaviour
{
    public int iSizeX;
    public int iSizeY;

    public float stepSizeX;
    public float stepSizeY;

    public GameObject tilePrefab;

    BattleFieldInstantiate battleFieldInstantiater;

    BattlePostion[,] map;

    public void Start()
    {
        //set an instantiater with current value
        battleFieldInstantiater = new BattleFieldInstantiate(iSizeX, iSizeY, stepSizeX, stepSizeY, tilePrefab);

        //fill map with logic
        map = battleFieldInstantiater.FillMapping();

        //After creating the map draw it on screen
        DrawMap();
    }

    private void DrawMap()
    {
        List<GameObject> drawables = battleFieldInstantiater.GetTileToDraw();

        foreach (GameObject ob in drawables)
        {
            Instantiate(ob);
        }
    }
}
