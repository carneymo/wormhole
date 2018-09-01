using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltModel : BaseModel {

    public GameObject explosion;
    public float speed;
    public float lifetime = 1.1f;

    void Start()
    {
        Init();
        InitializeMovement();
        Destroy(this.gameObject, lifetime);
    }

    void InitializeMovement()
    {
        rig.velocity = transform.forward * speed;
    }

    void FixedUpdate()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        Boundary.UpdateRigidbodyPosition(rigidBody);
    }
}
