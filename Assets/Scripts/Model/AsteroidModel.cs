using System;
using UnityEngine;

public class AsteroidModel : BaseModel {

    public float speed;
    public GameObject explosion;
    public float tumble;

    public int baseScore = 10;
    protected int score;

    void Start()
    {
        Init();
        InitializeMovement();
        ChangeSize();
    }
    
    void OnDestroy()
    {
        CalculateScore();
    }

    void ChangeSize()
    {
        float scalar = UnityEngine.Random.Range(0.5f, 1.5f);
        transform.localScale += new Vector3(transform.localScale.x * scalar, transform.localScale.y * scalar, transform.localScale.z * scalar);
        rig.mass = rig.mass * scalar;
        speed = speed * (3.5f / scalar);
    }

    public void MakeSmallerThan(Rigidbody parentRigidbody)
    {
        transform.localScale = new Vector3(
            parentRigidbody.transform.localScale.x / 2.0f,
            parentRigidbody.transform.localScale.y / 2.0f,
            parentRigidbody.transform.localScale.z / 2.0f
        );
        rig.mass = rig.mass / 2.0f;
        speed = speed * 2.0f;
    }

    void InitializeMovement()
    {
        rig.angularVelocity = UnityEngine.Random.insideUnitSphere * tumble;

        rig.velocity = (UnityEngine.Random.onUnitSphere * speed);
        // Clamp velocity to the Y Plane
        rig.velocity = new Vector3(
            rig.velocity.x,
            0,
            rig.velocity.z
        );
    }

    void FixedUpdate()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        Boundary.UpdateRigidbodyPosition(rigidBody);
    }

    public int CalculateScore()
    {
        return (int)(baseScore * rig.transform.localScale.magnitude * (rig.velocity.magnitude / 2.0f));
    }

    public void SpawnReward()
    {
        // Spawn a reward on destroying this object
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("AsteroidModel"))
        {
            // Asteroids can collide and not explode
        }
        else if (other.name.Equals("PlayerShipModel"))
        {
            PlayerDestruction(other);
        }
        else if (other.name.Equals("BoltModel"))
        { 
            AsteroidDestruction(other);
        }
    }


    private void PlayerDestruction(Collider other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
        other.GetComponent<PlayerShipModel>().Death();
    }

    private void AsteroidDestruction(Collider other)
    {
        gameController.UpdateScore(CalculateScore());
        Destroy(other.gameObject);
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
        
        // Last Asteroid destroyed
        if(GameObject.FindObjectsOfType<AsteroidModel>().Length == 1)
        {
            gameController.StartSpawnWave();
        }
    }

}
