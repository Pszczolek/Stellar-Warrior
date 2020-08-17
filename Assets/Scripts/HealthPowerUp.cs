using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Power Ups/Health Power Up")]
public class HealthPowerUp : PowerUp{

    public int healthAmount;

    public override void Activate(GameObject owner)
    {
        PlayerDamageable damageable = owner.GetComponent<PlayerDamageable>();
        if (damageable) { damageable.GainHealth(healthAmount); }      
    }

}
