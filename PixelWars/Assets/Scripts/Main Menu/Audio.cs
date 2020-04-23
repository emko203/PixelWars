using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private static Audio instance = null;

    public static Audio Instance 
    {
        get{ return instance; }
    }

    void Awake() 
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
