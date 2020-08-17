using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossBehaviour : MonoBehaviour {
    

    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] PrimaryWeaponType[] weapons;
    [SerializeField] float speed = 1f;
    [SerializeField] List<UnityEvent> bossEvents;

    private Coroutine[] weaponCoroutines;
    private bool[] weaponFiring;

    private int waypointIndex = 0;
    private int orderIndex = 0;
    private bool isWaiting = false;

	// Use this for initialization
	void Start () {
        weaponCoroutines = new Coroutine[weapons.Length];
        weaponFiring = new bool[weapons.Length];
        StartCoroutine(MainCoroutine());
	}
	
	// Update is called once per frame
	void Update () {



	}
    private void OnBecameInvisible()
    {
        StopAllFire();
    }

    private IEnumerator MainCoroutine() {

        while (true)
        {
            if (isWaiting) { yield return null; }
            else
            {
                //bossEvents[orderIndex].Invoke();
                orderIndex++;
                if (orderIndex >= bossEvents.Count) { orderIndex = 0; }
                yield return null;
            }

        }

    }

    public void MoveToNextWaypoint()
    {
        var targetPosition = waypoints[waypointIndex].transform.position;
        StartCoroutine(MovementCoroutine(waypoints[waypointIndex].position));
    }

    IEnumerator MovementCoroutine(Vector2 targetPosition)
    {
        while (Vector2.Distance(transform.position, targetPosition) > .1f)
        {
            Debug.Log("Coroutine running");
            float movementDelta = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementDelta);
            yield return null;
        }

        waypointIndex++;
        if(waypointIndex >= waypoints.Count) { waypointIndex = 0; }
        yield return null;
    }

    public void Wait(float time)
    {
        isWaiting = true;
        StartCoroutine(WaitCoroutine(time));
    }

    private IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        isWaiting = false;
    }

    public void FireWeapon(int weaponIndex)
    {
        if (!weaponFiring[weaponIndex])
        {
            weaponFiring[weaponIndex] = true;
            weaponCoroutines[weaponIndex] = StartCoroutine(weapons[weaponIndex].Fire(gameObject));
        }
    }

    public void StopFire(int weaponIndex)
    {
        if (weaponFiring[weaponIndex])
        {
            weaponFiring[weaponIndex] = false;
            StopCoroutine(weaponCoroutines[weaponIndex]);
        }
    }

    public void StopAllFire()
    {
        Debug.Log("Stop all fire");
        for(int i = 0; i < weapons.Length; i++)
        {
            StopFire(i);
        }
    }
}
