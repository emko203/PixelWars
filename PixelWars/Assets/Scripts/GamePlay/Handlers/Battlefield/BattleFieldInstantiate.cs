using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldInstantiate
{
    private BattlePostion[,] Mapping; 

    private float startPointX = 0;
    private float startPointY = 0;
    private float steppingX;
    private float steppingY;
    private float whereToPlaceRedX;
    private float whereToPlaceRedY;
    private float whereToPlaceBlueX;
    private float whereToPlaceBlueY;

    private readonly List<GameObject> tilesToDraw = new List<GameObject>();

    private int iSizeX;
    private int iSizeY;

    private float stepSizeX;
    private float stepSizeY;

    private GameObject tilePrefab;

    public BattleFieldInstantiate(int iSizeX, int iSizeY, float stepSizeX, float stepSizeY, GameObject tilePrefab)
    {
        this.iSizeX = iSizeX;
        this.iSizeY = iSizeY;
        this.stepSizeX = stepSizeX;
        this.stepSizeY = stepSizeY;
        this.tilePrefab = tilePrefab;
    }
    #region Publics
    public BattlePostion[,] FillMapping()
    {
        //set size of tiles
        steppingX = stepSizeX;
        steppingY = stepSizeY;

        //set size of mapping
        Mapping = new BattlePostion[iSizeX, iSizeY];

        SetStartingPlacements();
        SetInitialMapping();
        ConnectTiles();
        CreateDrawableMapping();

        return Mapping;
    }

    public List<GameObject> GetTileToDraw()
    {
        return tilesToDraw;
    }
    #endregion

    #region Privates
    /// <summary>
    /// Where to place a teams character on a space
    /// </summary>
    private void SetStartingPlacements()
    {
        whereToPlaceRedX = steppingX / 4;
        whereToPlaceBlueX = steppingX / 4 * 3;

        whereToPlaceRedY = steppingY / 2;
        whereToPlaceBlueY = steppingY / 2;
    }

    private void SetInitialMapping()
    {
        for (int x = 0; x < iSizeX; x++)
        {
            for (int y = 0; y < iSizeY; y++)
            {
                //Step vector y
                BattlePostion tempPos = new BattlePostion(new Vector3(whereToPlaceBlueX, whereToPlaceBlueY),
                                                          new Vector3(whereToPlaceRedX, whereToPlaceRedY),
                                                          x, y);

                Mapping[tempPos.LocationX, tempPos.LocationY] = tempPos;

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

    private void CreateDrawableMapping()
    {
        float xpos = startPointX;
        float ypos = startPointY;

        for (int X = 0; X < iSizeX; X++)
        {
            for (int Y = 0; Y < iSizeY; Y++)
            {
                SpawnTile(xpos, ypos);
                ypos += stepSizeY;
            }
            xpos += stepSizeX;
            ypos = startPointY;
        }
    }


    /// <summary>
    /// Make connections to FRONT,LEFT,RIGHT and BEHIND
    /// </summary>
    private BattlePostion[,] ConnectTiles()
    {
        for (int x = 0; x < iSizeX; x++)
        {
            for (int y = 0; y < iSizeY; y++)
            {
                //set tile in FRONT
                if (y + 1 < iSizeY)
                {
                    Mapping[x, y].TileFront = Mapping[x, y + 1];
                }
                //set tile in BEHIND
                if (y - 1 >= 0)
                {
                    Mapping[x, y].TileBehind = Mapping[x, y - 1];
                }
                //set tile in LEFT
                if (x - 1 >= 0)
                {
                    Mapping[x, y].TileLeft = Mapping[x - 1, y];
                }
                //set tile in RIGHT
                if (x + 1 < iSizeX)
                {
                    Mapping[x, y].TileRight = Mapping[x + 1, y];
                }
            }
        }

        return Mapping;
    }

    private void SpawnTile(float x, float y)
    {
        GameObject tileToSpawn = tilePrefab;
        tileToSpawn.transform.position = new Vector3(x,y);

        tilesToDraw.Add(tileToSpawn);

        GameObject.Instantiate(tileToSpawn);
    }
    #endregion
}
