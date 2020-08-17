using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player movement")]

    [SerializeField] PlayerInputBase playerInput;
    [SerializeField] float speed = 5f;
    [SerializeField] float sidewaysPadding = 0.5f;
    [SerializeField] float topPadding = 2f;
    [SerializeField] float bottomPadding = 2f;

    [Header("Weapons")]

    [SerializeField] PrimaryWeaponType defaultWeapon;
    [SerializeField] PrimaryWeaponType currentWeapon;
    [SerializeField] SecondaryWeaponType secondaryWeapon;

    //[SerializeField] GameObject projectilePrefab;
    //[SerializeField] AudioClip projectileSFX;
    //[SerializeField] float projectileSpeed = 10f;
    //[SerializeField] float fireCooldown = .5f;

    [Header("Game Session")]
    [SerializeField] GameSession gameSession;

    private Coroutine firingCoroutine;
    private bool isFiring = false;
    private AudioSource myAudioSource;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

	// Use this for initialization
	void Start () {
        myAudioSource = GetComponent<AudioSource>();
        SetUpBoundaries();
        AutoFire();
    }

    // Update is called once per frame
    void Update () {
        //FaceMouseCursor();
        if(Time.timeScale > 0)
            Move();
        //FireSecondary();


	}

    public void ChangeWeapon(PrimaryWeaponType newWeapon)
    {
        currentWeapon = newWeapon;
        if (isFiring)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = StartCoroutine(currentWeapon.Fire(gameObject));
        }
    }

    public void ResetWeapon()
    {
        ChangeWeapon(defaultWeapon);
    }

    private void AutoFire() {
        isFiring = true;
        firingCoroutine = StartCoroutine(currentWeapon.Fire(gameObject));
    }

    private void StopFire()
    {
        StopCoroutine(firingCoroutine);
        isFiring = false;
    }

    private void FireSecondary()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            secondaryWeapon.Fire(gameObject);
        }
    }
  
    private void Move()
    {
        var movementDelta = playerInput.ProcessInput();
        var newPosX = Mathf.Clamp((transform.position.x + movementDelta.x), xMin, xMax);
        var newPosY = Mathf.Clamp((transform.position.y + movementDelta.y), yMin, yMax);


        transform.position = new Vector2(newPosX, newPosY);
    }

    private void SetUpBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + sidewaysPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - sidewaysPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + bottomPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - topPadding;
    }

    //private void FaceMouseCursor()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    Vector2 mousePosConverted = Camera.current.ScreenToWorldPoint(mousePos);
    //    Vector2 shipPos = transform.position;

    //    float angle = Vector2.SignedAngle(Vector2.up, mousePosConverted - shipPos);
    //    Debug.Log(mousePos + " " + mousePosConverted + " " + angle);
    //    transform.rotation = Quaternion.Euler(0, 0, angle);
    //}

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnBecameInvisible()
    {
        Destroy(this);
    }

}
