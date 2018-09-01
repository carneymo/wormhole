using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {

    public float tumble;

	// Use this for initialization
	void Start ()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();

        rigidBody.angularVelocity = Random.insideUnitSphere * tumble;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
