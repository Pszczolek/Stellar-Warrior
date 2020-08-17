using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Type/Primary Weapon Type")]
public class PrimaryWeaponType : WeaponType {



    public virtual IEnumerator Fire(GameObject shooter)
    {
        while (shooter)
        {
            if(firingShape == FiringShape.Straight){ FireStraight(shooter); }
            if(firingShape == FiringShape.Cone) { FireCone(shooter); }
            shooter.GetComponent<AudioSource>().PlayOneShot(projectileSFX, PlayerPrefsManager.SoundVolume);
            yield return new WaitForSeconds(fireCooldown);
        }
        yield return null;
    }



}
