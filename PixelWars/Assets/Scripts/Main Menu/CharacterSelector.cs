using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class CharacterSelector : MonoBehaviour
{
    [Header("Characters")]
    public List<GameObject> AllCharacters = new List<GameObject>();

    [Header("Dropdown")]
    public Dropdown CharacterDropdown;
    [Header("Buttons")]
    public GameObject ConfirmBt;
    public GameObject AddCharacterButton;
    public GameObject RemoveBt;
    public GameObject BackBt;
    [Header("Text")]
    public Text CharacterCount;
    public Text CharacterDescription;
    [Header("Sceneloader")]
    public SceneLoader sceneLoader;

    private List<GameObject> SelectableAbleCharacters = new List<GameObject>();
    private List<GameObject> SelectedCharacters = new List<GameObject>();
    private GameObject CurrentSelectedCharacterObject = null;
    private List<EnumUnit> listEnum = new List<EnumUnit>();

    private int PosInList = 0;
    private int teamsize = 3;
    private EnumTeams CurrentTeam = EnumTeams.Red;

    // Start is called before the first frame update
    void Start()
    {
        ClearAll();
        UpdateSelectableCharactersWithTeam();
        UpdateCurrentSelectableCharacter();
        CheckButtonsAvailable();
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

    public void ClearAll()
    {
        CharacterDropdown.ClearOptions();
        CharacterDropdown.value = 0;
        CharacterDropdown.RefreshShownValue();
        CurrentSelectedCharacterObject = null;
        PosInList = 0;
    }

    public void AddCharacterClick()
    {
        if (SelectedCharacters.Count < teamsize * 2)
        {
            SelectedCharacters.Add(CurrentSelectedCharacterObject);
            OptionData option = new OptionData(CurrentSelectedCharacterObject.GetComponent<Character>().CharacterName);
            CharacterDropdown.options.Add(option);
            CharacterDropdown.SetValueWithoutNotify(CharacterDropdown.options.Count - 1);
            CharacterDropdown.RefreshShownValue();

            UpdateText();

            CheckButtonsAvailable();
        }
    }

    private void UpdateText()
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

    IEnumerator UpdateCharacterDescriptionString(Character currentCharacter)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(currentCharacter.CharacterName);
        builder.AppendLine();
        builder.Append("Health: ");
        builder.Append(currentCharacter.MaxHealth);
        builder.AppendLine();
        builder.Append("Manacost: ");
        builder.Append(currentCharacter.ManaCost);
        builder.AppendLine();
        builder.Append("Damage: ");
        builder.Append(currentCharacter.Damage);
        builder.AppendLine();
        builder.Append("Speed: ");
        builder.Append(currentCharacter.Speed);
        builder.AppendLine();
        builder.Append("Range: ");
        builder.Append(currentCharacter.Range);
        builder.AppendLine();

        CharacterDescription.text = builder.ToString();
        yield return new WaitForSeconds(0.1f);
    }

    private void CheckButtonsAvailable()
    {
        if (SelectedCharacters.Count == teamsize && CurrentTeam == EnumTeams.Red )
        {
            ConfirmBt.SetActive(true);
            AddCharacterButton.SetActive(false);
        }
        else if (SelectedCharacters.Count == teamsize * 2 && CurrentTeam == EnumTeams.Blue)
        {
            ConfirmBt.SetActive(true);
            AddCharacterButton.SetActive(false);
        }
        else
        {
            ConfirmBt.SetActive(false);
            AddCharacterButton.SetActive(true);
        }

        if (SelectedCharacters.Count > 0 && CurrentTeam == EnumTeams.Red)
        {
            RemoveBt.SetActive(true);
        }
        else if (SelectedCharacters.Count > teamsize && CurrentTeam == EnumTeams.Blue)
        {
            RemoveBt.SetActive(true);
        }
        else
        {
            RemoveBt.SetActive(false);
        }
    }

    public void BackClick()
    {
        if (CurrentTeam == EnumTeams.Blue)
        {
            for (int i = 0; i < SelectedCharacters.Count; i++)
            {
                if (SelectedCharacters[i].GetComponent<Character>().TeamColor == CurrentTeam)
                {
                    SelectedCharacters.RemoveRange(i, SelectedCharacters.Count - 1 - i);
                    ClearAll();
                    CheckButtonsAvailable();
                    break;
                }
            }

            CurrentTeam = EnumTeams.Red;

            UpdateSelectableCharactersWithTeam();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void UpdateSelectableCharactersWithTeam()
    {
        SelectableAbleCharacters.Clear();
        foreach (GameObject ob in AllCharacters)
        {
            ob.SetActive(false);
            Character c = ob.GetComponent<Character>();

            if (c.TeamColor == CurrentTeam)
            {
                SelectableAbleCharacters.Add(ob);
            }
        }
    }

    private void UpdateCurrentSelectableCharacter()
    {
        foreach (GameObject ob in SelectableAbleCharacters)
        {
            ob.SetActive(false);
        }

        SelectableAbleCharacters[PosInList].SetActive(true);
        CurrentSelectedCharacterObject = SelectableAbleCharacters[PosInList];
        StartCoroutine(UpdateCharacterDescriptionString(SelectableAbleCharacters[PosInList].GetComponent<Character>()));
    }

    public void RemoveClick()
    {
        for (int i = 0; i < SelectedCharacters.Count; i++)
        {
            if (SelectedCharacters[i].GetComponent<Character>().CharacterName == CharacterDropdown.options[CharacterDropdown.value].text)
            {
                CharacterDropdown.options.Remove(CharacterDropdown.options[CharacterDropdown.value]);
                CharacterDropdown.value = 0;
                CharacterDropdown.RefreshShownValue();
                SelectedCharacters.Remove(SelectedCharacters[i]);
                UpdateText();
                CheckButtonsAvailable();
                break;
            }
        }
    }

    public void PreviousCharacterClick()
    {
        if (PosInList > 0)
        {
            PosInList--;
        }
        else
        {
            PosInList = SelectableAbleCharacters.Count - 1;
        }

        UpdateCurrentSelectableCharacter();
    }

    public void ConfirmClick()
    {
        if (SelectedCharacters.Count == teamsize)
        {
            CurrentTeam = EnumTeams.Blue;
            UpdateSelectableCharactersWithTeam();
            ClearAll();
            UpdateCurrentSelectableCharacter();
            CheckButtonsAvailable();
        }
        else if (SelectedCharacters.Count == teamsize * 2)
        {
            SavePlayerPrefs();
            Audio.PlayGameMusic();
            sceneLoader.LoadNextLevel();
            listEnum.Clear();
            SelectedCharacters.Clear();
        }
    }

    private void SavePlayerPrefs()
    {
        foreach (GameObject CharacterObject in SelectedCharacters)
        {
            listEnum.Add(CharacterObject.GetComponent<Character>().UnitType);
        }

        PlayerPrefs.SetInt("SelectionSize", listEnum.Count);
        for (int i = 0; i < listEnum.Count; i++)
        {
            PlayerPrefs.SetInt("Selection_" + i, (int)listEnum[i]);
        }
    }
}
