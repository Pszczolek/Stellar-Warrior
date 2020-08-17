using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTimer : MonoBehaviour {
    [SerializeField] float elapsedTime;
    bool expired = false;
    GameObject powerUpOwner;
    Coroutine timerCoroutine;

    public void StartTimer(TimedPowerUp powerUp, GameObject owner, float time)
    {
        expired = false;
        powerUpOwner = owner;
        if(timerCoroutine != null) { StopCoroutine(timerCoroutine); }
        timerCoroutine = StartCoroutine(TimerCoroutine(powerUp, time));
    }

    private IEnumerator TimerCoroutine(TimedPowerUp powerUp,float time)
    {
        elapsedTime = 0;
        while(elapsedTime < time)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        powerUp.Deactivate(powerUpOwner);
        expired = true;
        Destroy(gameObject);
        yield return null;
    }
}
