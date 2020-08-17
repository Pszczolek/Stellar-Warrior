using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Weapon Type/Secondary Weapon Type")]
public class SecondaryWeaponType : WeaponType {

    public void Fire(GameObject shooter)
    {
        if (shooter)
        {
            if (firingShape == FiringShape.Straight) { FireStraight(shooter); }
            if (firingShape == FiringShape.Cone) { FireCone(shooter); }
            shooter.GetComponent<AudioSource>().PlayOneShot(projectileSFX, PlayerPrefsManager.SoundVolume);
        }
    }

}
