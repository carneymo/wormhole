using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody rig;
    public float speed;
    public bool randomDirection;
    
    // Use this for initialization
    void Start()
    {
        Rigidbody rig = GetComponent<Rigidbody>();

        if (randomDirection)
        {
            rig.velocity = (Random.onUnitSphere * speed);
            rig.velocity = new Vector3(
                rig.velocity.x,
                0,
                rig.velocity.z
            );
        }
        else
        {
            rig.velocity = transform.forward * speed;
        }
    }

    void FixedUpdate()
    {
        Rigidbody rig = GetComponent<Rigidbody>();
        Boundary.UpdateRigidbodyPosition(rig);
    }
}