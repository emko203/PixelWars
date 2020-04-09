using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldMapping : MonoBehaviour
{
    public BattleFieldInstantiate instantiate;

    float steppingX;
    float steppingY;

    float whereToPlaceRedX;
    float whereToPlaceRedY;

    float whereToPlaceBlueX;
    float whereToPlaceBlueY;

    BattlePostion[,] map;

    // Start is called before the first frame update
    void Start()
    {
        steppingX = instantiate.stepSizeX;
        steppingY = instantiate.stepSizeY;
        map = new BattlePostion[instantiate.iSizeX, instantiate.iSizeY];

        SetStartingPlacements();

        SetInitialMapping();

        ConnectTiles();
    }

    /// <summary>
    /// Where to place a teams character on a space
    /// </summary>
    void SetStartingPlacements()
    {
        whereToPlaceRedX = steppingX / 4;
        whereToPlaceBlueX = steppingX / 4 * 3;

        whereToPlaceRedY = steppingY / 2;
        whereToPlaceBlueY = steppingY / 2;
    }

    void SetInitialMapping()
    {
        for (int x = 0; x < instantiate.iSizeX; x++)
        {
            for (int y = 0; y < instantiate.iSizeY; y++)
            {
                //Step vector y
                BattlePostion tempPos = new BattlePostion(new Vector3(whereToPlaceBlueX, whereToPlaceBlueY),
                                                          new Vector3(whereToPlaceRedX, whereToPlaceRedY),
                                                          x, y);

                map[tempPos.LocationX, tempPos.LocationY] = tempPos;

                UpdateYPos(y);
            }

            UpdateXPos(x);
        }
    }
    /// <summary>
    /// Make connections to FRONT,LEFT,RIGHT and BEHIND
    /// </summary>
    void ConnectTiles()
    {
        for (int x = 0; x < instantiate.iSizeX; x++)
        {
            for (int y = 0; y < instantiate.iSizeY; y++)
            {
                //set tile in FRONT
                if (y + 1 < instantiate.iSizeY)
                {
                    map[x, y].TileFront = map[x, y + 1];
                }
                //set tile in BEHIND
                if (y - 1 >= 0)
                {
                    map[x, y].TileBehind = map[x, y - 1];
                }
                //set tile in LEFT
                if (x - 1 >= 0)
                {
                    map[x, y].TileLeft = map[x - 1, y];
                }
                //set tile in RIGHT
                if (x + 1 < instantiate.iSizeX)
                {
                    map[x, y].TileRight = map[x + 1, y + 1];
                }
            }
        }
    }

    void UpdateYPos(int yAmount)
    {
        yAmount += 1;

        whereToPlaceRedY = (steppingY * yAmount) / 2;
        whereToPlaceBlueY = (steppingY * yAmount) / 2;
    }

    void UpdateXPos(int xAmount)
    {
        whereToPlaceRedX = (steppingX * xAmount) / 4;
        whereToPlaceBlueX = (steppingX * xAmount) / 4 * 3;
    }
}
