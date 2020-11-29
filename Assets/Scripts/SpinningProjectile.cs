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
        if(myRigidBody != null)
        {
            myRigidBody.rotation += rotationPerSecond * Time.deltaTime;
        }
        else
        {
            transform.Rotate(0, 0, rotationPerSecond * Time.deltaTime);
        }

    }
}
