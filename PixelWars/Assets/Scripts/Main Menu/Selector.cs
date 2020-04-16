﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Selector : MonoBehaviour
{
    public GameObject Archer;
    public GameObject Knight;
    public GameObject Mage;
    public GameObject Rogue;
    public GameObject SelectedObject;

    public static List<GameObject> SelectedCharactersPlayer1 = new List<GameObject>();
    public static List<GameObject> SelectedCharactersPlayer2 = new List<GameObject>();
    
    private Vector3 CharacterPosition;
    private Vector3 OffScreenPosition;

    private int CharacterInt = 1;

    private SpriteRenderer ArcherRender, KnightRender, MageRender, RogueRender;
    
    private void Awake() 
    {
        CharacterPosition = Archer.transform.position;
        OffScreenPosition = Knight.transform.position;


        ArcherRender = Archer.GetComponent<SpriteRenderer>();
        KnightRender = Knight.GetComponent<SpriteRenderer>();
        MageRender = Mage.GetComponent<SpriteRenderer>();
        RogueRender = Rogue.GetComponent<SpriteRenderer>();

        SelectedObject = Archer;

    }

    public void NextCharacter() 
    {
        switch (CharacterInt)
        {
            case 1:
                ArcherRender.enabled = false;
                Archer.transform.position = OffScreenPosition;
                Knight.transform.position = CharacterPosition;
                KnightRender.enabled = true;
                SelectedObject = Knight;
                CharacterInt++;
                break;
            case 2:

                KnightRender.enabled = false;
                Knight.transform.position = OffScreenPosition;
                Mage.transform.position = CharacterPosition;
                MageRender.enabled = true;
                SelectedObject = Mage;
                CharacterInt++;
                break;
            case 3:
                MageRender.enabled = false;
                Mage.transform.position = OffScreenPosition;
                Rogue.transform.position = CharacterPosition;
                RogueRender.enabled = true;
                SelectedObject = Rogue;
                CharacterInt++;
                break;
            case 4:
                RogueRender.enabled = false;
                Rogue.transform.position = OffScreenPosition;
                Archer.transform.position = CharacterPosition;
                ArcherRender.enabled = true;
                SelectedObject = Archer;
                ResetInt();
                break;
            default:
                ResetInt();
                break;
        }
    }

    private void ResetInt() 
    {
        if (CharacterInt >= 4)
        {
            CharacterInt = 1;
        }
        else 
        {
            CharacterInt = 4;
        }
    }

    public void PreviousCharacter() 
    {
        switch (CharacterInt)
        {
            case 1:
                ArcherRender.enabled = false;
                Archer.transform.position = OffScreenPosition;
                Rogue.transform.position = CharacterPosition;
                RogueRender.enabled = true;
                ResetInt();
                break;
            case 2:
                KnightRender.enabled = false;
                Knight.transform.position = OffScreenPosition;
                Archer.transform.position = CharacterPosition;
                ArcherRender.enabled = true;
                CharacterInt--;
                break;
            case 3:
                MageRender.enabled = false;
                Mage.transform.position = OffScreenPosition;
                Knight.transform.position = CharacterPosition;
                KnightRender.enabled = true;
                CharacterInt--;
                break;
            case 4:
                RogueRender.enabled = false;
                Rogue.transform.position = OffScreenPosition;
                Mage.transform.position = CharacterPosition;
                MageRender.enabled = true;
                CharacterInt--;
                break;
            default:
                ResetInt();
                break;
        }
    }

    public void Back() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void AddCharacter() 
    {

        if (SelectedCharactersPlayer1.Count < 2)
        {            
            SelectedCharactersPlayer1.Add(SelectedObject);
            Debug.Log("SelectedObject: " + SelectedObject);
        }
        else 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void DebugLogLog() 
    {
        for (int i = 0; i < SelectedCharactersPlayer1.Count; i++)
        {
            Debug.Log(SelectedCharactersPlayer1[i]);
        }
        Debug.Log("Count " + SelectedCharactersPlayer1.Count);
    }
}
