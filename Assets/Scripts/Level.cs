using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    public bool gameOver = false;
    public int EnemiesCount{ get { return enemiesList.Count; } }
    public int EnemiesSpawned { get { return _enemiesSpawned; } }
    public int EnemiesDestroyed { get { return _enemiesDestroyed; } }
    public int EnemiesInWave { get { return _enemiesInWave; } }
    public int EnemiesInWaveDestroyed { get { return _enemiesInWaveDestroyed; } }
    [SerializeField] int _enemiesSpawned;
    [SerializeField] int _enemiesDestroyed;
    [SerializeField] int _enemiesInWave;
    [SerializeField] int _enemiesInWaveSpawned;
    [SerializeField] int _enemiesInWaveDestroyed;
    [SerializeField] bool spawnPlayerAtStart = true;
    [SerializeField] bool resetGame = false;
    [SerializeField] bool levelFinished = false;
    [SerializeField] bool lastWave = false;
    [SerializeField] GameSession gameSession;
    [SerializeField] GameEvent enemyWaveDestroyedEvent;
    [SerializeField] GameEvent levelFinishedEvent;
    [SerializeField] PlayerInventory baseInventory;
    [SerializeField] PlayerInventory currentInventory;
    [SerializeField] List<GameObject> enemiesList;

    private int levelIndex;

    private void Awake()
    {
        if (resetGame)
        {
            ResetGameSession();
        }
        else
        {
            levelIndex = SceneManager.GetActiveScene().buildIndex;
            StartLevel();
        }
    }
    // Use this for initialization
    void Start () {

	}

    void ResetGameSession()
    {
        gameSession.ResetGameSession();
    }

    void StartLevel()
    {
        gameSession.StartLevel(spawnPlayerAtStart);
        if(baseInventory == null)
            return;
        for (int i = 0; i < baseInventory.items.Length; i++)
        {
            currentInventory.items[i] = baseInventory.items[i];
        }
    }
	
	// Update is called once per frame
	void Update () {
		//if (gameOver)
  //      {
  //          gameOver = false;
  //          FindObjectOfType<LevelManager>().LoadGameOverScreen();
  //      }
	}

    public void LevelFinished()
    {
        if (levelIndex + 2 < SceneManager.sceneCountInBuildSettings)
        {
            gameSession.UnlockLevel(levelIndex + 1);
        }
        levelFinished = true;
        levelFinishedEvent.Raise();
    }

    public void LastWave()
    {
        lastWave = true;
    }

    public void ResetWaveCount(int howMany)
    {
        if (enemiesList.Count == 0)
        {
            _enemiesInWave = howMany;
            _enemiesInWaveDestroyed = 0;
            _enemiesInWaveSpawned = 0;
        }
        else
        {
            _enemiesInWave += howMany;
        }


    }

    public void OnEnemySpawned(GameObject enemy)
    {
        _enemiesSpawned++;
        _enemiesInWaveSpawned++;
        AddEnemyToList(enemy);
    }

    public void OnEnemyDestroyed(GameObject enemy)
    {
        _enemiesDestroyed++;
        _enemiesInWaveDestroyed++;
        RemoveEnemyFromList(enemy);
        CheckWaveState();


    }

    public void OnEnemyRemoved(GameObject enemy)
    {
        RemoveEnemyFromList(enemy);
        CheckWaveState();

    }

    private void CheckWaveState()
    {
        //if (EnemiesInWaveDestroyed == EnemiesInWave && EnemiesCount == 0)
        if(_enemiesInWaveSpawned == EnemiesInWave && EnemiesCount == 0) { 
            enemyWaveDestroyedEvent.Raise();
            if (lastWave)
            {
                LevelFinished();
            }
        }
    }

    private void AddEnemyToList(GameObject enemy)
    {
        enemiesList.Add(enemy);
    }

    private void RemoveEnemyFromList(GameObject enemy)
    {
        if (enemiesList.Contains(enemy))
        {
            enemiesList.Remove(enemy);
        }
        if (lastWave && enemiesList.Count == 0 && _enemiesInWaveSpawned == _enemiesInWave)
        {
            LevelFinished();
        }
    }
}
