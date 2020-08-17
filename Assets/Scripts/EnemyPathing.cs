using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathingBehavior { Single, Loop, Stationary}

public class EnemyPathing : MonoBehaviour {

    public bool IsMoving { get; set; }
    [SerializeField] float turnRate = .1f;
    [SerializeField] Rigidbody2D myRigidBody;
    private List<Transform> waypoints;
    private float speed = 2f;
    private bool loopMovement = false;
    private Vector2 pathingOffset;
    private PathingBehavior pathingBehavior;
    private Vector2 smoothVelocity = Vector2.zero;

    int waypointIndex = 0;

	// Use this for initialization
	void Start () {
        IsMoving = false;
	}

    public void SetPathing(WaveConfig waveConfig, int spawnIndex)
    {
        waypoints = waveConfig.GetWaypoints();
        speed = waveConfig.GetMoveSpeed();
        loopMovement = waveConfig.IsMovementLooped();
        pathingOffset = waveConfig.PathOffsetPerSpawn * spawnIndex;
        pathingBehavior = waveConfig.PathingBehavior;
        transform.position = (Vector2)waypoints[waypointIndex].position + pathingOffset;
        StartCoroutine(InstantTurn());
    }

    private IEnumerator InstantTurn()
    {
        yield return null;
        float angle = Vector2.SignedAngle(Vector2.up,(
           waypoints[waypointIndex + 1].position + (Vector3)pathingOffset) - transform.position);
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = targetRotation;

        yield return null;
        IsMoving = true;
        yield return null;
    }
	
	// Update is called once per frame
	void Update () {


        if (!IsMoving)
        {
            return;
        }

        if (waypointIndex <= waypoints.Count - 1)
        {
            if ((pathingBehavior == PathingBehavior.Stationary) && (waypointIndex == waypoints.Count - 1))
            {
                MoveTowardsLastWaypointAndStop();
            }
            else
                MoveTowardsWaypoint();
        }
        else
        {
            if (pathingBehavior == PathingBehavior.Loop)
            {
                waypointIndex = 0;
                MoveForward();
            }
            if(pathingBehavior == PathingBehavior.Single)
                MoveForward();
        }
	}

    private void MoveTowardsWaypoint()
    {
        var targetPosition = (Vector2)waypoints[waypointIndex].transform.position + pathingOffset;
        var movementDelta = speed * Time.deltaTime;
        MoveForward();
        if (Vector2.Distance(transform.position, targetPosition) < 1f)
        {
            waypointIndex++;
        }
        else
        {
            TurnTowardsTarget(targetPosition);
        }

    }

    private void MoveTowardsLastWaypointAndStop()
    {
        var targetPosition = (Vector2)waypoints[waypointIndex].transform.position + pathingOffset;
        var movementDelta = speed * Time.deltaTime;
        var velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        var distance = Vector2.Distance(transform.position, targetPosition);

        //TurnTowardsTarget(targetPosition);
        ////Debug.Log("Smoothing");
        //if (duration == 0)
        //{
        //    duration = distance / speed;
        //    Debug.Log("duration: " + duration);
        //}

        //gameObject.GetComponent<Rigidbody2D>().velocity = -transform.up * Mathf.SmoothDamp(
        //    0, speed, ref currentVelocity, duration);

        if (velocity.magnitude > 0)
        {
            smoothVelocity = velocity;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        TurnTowardsTarget(targetPosition);
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition,
            ref smoothVelocity, Vector2.Distance(transform.position, targetPosition) / speed);
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            waypointIndex++;
        }
    }

    void MoveForward()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    void TurnTowardsTarget(Vector2 targetPos)
    {
        Vector2 pos = transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, targetPos - pos);
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
            (turnRate * Time.deltaTime) / Mathf.Abs(angle));
    }


}
