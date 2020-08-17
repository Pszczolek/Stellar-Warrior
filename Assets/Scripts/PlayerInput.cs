using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : PlayerInputBase {

    [SerializeField] float playerSpeed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public override Vector2 ProcessInput()
    {
        var movementFactor = Time.deltaTime * playerSpeed;
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        return new Vector2(deltaX, deltaY) * movementFactor;
    }
}
