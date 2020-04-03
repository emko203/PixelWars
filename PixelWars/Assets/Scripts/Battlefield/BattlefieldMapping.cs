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
    }

    private void SetStartingPlacements()
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

    private void UpdateYPos(int yAmount)
    {
        yAmount += 1;

        whereToPlaceRedY = (steppingY * yAmount) / 2;
        whereToPlaceBlueY = (steppingY * yAmount) / 2;
    }

    private void UpdateXPos(int xAmount)
    {
        whereToPlaceRedX = (steppingX * xAmount) / 4;
        whereToPlaceBlueX = (steppingX * xAmount) / 4 * 3;
    }
}
