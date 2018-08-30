using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class Boundary
{
    public static float xMin = 0.0f, xMax = 0.0f, zMin = 0.0f, zMax = 0.0f, buffer = 0.0f;

    static float GetMinXBounds()
    {
        return zMin - buffer;
    }

    static float GetMaxXBounds()
    {
        return xMax + buffer;
    }

    static float GetMinZBounds()
    {
        return zMin - buffer;
    }

    static float GetMaxZBounds()
    {
        return zMax + buffer;
    }

    public static void UpdateRigidbodyPosition(Rigidbody rig)
    {
        if (rig.position.x < GetMinXBounds())
        {
            rig.position = new Vector3(
                GetMaxXBounds(),
                0.0f,
                rig.position.z
            );
        }

        if (rig.position.x > GetMaxXBounds())
        {
            rig.position = new Vector3(
                GetMinXBounds(),
                0.0f,
                rig.position.z
            );
        }

        if (rig.position.z < GetMinZBounds())
        {
            rig.position = new Vector3(
                rig.position.x,
                0.0f,
                GetMaxZBounds()
            );
        }

        if (rig.position.z > GetMaxZBounds())
        {
            rig.position = new Vector3(
                rig.position.x,
                0.0f,
                GetMinZBounds()
            );
        }
    }
}

public class PlayerShipController : MonoBehaviour {

    public float speed;
    public float tilt;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float rotationFactor;
    public float thrust;
    public float turnSpeed = 50f;
    public float leftConstraint = 0.0f;
    public float rightConstraint = 960.0f;
    public float maxSpeed = 10.0f;

    private float nextFire;

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
        }
    }

    /**
     * Automatically called by Unity
     */
    void FixedUpdate()
    {
        Rigidbody rig = GetComponent<Rigidbody>();

        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal < 0)
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }

        if (moveHorizontal > 0)
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        if (moveVertical > 0)
        {
            rig.AddRelativeForce(
                Vector3.forward * moveVertical * Time.deltaTime * thrust
            );

            if(rig.velocity.magnitude > maxSpeed)
            {
                rig.velocity = rig.velocity.normalized * maxSpeed;
            }
        }

        Boundary.UpdateRigidbodyPosition(rig);
    }
}
