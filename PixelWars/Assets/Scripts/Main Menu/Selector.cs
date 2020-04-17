using System.Collections;
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

    public static List<GameObject> SelectedCharacters = new List<GameObject>();

    public static List<EnumUnit> listEnum = new List<EnumUnit>();


    
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

        if (SelectedCharacters.Count < 2)
        {            
            SelectedCharacters.Add(SelectedObject);
            listEnum.Add(SelectedObject.GetComponent<Character>().UnitType);
            Debug.Log("SelectedObject: " + SelectedObject);
        }
        else 
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
       
    }

    private void SavePlayerPrefs()    
    {
        PlayerPrefs.SetInt("SelectionSize", listEnum.Count);
        for (int i = 0; i < listEnum.Count; i++)
        {
            PlayerPrefs.SetInt("Selection_" + i, (int)listEnum[i]);
        }

        
    }

    public void AddCharacterP2()
    {

        if (SelectedCharacters.Count < 5)
        {
            SelectedCharacters.Add(SelectedObject);
            Debug.Log("SelectedObject: " + SelectedObject);
        }
        else
        {
            SavePlayerPrefs();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RemoveCharacter() 
    {
        if (SelectedCharacters.Count >= 1)
        {
            SelectedCharacters.RemoveAt(SelectedCharacters.Count - 1);
            listEnum.RemoveAt(listEnum.Count - 1);
            Debug.Log("RemovedObject:" + SelectedObject);
        }
        else 
        {
            Debug.Log("Character List is Empty");
        }
    }

    public void PrintListConsole() 
    {
        foreach (object o in SelectedCharacters) 
        {
            Debug.Log(o);
        }
    }

    public void DebugLogLog() 
    {
        for (int i = 0; i < SelectedCharacters.Count; i++)
        {
            Debug.Log(SelectedCharacters[i]);
        }
        Debug.Log("Count " + SelectedCharacters.Count);
    }
}
