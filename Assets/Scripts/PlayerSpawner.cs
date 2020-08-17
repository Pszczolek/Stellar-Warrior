using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    [SerializeField] GameObject playerShipPrefab;
    [SerializeField] GameObject spawnVFX;


    public void SpawnPlayerShip(float respawnTime, float respawnInvincibilityTime = 0)
    {
        StartCoroutine(SpawnWithDelay(respawnTime, respawnInvincibilityTime));
    }

    private IEnumerator SpawnWithDelay(float respawnTime, float respawnInvincibilityTime)
    {
        yield return new WaitForSeconds(respawnTime);
        GameObject newPlayerShip = Instantiate(playerShipPrefab);
        GameObject vfx = Instantiate(spawnVFX, newPlayerShip.transform.position, Quaternion.identity);
        if (respawnInvincibilityTime > 0)
            newPlayerShip.GetComponent<PlayerDamageable>().TimedInvincibility(respawnInvincibilityTime);
        else
            newPlayerShip.GetComponent<PlayerDamageable>().SetInvincible(false);
        Destroy(vfx, 2f);
        Destroy(gameObject);
        yield return null;
    }
    
}
