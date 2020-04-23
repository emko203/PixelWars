using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void ButtonBackToMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void ButtonQuitClick()
    {
        Application.Quit();
    }
}
