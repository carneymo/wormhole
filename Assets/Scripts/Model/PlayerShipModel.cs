using UnityEngine;

public class PlayerShipModel : BaseModel {

    public Vector3 speed;
    public float tilt;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float thrust;
    public float turnSpeed = 50f;
    public float maxSpeed = 10.0f;
    private float nextFire;

    public GameObject explosion;

    private void Start()
    {
        Init();
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

    /**
     * Automatically called by Unity
     */
    void FixedUpdate()
    {
        Rigidbody rig = GetComponent<Rigidbody>();

        float acceleration = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");
        
        rig.AddTorque(
            0, 
            rotation * turnSpeed, 
            0
        );

        if (acceleration > 0)
        {
            rig.AddForce(
                transform.forward * acceleration * thrust
            );
        }

        if (rig.velocity.magnitude > maxSpeed)
        {
            rig.velocity = rig.velocity.normalized * maxSpeed;
        }

        rig = Boundary.UpdateRigidbodyPosition(rig);
    }

    public void Death()
    {
        Explode();
        StartCoroutine(gameController.DisplayGameOver());
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

}
