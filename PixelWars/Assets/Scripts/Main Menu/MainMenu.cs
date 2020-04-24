using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SettingsMenu settingsMenu;
    public SceneLoader sceneLoader;

    private void Start()
    {
        Audio.PlayMenuMusic();
        settingsMenu.LoadSettings();
    }

    public void PlayGame() 
    {
        sceneLoader.LoadNextLevel();
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
