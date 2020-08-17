using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningProjectile : MonoBehaviour {

    [SerializeField] float rotationPerSecond;
    [SerializeField] Rigidbody2D myRigidBody;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        Rotate();
	}

    private void Rotate()
    {
        myRigidBody.rotation += rotationPerSecond * Time.deltaTime;
    }
}
