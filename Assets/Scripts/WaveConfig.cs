using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float waveStartDelay = 5f;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] float spawnRandomFactor = .5f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Vector2 _pathOffsetPerSpawn;
    [SerializeField] PathingBehavior _pathingBehavior = PathingBehavior.Single;
    [SerializeField] bool loopMovement = false;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public float GetWaveStartDelay() { return waveStartDelay; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }
    public bool IsMovementLooped() { return loopMovement; }
    public PathingBehavior PathingBehavior { get { return _pathingBehavior; } }
    public Vector2 PathOffsetPerSpawn { get { return _pathOffsetPerSpawn; } }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform waypoint in pathPrefab.transform){
            waveWaypoints.Add(waypoint);
        }
        return waveWaypoints;
    }
}
