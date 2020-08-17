using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static int previousLevelIndex = 1;
    public float loadDelay = 1f;
    Coroutine loadCoroutine;

    public void LoadNextLevel()
    {
        //StopCoroutine(loadCoroutine);
        loadCoroutine = StartCoroutine(WaitAndLoad(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMenu()
    {
        //StopCoroutine(loadCoroutine);
        loadCoroutine = StartCoroutine(WaitAndLoad("Main Menu"));
        //SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevel(int whichLevelIndex)
    {
        //StopCoroutine(loadCoroutine);
        loadCoroutine = StartCoroutine(WaitAndLoad(whichLevelIndex));
        //SceneManager.LoadScene(whichLevelIndex);
    }

    public void RetryLevel()
    {
        loadCoroutine = StartCoroutine(WaitAndLoad(previousLevelIndex));
    }

    public void LoadGameOverScreen()
    {
        //StopCoroutine(loadCoroutine);
        StartCoroutine(WaitAndLoad("Game Over"));
    }

    IEnumerator WaitAndLoad(string levelName)
    {
        yield return new WaitForSeconds(loadDelay);
        previousLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelName);
        yield return null;
    }

    IEnumerator WaitAndLoad(int levelIndex)
    {
        yield return new WaitForSeconds(loadDelay);
        previousLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
        yield return null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
