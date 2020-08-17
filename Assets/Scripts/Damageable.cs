using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour {

    [SerializeField] protected bool invincible = false;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] Animator animator;
    protected bool isDead = false;
    public bool IsDead { get { return isDead; } }

    public void SetInvincible(bool toggle)
    {
        invincible = toggle;
    }

    private void OnBecameVisible()
    {
        invincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        
        if (!damageDealer) { return; }
        damageDealer.Hit();
        if (!invincible && !isDead)
        {
            TakeDamage(damageDealer.GetDamage());
        }
    }

    abstract protected void TakeDamage(int damageAmount);

    protected virtual void Die()
    {
        isDead = true;
        if (animator)
        {
            isDead = true;
            animator.SetTrigger("Death");
            GetComponent<Rigidbody2D>().Sleep();
        }
        else
        {
            DisableComponents();
        }

        //gameObject.SetActive(false);
        //PlayEffects();
        //Destroy(gameObject);
    }

    protected void PlayEffects()
    {
        if (explosionSFX)
        {
            Debug.Log("Playing death sound");
            AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, PlayerPrefsManager.SoundVolume);
            //GetComponent<AudioSource>().PlayOneShot(explosionSFX);
        }

        //if (explosionVFX)
        //{
        //    GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        //    Destroy(explosion, explosionDuration);
        //}
    }

    private void DisableComponents()
    {
        Destroy(this);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
    }

    public void DeathInstant()
    {
        Destroy(gameObject);
    }
}
