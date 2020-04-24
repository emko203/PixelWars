using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float TransitionTime = 1f;
    [SerializeField] private Animator TransitionAnimator;

    public void LoadNextLevel()
    {
        int SceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        StartCoroutine(PlayTransition(SceneToLoad));
    }

    public void LoadFirstLevel()
    {
        int SceneToLoad = 0;

        StartCoroutine(PlayTransition(SceneToLoad));
    }

    IEnumerator PlayTransition(int sceneToLoad)
    {
        TransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
