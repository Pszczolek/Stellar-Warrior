using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : Damageable {

    [SerializeField] IntVariable health;
    [SerializeField] IntVariable maxHealth;
    [SerializeField] GameSession gameSession;

    [Header("GameEvents")]
    [SerializeField] GameEvent playerDamageEvent;
    [SerializeField] GameEvent playerShieldedEvent;
    [SerializeField] GameEvent playerShieldExpiredEvent;

    private Coroutine invinicilityCoroutine;
    private float invincibilityTime;
    private bool permanentInvincibility = false;

    protected override void TakeDamage(int damageAmount)
    {
        if (!invincible && health)
        {
            health.Value -= damageAmount;
            playerDamageEvent.Raise();
            if (health.Value <= 0)
            {
                Die();
            }
        }

    }

    public void GainHealth(int amount)
    {
        if (maxHealth.Value > health.Value + amount)    { health.Value += amount; }
        else                                            { health.Value = maxHealth.Value; }
    }

    protected override void Die()
    {
        base.Die();
        gameSession.LoseLife();
    }

    public void TimedInvincibility(float time)
    {
        SetInvincible(true);
        playerShieldedEvent.Raise();
        invincibilityTime = time;
        if (time <= 0)
            permanentInvincibility = true;
        invinicilityCoroutine = StartCoroutine(InvincibilityTimer());
    }

    public void StopInvincibilityTimer()
    {
        StopCoroutine(invinicilityCoroutine);
        SetInvincible(false);
    }

    private IEnumerator InvincibilityTimer()
    {
        if (invincibilityTime > 0)
        {
            while (invincibilityTime > 0)
            {
                invincibilityTime -= Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (permanentInvincibility)
            {
                yield return null;
            }
        }
        playerShieldExpiredEvent.Raise();
        SetInvincible(false);

    }

    //private void OnDestroy()
    //{
    //    if (!isDead)
    //        gameSession.LoseLife();
    //}

}
