using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CooldownButton : MonoBehaviour
{

    [SerializeField] Image image;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Button button;
    [SerializeField] Text text;
    [SerializeField] InventoryItem item;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] int slotNumber;
    [SerializeField] float startingCooldown = 3;

    [SerializeField] float time;

    private void Start()
    {
        button.interactable = false;
        UpdateItemDisplay();
        if (startingCooldown > 0)
        {
            button.interactable = false;
            button.image.fillAmount = 0;
            text.text = startingCooldown.ToString("F0");
            time = 0;
            StartCoroutine(CooldownTimer(startingCooldown));
        }
    }

    public void UpdateItemDisplay()
    {
        if(item != inventory.items[slotNumber])
        {
            item = inventory.items[slotNumber];
            time = item.cooldown;
            image.sprite = item.icon;
            button.interactable = true;
        }
        //item = inventory.items[slotNumber];
        //if (item != null)
        //{
        //    image.sprite = item.icon;
        //    button.interactable = true;
        //}

    }

    public void ActivateItem()
    {
        var player = FindObjectOfType<Player>().gameObject;
        if (!player) return;
        item.Activate(player);
        float cooldownTime = inventory.items[slotNumber].cooldown;
        if (cooldownTime > 0)
        {
            button.interactable = false;
            button.image.fillAmount = 0;
            text.text = cooldownTime.ToString("F0");
            time = 0;
            StartCoroutine(CooldownTimer(cooldownTime));
        }

    }

    private IEnumerator CooldownTimer(float cooldownTime)
    {
        while(time < cooldownTime)
        {
            time += Time.deltaTime;
            button.image.fillAmount = time / cooldownTime;
            text.text = (cooldownTime - time).ToString("F0");
            yield return null;
        }
        button.image.fillAmount = 1;
        text.text = "";
        button.interactable = true;
        yield return null;
    }
}
