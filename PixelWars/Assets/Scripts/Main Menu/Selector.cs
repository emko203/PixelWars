using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    [HideInInspector]
    public GameObject SelectedObject;

    public List<GameObject> SelectableAbleCharacters = new List<GameObject>();

    public Dropdown CharacterDropdown;

    public GameObject ConfirmBt;
    public GameObject AddBt;
    public GameObject RemoveBt;

    public Text CharacterCount;

    public static List<GameObject> SelectedCharacters = new List<GameObject>();

    public static List<EnumUnit> listEnum = new List<EnumUnit>();

    public static List<string> DropdownOptions = new List<string>();

    private int PosInList = 0;
    private int teamsize = 3;

    private void Start()
    {
        ClearAll();
        UpdateCurrentSelectableCharacter();
    }

    public void ClearAll()
    {
        listEnum.Clear();
        SelectedCharacters.Clear();
        DropdownOptions.Clear();
        SelectedObject = null;
        CharacterDropdown.ClearOptions();
    }

    public void NextCharacter()
    {
        if (PosInList < SelectableAbleCharacters.Count - 1)
        {
            PosInList++;
        }
        else
        {
            PosInList = 0;
        }

        UpdateCurrentSelectableCharacter();
    }

    private void UpdateCurrentSelectableCharacter()
    {
        foreach (GameObject ob in SelectableAbleCharacters)
        {
            ob.SetActive(false);
        }

        SelectableAbleCharacters[PosInList].SetActive(true);

        SelectedObject = SelectableAbleCharacters[PosInList];
    }

    public void PreviousCharacter()
    {
        if (PosInList > 0)
        {
            PosInList--;
        }
        else
        {
            PosInList = SelectableAbleCharacters.Count -1;
        }

        UpdateCurrentSelectableCharacter();
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void AddCharacter()
    {
        if (SelectedCharacters.Count < teamsize)
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
        if (SelectedCharacters.Count == teamsize)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (SelectedCharacters.Count == teamsize * 2)
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
        if (SelectedCharacters.Count < teamsize * 2)
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
        DropdownOptions.Add(SelectedObject.GetComponent<Character>().CharacterName);
        CharacterDropdown.AddOptions(DropdownOptions);
        CharacterDropdown.RefreshShownValue();
    }

    private void ItemCheck() 
    {
        if (SelectedCharacters.Count == teamsize || SelectedCharacters.Count == teamsize * 2)
        {
            ConfirmBt.SetActive(true);
            AddBt.SetActive(false);
        }
        
        if(ConfirmBt.activeSelf && SelectedCharacters.Count != teamsize)
        {
            ConfirmBt.SetActive(false);
            AddBt.SetActive(true);
        }

        if (SelectedCharacters.Count > 0)
        {
            RemoveBt.SetActive(true);
        }
        else
        {
            RemoveBt.SetActive(false);
        }   
    }

    private void CharCountText() 
    {
        if (SelectedCharacters.Count > teamsize)
        {
            CharacterCount.text = "Select Characters: " + (SelectedCharacters.Count - teamsize) + "/3";
        }
        else
        {
            CharacterCount.text = "Select Characters: " + (SelectedCharacters.Count) + "/3";
        }
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
       
        for (int i = 0; i < SelectedCharacters.Count; i++)
        {
            if (CharacterDropdown.options.Count > 0)
            {
                if (SelectedCharacters[i].GetComponent<Character>().CharacterName == CharacterDropdown.options[CharacterDropdown.value].text)
                {
                    Debug.Log(SelectedCharacters[i].GetComponent<Character>().CharacterName);
                    Debug.Log(SelectedCharacters.Count);
                    SelectedCharacters.RemoveAt(i);
                    break;
                }
            }
        }

        for (int i = 0; i < CharacterDropdown.options.Count; i++)
        {
            if (CharacterDropdown.options[CharacterDropdown.value].text == CharacterDropdown.options[i].text)
            {
                CharacterDropdown.options.RemoveAt(i);
                CharacterDropdown.value = 0;
                CharacterDropdown.RefreshShownValue();
                break;
            }
        }
    }
}
