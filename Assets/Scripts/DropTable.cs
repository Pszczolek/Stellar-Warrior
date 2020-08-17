using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop
{
    public GameObject item;
    public float dropChance;
}

[CreateAssetMenu (menuName = "DropTable")]
public class DropTable : ScriptableObject {

    public bool allowMultipleDrops = false;
    public ItemDrop[] possibleDrops;


    public void DropItems(Vector2 where)
    {
        for(int i = 0; i< possibleDrops.Length; i++)
        {
            if(Random.value < possibleDrops[i].dropChance)
            {
                Instantiate(possibleDrops[i].item, where, Quaternion.identity);
                if (!allowMultipleDrops) { return; }
            }
        }
    }

}
