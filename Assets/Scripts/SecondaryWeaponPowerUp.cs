using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Power Ups/Secondary Weapon Power Up")]
public class SecondaryWeaponPowerUp : PowerUp {
    public SecondaryWeaponType secondaryWeapon;

    public override void Activate(GameObject owner)
    {
        secondaryWeapon.Fire(owner);
    }
}
