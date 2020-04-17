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

    public Dropdown CharacterDropdown;

    public GameObject ConfirmBt;
    public GameObject AddBt;
    public GameObject RemoveBt;

    public Text CharacterCount;

    public static List<GameObject> SelectedCharacters = new List<GameObject>();

    public static List<EnumUnit> listEnum = new List<EnumUnit>();


    public static List<string> DropdownOptions = new List<string>();

    private Vector3 CharacterPosition;
    private Vector3 OffScreenPosition;

    private int CharacterInt = 1;

    private SpriteRenderer ArcherRender, KnightRender, MageRender, RogueRender;

    private void Awake()
    {
        CharacterPosition = Archer.transform.position;
        OffScreenPosition = Knight.transform.position;

        ConfirmBt.GetComponent<Button>();
        AddBt.GetComponent<Button>();
        RemoveBt.GetComponent<Button>();

        CharacterCount.GetComponent<Text>();

        ArcherRender = Archer.GetComponent<SpriteRenderer>();
        KnightRender = Knight.GetComponent<SpriteRenderer>();
        MageRender = Mage.GetComponent<SpriteRenderer>();
        RogueRender = Rogue.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CharacterDropdown.ClearOptions();
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
                SelectedObject = Rogue;
                ResetInt();
                break;
            case 2:
                KnightRender.enabled = false;
                Knight.transform.position = OffScreenPosition;
                Archer.transform.position = CharacterPosition;
                ArcherRender.enabled = true;
                SelectedObject = Archer;
                CharacterInt--;
                break;
            case 3:
                MageRender.enabled = false;
                Mage.transform.position = OffScreenPosition;
                Knight.transform.position = CharacterPosition;
                KnightRender.enabled = true;
                SelectedObject = Knight;
                CharacterInt--;
                break;
            case 4:
                RogueRender.enabled = false;
                Rogue.transform.position = OffScreenPosition;
                Mage.transform.position = CharacterPosition;
                MageRender.enabled = true;
                SelectedObject = Mage;
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
        if (SelectedCharacters.Count < 3)
        {
            SelectedCharacters.Add(SelectedObject);
            listEnum.Add(SelectedObject.GetComponent<Character>().UnitType);
            Options();
            ItemCheck();
            CharCountText();
            Debug.Log("SelectedObject: " + SelectedObject);
        }
    }

    public void Confirm()
    {
        if (SelectedCharacters.Count == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (SelectedCharacters.Count == 6)
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
        if (SelectedCharacters.Count < 6)
        {
            SelectedCharacters.Add(SelectedObject);
            listEnum.Add(SelectedObject.GetComponent<Character>().UnitType);
            Options();
            ItemCheck();
            CharCountText();
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
            RemoveItemDropbox();
            ItemCheck();
            CharCountText();
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

    public void Options()
    {
        DropdownOptions.Clear();
        DropdownOptions.Add(SelectedObject.GetComponent<Character>().Data.Name);
        CharacterDropdown.AddOptions(DropdownOptions);
        CharacterDropdown.RefreshShownValue();
    }

    private void ItemCheck() 
    {
        if (SelectedCharacters.Count == 3)
        {
            ConfirmBt.SetActive(true);
            AddBt.SetActive(false);
        }
        else if (SelectedCharacters.Count > 0)
        {
            RemoveBt.SetActive(true);
        }
        else if (SelectedCharacters.Count == 0) 
        {
            RemoveBt.SetActive(false);
        }
        else
        {
            ConfirmBt.SetActive(false);
            AddBt.SetActive(true);
        }       
    }

    private void CharCountText() 
    {
        CharacterCount.text = "Select Characters: " + SelectedCharacters.Count + "/3";
    }

    private void RemoveItemDropbox()
    {
        for (int i = 0; i < DropdownOptions.Count; i++)
        {
            if (DropdownOptions[i] == CharacterDropdown.options[CharacterDropdown.value].text)
            {
                Debug.Log(CharacterDropdown.options[CharacterDropdown.value].text);
                DropdownOptions.RemoveAt(i);
                break;
            }
        }
        for (int i = 0; i < CharacterDropdown.options.Count; i++)
        {
            if (CharacterDropdown.options[CharacterDropdown.value].text == CharacterDropdown.options[i].text)
            {
                CharacterDropdown.options.RemoveAt(i);
                CharacterDropdown.RefreshShownValue();
                break;
            }
        }
        for (int i = 0; i < SelectedCharacters.Count; i++)
        {
            if (SelectedCharacters[i].GetComponent<Character>().Data.Name == CharacterDropdown.options[CharacterDropdown.value].text)
            {
                Debug.Log(SelectedCharacters[i].GetComponent<Character>().Data.Name);
                Debug.Log(SelectedCharacters.Count);
                SelectedCharacters.RemoveAt(i);
                break;
            }
        }
    }
}
