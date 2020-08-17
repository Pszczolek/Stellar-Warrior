using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    [SerializeField] Text scoreText;
    [SerializeField] GameSession gameSession;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateScoreDisplay();
	}

    void UpdateScoreDisplay()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
