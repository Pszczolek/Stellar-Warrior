using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedKillsDisplay : MonoBehaviour {

    [SerializeField] Text myText;
    [SerializeField] Level levelInfo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        myText.text = levelInfo.EnemiesDestroyed.ToString() + "/" +
            levelInfo.EnemiesSpawned.ToString();
    }
}
