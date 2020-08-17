using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    [SerializeField] Slider slider;
    [SerializeField] IntVariable maxHealth;
    [SerializeField] IntVariable health;
	// Use this for initialization
	void Start () {
        UpdateDisplay();
    }
	
    public void UpdateDisplay()
    {
        slider.value = (float)(health.Value) / (float)(maxHealth.Value);
    }
}
