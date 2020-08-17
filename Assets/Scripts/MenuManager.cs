using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject levelSelectionMenu;
    [SerializeField] Button nextLevelButton;
    [SerializeField] Button previousLevelButton;
    [SerializeField] Text selectedLevelText;
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameSession gameSession;
    [SerializeField] int selectedLevel = 1;
    [SerializeField] int unlockedLevel = 3;

    //private void Awake()
    //{
    //    InitializeMenuValues();
    //}

    //private void InitializeMenuValues()
    //{

    //}

    public void SetSoundVolume(float value)
    {
        PlayerPrefsManager.SoundVolume = value;
    }

    public void ShowSettings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);

        musicSlider.value = PlayerPrefsManager.MusicVolume;
        soundSlider.value = PlayerPrefsManager.SoundVolume;
    }

    public void ShowLevelSelectionMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);

        unlockedLevel = gameSession.LastLevelUnlocked;
        selectedLevel = unlockedLevel;
        selectedLevelText.text = selectedLevel.ToString();

        nextLevelButton.gameObject.SetActive(false);
        if (unlockedLevel == 1)
        {
            previousLevelButton.gameObject.SetActive(false);
        }
    }

    public void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        levelSelectionMenu.SetActive(false);
    }

    public void SelectNextLevel()
    {
        selectedLevel++;
        selectedLevelText.text = selectedLevel.ToString();
        if(selectedLevel == unlockedLevel)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
        previousLevelButton.gameObject.SetActive(true);
    }

    public void SelectPreviousLevel()
    {
        selectedLevel--;
        selectedLevelText.text = selectedLevel.ToString();
        if (selectedLevel == 1)
        {
            previousLevelButton.gameObject.SetActive(false);
        }
        nextLevelButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        levelManager.LoadLevel(selectedLevel);
    }
}
