using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Session")]
public class GameSession : ScriptableObject {

    [SerializeField] IntVariable score;
    [SerializeField] IntVariable lives;
    [SerializeField] IntVariable health;
    [SerializeField] IntVariable maxHealth;
    [SerializeField] GameObject playerShipPrefab;
    [SerializeField] float startRespawnDelay = 1f;
    [SerializeField] float respawnDelay = 3f;
    [SerializeField] float respawnInvincibilityTime = 2f;
    [SerializeField] float timeScale = 1f;
    [SerializeField] int lastLevelUnlocked = 1;
    [SerializeField] GameObject playerSpawnerPrefab;
    [SerializeField] GameEvent scoreEvent;
    [SerializeField] GameEvent playerDestroyedEvent;
    [SerializeField] GameEvent gameOverEvent;
    [SerializeField] GameEvent playerRespawnEvent;
    [SerializeField] GameEvent gamePausedEvent;
    [SerializeField] GameEvent gameUnpausedEvent;
    [SerializeField] SaveData saveData;
    [SerializeField] bool resetProggress;

    public int LastLevelUnlocked {
        get { return lastLevelUnlocked; }
        set { lastLevelUnlocked = value; }
    }

    private void OnEnable()
    {
        if (saveData == null)
        {
            saveData = SaveSystem.LoadProggress();
            if(saveData == null)
            {
                saveData = new SaveData { unlockedLevel = lastLevelUnlocked };
            } else
            {
                lastLevelUnlocked = saveData.unlockedLevel;
            }
        }
        if (resetProggress)
        {
            lastLevelUnlocked = 1;
            saveData.unlockedLevel = 1;
            SaveProggress();
        }
    }

    public int GetScore()
    {
        return score.Value;
    }

    public void AddScore(int points)
    {
        score.Value += points;
        scoreEvent.Raise();
    }

    public int GetLives()
    {
        return lives.Value;
    }

    public int GetHealth()
    {
        return health.Value;
    }

    public int GetMaxHealth()
    {
        return maxHealth.Value;
    }

    public void LoseLife()
    {
        lives.Value--;
        if(lives.Value <= 0)
        {
            GameOver();
        }
        else
        {
            playerDestroyedEvent.Raise();
            RespawnPlayer(respawnDelay);
        }
    }

    public void GameOver()
    {
        gameOverEvent.Raise();
        FindObjectOfType<LevelManager>().LoadGameOverScreen();
    }

    public void RespawnPlayer(float respawnDelay = 3)
    {
        health.Value = maxHealth.Value;
        playerRespawnEvent.Raise();
        GameObject playerSpawner = Instantiate(playerSpawnerPrefab);
        playerSpawner.GetComponent<PlayerSpawner>().SpawnPlayerShip(respawnDelay, respawnInvincibilityTime);

    }

    public void ResetGameSession()
    {
        score.ResetValue();
        lives.ResetValue();
        health.Value = maxHealth.Value;
    }

    public void StartLevel(bool spawnPlayer = false)
    {
        lives.ResetValue();
        health.Value = maxHealth.Value;
        if (spawnPlayer)
        {
            RespawnPlayer(startRespawnDelay);
        }
    }

    public void UnlockLevel(int whichLevel)
    {
        if (lastLevelUnlocked < whichLevel) {
            lastLevelUnlocked = whichLevel;
            SaveProggress();
        };
    }

    public void SaveProggress()
    {
        SaveSystem.SaveProggress(saveData);
    }

    public void LoadProggress()
    {
        saveData = SaveSystem.LoadProggress();
    }

    public void ResetProggress()
    {
        saveData.unlockedLevel = 1;
        lastLevelUnlocked = 1;
        SaveProggress();
    }

    public void TogglePauseGame()
    {
        if(Time.timeScale > 0)
        {
            Time.timeScale = 0;
            gamePausedEvent.Raise();
        }
        else
        {
            Time.timeScale = timeScale;
            gameUnpausedEvent.Raise();
        }

    }

}
