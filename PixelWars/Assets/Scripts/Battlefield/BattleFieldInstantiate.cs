using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldInstantiate : MonoBehaviour
{
    private float startPointX;
    private float startPointY;

    List<GameObject> Map = new List<GameObject>();

    public int iSizeX;
    public int iSizeY;

    public float stepSizeX;
    public float stepSizeY;

    public GameObject tilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //SetStartPoint();

        startPointX = 0;
        startPointY = 0;

        createMap();
    }

    void createMap()
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
    /*void SetStartPoint()
    {
        startPointX = iSizeX * stepSizeX / 2;
        startPointX = startPointX - 2 * startPointX;

        startPointY = iSizeY * stepSizeY / 2;
        startPointY = startPointY - 2 * startPointY;
    }*/

    void SpawnTile(float x, float y)
    {
        GameObject tileToSpawn = tilePrefab;
        tileToSpawn.transform.position = new Vector3(x,y);

        Map.Add(tileToSpawn);

        Instantiate(tileToSpawn);
    }
}
