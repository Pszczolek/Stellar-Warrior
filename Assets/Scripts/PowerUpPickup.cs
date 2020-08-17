using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour {

    [SerializeField] PowerUp powerUp;
    [SerializeField] int inventorySlotNumber;
    [SerializeField] float velocity = 3;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] GameEvent pickupEvent;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = Vector2.down * velocity;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) { return; }
        GetComponent<Collider2D>().enabled = false;
        Pickup(collision.gameObject);
    }

    private void Pickup(GameObject owner)
    {
        if (powerUp.useOnPickup)
        {
            powerUp.Activate(owner);
        }
        else
        {
            inventory.AddItem(powerUp, inventorySlotNumber);
        }
        pickupEvent.Raise();
        DestroyPickup();
    }

    private void DestroyPickup()
    {
        Destroy(gameObject);
    }
}
