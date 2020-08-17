using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Power Ups/Weapon Power Up")]
public class WeaponPowerUp : TimedPowerUp {
    public PrimaryWeaponType weapon;
    private PowerUpTimer timer;

    public override void Activate(GameObject owner)
    {
        owner.GetComponent<Player>().ChangeWeapon(weapon);
        timer = owner.GetComponentInChildren<PowerUpTimer>();
        if (!timer)
        {
            timer = Instantiate(timerPrefab, owner.transform);
        }

        timer.StartTimer(this, owner, duration);
    }

    public override void Deactivate(GameObject owner)
    {
        if (owner) { owner.GetComponent<Player>().ResetWeapon(); };
    }
}
