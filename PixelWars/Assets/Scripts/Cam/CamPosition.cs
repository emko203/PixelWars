using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    public Transform camTrans;
    public BattleFieldInstantiate instant;

    float startPointX = 0;
    float startPointY = 0;
    float startpointZ = -10;

    // Start is called before the first frame update
    void Start()
    {
        startPointX = instant.iSizeX * instant.stepSizeX / 2;
        // startPointX += instant.stepSizeX / 2; 

        startPointY = instant.iSizeY * instant.stepSizeY / 2;

        //startPointY += instant.stepSizeY / 2;

        camTrans.position = new Vector3(startPointX, startPointY, startpointZ);
    }
}
