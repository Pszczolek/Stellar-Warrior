using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : Damageable {

    [SerializeField] int health = 100;
    [SerializeField] int scoreValue = 100;
    [SerializeField] GameSession gameSession;
    [SerializeField] DropTable dropTable;
    [SerializeField] GameEvent enemySpawnedEvent;
    [SerializeField] GameEvent enemyDestroyedEvent;
    [SerializeField] GameEvent enemyRemovedEvent;
    [SerializeField] EnemyHealthDisplay healthDisplay;
    public int Health { get { return health; } }

    private void Start()
    {
        enemySpawnedEvent.Raise(gameObject);
    }

    protected override void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (healthDisplay)
            healthDisplay.UpdateDisplay(health);
        if (health <= 0)
            Die();
    }

    protected override void Die()
    {
        base.Die();
        if (dropTable)
        {
            dropTable.DropItems(transform.position);
        }

        enemyDestroyedEvent.Raise(gameObject);
        if (healthDisplay)
        {
            healthDisplay.gameObject.SetActive(false);
        }

        gameSession.AddScore(scoreValue);
    }


    private void OnDestroy()
    {
        if(!isDead)
            enemyRemovedEvent.Raise(gameObject);
    }
}
