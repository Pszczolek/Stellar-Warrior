using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour {

    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] EnemyDamageable myDamageable;
    [SerializeField] Image healthBarImage;
	// Use this for initialization
	void Start () {
        maxHealth = myDamageable.Health;
        currentHealth = maxHealth;
	}
	
    public void UpdateDisplay(int newHealth)
    {
        currentHealth = newHealth;
        healthBarImage.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    private void Update()
    {
        gameObject.transform.rotation = Quaternion.identity;
    }

}
