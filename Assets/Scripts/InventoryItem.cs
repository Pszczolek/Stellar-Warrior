using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : ScriptableObject {
    public Sprite icon;
    public bool singleUse;
    public float cooldown;

    public abstract void Activate(GameObject owner);
}
