using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryButton : MonoBehaviour {

    [SerializeField] Image image;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Button button;
    [SerializeField] InventoryItem item;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] int slotNumber;

    private void Start()
    {
        button.interactable = false;
        UpdateItemDisplay();
    }

    public void UpdateItemDisplay()
    {
        item = inventory.items[slotNumber];
        if (item != null)
        {
            image.sprite = item.icon;
            button.interactable = true;
        }

    }

    public void ActivateItem()
    {
        item.Activate(FindObjectOfType<Player>().gameObject);
        if (inventory.items[slotNumber].singleUse)
        {
            inventory.RemoveItem(slotNumber);
            item = null;
            button.interactable = false;
            image.sprite = defaultSprite;
        }

    }
}
