using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

    [SerializeField] int damage = 100;
    [SerializeField] GameObject impactVFX;
    [SerializeField] bool selfDestructOnHit = true;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (impactVFX)
        {
            Destroy(Instantiate(impactVFX, transform.position, Quaternion.identity), 2f);
        }
        if (selfDestructOnHit)
        {
            Destroy(gameObject);
        }
    }
}
