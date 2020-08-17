using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedPowerUp : PowerUp {
    public float duration;
    public PowerUpTimer timerPrefab;

    public abstract void Deactivate(GameObject owner);

}
