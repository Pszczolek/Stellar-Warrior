using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

    [SerializeField] PrimaryWeaponType weapon;
    [SerializeField] float startingFireDelay = 1f;
    private bool outOfBounds = false;
    private Coroutine delayCoroutine;
    private Coroutine fireCoroutine;
    //private bool isFiring = false;

    private void Start()
    {
        
    }

    private void OnBecameInvisible()
    {
        StopCoroutine(FireStartDelay());
        outOfBounds = true;
        //if (isFiring)
            StopFiring();
    }

    private void OnBecameVisible()
    {
        StopCoroutine(FireStartDelay());
        outOfBounds = false;
        //if (!isFiring)
        delayCoroutine = StartCoroutine(FireStartDelay());
    }

    private IEnumerator FireStartDelay()
    {
        yield return new WaitForSeconds(startingFireDelay);
        StartFiring();
        yield return null;
    }

    private void OnDisable()
    {
        StopFiring();
    }

    private void OnEnable()
    {
        StartFiring();
    }

    public void StartFiring()
    {
        //isFiring = true;
        if (fireCoroutine == null)
            fireCoroutine = StartCoroutine(weapon.Fire(gameObject));
    }

    public void StopFiring()
    {
        //isFiring = false;
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }

    }


}
