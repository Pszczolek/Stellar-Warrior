using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

    [SerializeField] PrimaryWeaponType weapon;
    [SerializeField] float startingFireDelay = 1f;
    private bool isVisible = false;
    private Coroutine fireCoroutine;
    //private bool isFiring = false;

    private void Start()
    {

    }

    private void OnBecameInvisible()
    {
        isVisible = false;
        FireOff();
    }

    private void OnBecameVisible()
    {
        isVisible = true;
        FireOn();
    }

    private IEnumerator FireStartDelay()
    {
        yield return new WaitForSeconds(startingFireDelay);
        StartFiring();
        yield return null;
    }

    private void OnDisable()
    {
        FireOff();
    }

    private void OnEnable()
    {
        if(isVisible)
            FireOn();
    }

    public void FireOn()
    {
        StopCoroutine(FireStartDelay());
        //outOfBounds = false;
        //if (!isFiring)
        StartCoroutine(FireStartDelay());
    }

    public void FireOff()
    {
        StopCoroutine(FireStartDelay());
        //outOfBounds = true;
        //if (isFiring)
        StopFiring();
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
