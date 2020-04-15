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
                CharacterInt++;
                break;
            case 2:

                KnightRender.enabled = false;
                Knight.transform.position = OffScreenPosition;
                Mage.transform.position = CharacterPosition;
                MageRender.enabled = true;
                CharacterInt++;
                break;
            case 3:
                MageRender.enabled = false;
                Mage.transform.position = OffScreenPosition;
                Rogue.transform.position = CharacterPosition;
                RogueRender.enabled = true;
                CharacterInt++;
                break;
            case 4:
                RogueRender.enabled = false;
                Rogue.transform.position = OffScreenPosition;
                Archer.transform.position = CharacterPosition;
                ArcherRender.enabled = true;
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
}
