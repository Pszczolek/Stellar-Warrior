using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : MonoBehaviour {
    [SerializeField] float turnRate = 360;
    [SerializeField] float sightRadius = 360;
    [SerializeField] float lockOnDelay = .5f;
    [SerializeField] float maxTrackingTime = 5f;
    [SerializeField] bool enemyProjectile = false;

    private Rigidbody2D myRigidBody;
    private Damageable[] trackedObjects;
    [SerializeField] Damageable target;
    private bool isOutOfBounds = false;
    private float timeTracking;


	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine(TrackTargets());
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameInvisible()
    {
        //isOutOfBounds = true;
    }

    private IEnumerator TrackTargets()
    {
        yield return new WaitForSeconds(lockOnDelay);
        timeTracking = 0;
        while (!isOutOfBounds)
        {
            if (timeTracking >= maxTrackingTime)
                break;
            timeTracking += Time.deltaTime;
            if (target == null || !IsTargetInSightRadius(target.transform.position))
            {
                FindClosestTarget();
            }
            else
            {
                TurnTowardsTarget(target.transform.position);
                MoveForward();
            }
            yield return null;
        }
        yield return null;
    }

    private bool FindClosestTarget()
    {
        if (enemyProjectile)
        {
            trackedObjects = FindObjectsOfType<PlayerDamageable>();
        }
        else
        {
            trackedObjects = FindObjectsOfType<EnemyDamageable>();
        }
        float distanceFromCurrentTarget = 100;
        float distanceChecking;
        foreach (Damageable obj in trackedObjects)
        {
            distanceChecking = Vector2.Distance(transform.position, obj.transform.position);
            if (distanceChecking < distanceFromCurrentTarget
                && IsTargetInSightRadius(obj.transform.position)
                && !obj.IsDead)
            {
                target = obj;
                distanceFromCurrentTarget = distanceChecking;
            }
        }

        return target;
    }

    private bool IsTargetInSightRadius(Vector2 targetPos)
    {
        Vector2 pos = transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, targetPos - pos);
        float currentAngle = myRigidBody.rotation;

        if (Mathf.Abs(Mathf.DeltaAngle(targetAngle, currentAngle)) > sightRadius / 2)
            { return false; }
        return true;
    }

    private void TurnTowardsTarget(Vector2 targetPos)
    {
        Vector2 pos = transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, targetPos - pos);
        float currentAngle = myRigidBody.rotation;
        float stepThisFrame = turnRate * Time.deltaTime;

        myRigidBody.rotation = Mathf.LerpAngle(currentAngle, targetAngle,
                                        stepThisFrame / Mathf.Abs(Mathf.DeltaAngle(targetAngle, currentAngle)));

    }

    void MoveForward()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
    }
}
