using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] float timer;
    [SerializeField] bool readyToSpawnNextWave = false;
    [SerializeField] bool looping = true;
    [SerializeField] GameEvent waveSpawnStartEvent;
    [SerializeField] GameEvent waveSpawnFinishedEvent;
    [SerializeField] GameEvent allWavesSpawnedEvent;

    private Transform enemiesMainTransform;

    Coroutine spawnCoroutine;
    float timeToNextWave;

    private void Awake()
    {
        enemiesMainTransform = GameObject.Find("Enemies").transform;
    }

    // Use this for initialization
    IEnumerator Start () {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
            if(allWavesSpawnedEvent)
                allWavesSpawnedEvent.Raise();
        }
        while (looping);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnNextWave()
    {
        readyToSpawnNextWave = true;
        Debug.Log("Ready for next wave");
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            StartWaveTimer(waveConfigs[waveIndex].GetWaveStartDelay());
            //yield return new WaitForSeconds(waveConfigs[waveIndex].GetWaveStartDelay());
            yield return new WaitUntil( () => readyToSpawnNextWave == true);
            Debug.Log("SpawningNextWave");
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[waveIndex]));
        }
    }

    private void StartWaveTimer(float time)
    {
        StartCoroutine(WaveTimer(time));
    }

    private IEnumerator WaveTimer(float time)
    {
        timer = time;
        while(time > 0 && !readyToSpawnNextWave)
        {
            time -= Time.deltaTime;
            timer = time;
            yield return null;
        }
        readyToSpawnNextWave = true;
        yield return null;
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig myWave)
    {
        readyToSpawnNextWave = false;
        if(waveSpawnStartEvent)
            waveSpawnStartEvent.Raise(myWave.GetNumberOfEnemies());
        for (int enemiesSpawned = 0; enemiesSpawned < myWave.GetNumberOfEnemies(); enemiesSpawned++)
        {
            if (myWave.GetEnemyPrefab())
            {
                GameObject enemy = Instantiate(myWave.GetEnemyPrefab(), transform.position, Quaternion.identity);
                EnemyPathing pathing = enemy.GetComponent<EnemyPathing>();
                if (pathing)
                {
                    pathing.SetPathing(myWave, enemiesSpawned);
                }
                Damageable damageable = enemy.GetComponent<Damageable>();
                if (damageable && !enemy.GetComponent<BossBehaviour>())
                {
                    damageable.SetInvincible(true);
                    enemy.transform.SetParent(enemiesMainTransform);
                }
            }

            float timeToNextSpawn = myWave.GetTimeBetweenSpawns() + Random.Range(0, myWave.GetSpawnRandomFactor());
            yield return new WaitForSeconds(timeToNextSpawn);
        }
        if(waveSpawnFinishedEvent)
            waveSpawnFinishedEvent.Raise();
        //waveIndex = (waveIndex + 1) % waveConfigs.Count;
        //currentWave = waveConfigs[waveIndex];
        //spawnCoroutine = StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        yield return null;

    }
}
