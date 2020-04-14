using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldHandler : MonoBehaviour
{
    BattlePostion[,] map;
    public int iSizeX;
    public int iSizeY;

    public float stepSizeX;
    public float stepSizeY;

    public GameObject tilePrefab;
    public Canvas canvas;

    BattleFieldInstantiate battleFieldInstantiater;

    

    public void Awake()
    {
        SpriteRenderer s = tilePrefab.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        stepSizeX = s.sprite.bounds.size.x;
        stepSizeY = s.sprite.bounds.size.y;

        //set an instantiater with current value
        battleFieldInstantiater = new BattleFieldInstantiate(iSizeX, iSizeY, stepSizeX, stepSizeY, tilePrefab);
    }

    public void Start()
    {
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
