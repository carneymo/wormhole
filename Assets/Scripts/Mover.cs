using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody rig;
    public float speed;
    public float initTime;
    public float timeToLive = 3.0f;
    
    // Use this for initialization
    void Start()
    {
        Rigidbody rig = GetComponent<Rigidbody>();

        rig.velocity = transform.forward * speed;
        initTime = Time.time;
    }

    void FixedUpdate()
    {
        if (Time.time - initTime > timeToLive && rig.gameObject != null)
        {

        }
        else
        {
            Boundary.UpdateRigidbodyPosition(rig);
        }
    }
}