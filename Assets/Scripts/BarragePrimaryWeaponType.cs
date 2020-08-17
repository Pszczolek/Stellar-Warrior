using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Type/Barrage Primary Weapon Type")]
public class BarragePrimaryWeaponType : PrimaryWeaponType
{
    public float timeBetweenShotsInBarrage = 0;


    public override IEnumerator Fire(GameObject shooter)
    {
        while (shooter)
        {
            for (int numShot = 1; numShot <= numberOfProjectiles; numShot++)
            {
                //Debug.Log("Shooting "+numShot);
                FireSingleBarrageShot(shooter, numShot);
                shooter.GetComponent<AudioSource>().PlayOneShot(projectileSFX, PlayerPrefsManager.SoundVolume);
                yield return new WaitForSeconds(timeBetweenShotsInBarrage);
            }

            yield return new WaitForSeconds(fireCooldown);
        }
        yield return null;
    }

    public void FireSingleBarrageShot(GameObject shooter, int shotNumber)
    {
        if (firingShape == FiringShape.Straight)
            FireStraightOnce(shooter, shotNumber);
        if (firingShape == FiringShape.Cone)
            FireInConeOnce(shooter, shotNumber);
    }



}
