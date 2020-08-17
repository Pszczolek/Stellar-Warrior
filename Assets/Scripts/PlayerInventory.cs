using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Inventory")]
public class PlayerInventory : ScriptableObject {

    public InventoryItem[] items = new InventoryItem[2];

    public void AddItem(InventoryItem newItem, int whichSlot)
    {
        items[whichSlot] = newItem;
    }

    public void RemoveItem(int whichSlot)
    {
        items[whichSlot] = null;
    }
    

}
