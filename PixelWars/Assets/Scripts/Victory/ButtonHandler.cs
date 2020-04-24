using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    public void ButtonBackToMenuClick()
    {
        sceneLoader.LoadFirstLevel();
    }

    public void ButtonQuitClick()
    {
        Application.Quit();
    }
}
