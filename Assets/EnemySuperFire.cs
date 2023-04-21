using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySuperFire : MonoBehaviour
{

    [SerializeField] Transform player;

    public GameObject prefabFireball;
    public Transform fireballSpawnPoint;
    private float fireballSpeed = 10f;
    private float fireballCooldownTime = 3f;

    int ShootCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootCounter++;
        transform.LookAt(player);

        if(ShootCounter >= fireballCooldownTime)
        {
            FireFireBall();
            ShootCounter = 0;
        }
    }

    void FireFireBall()
    {
        GameObject fireball = Instantiate(prefabFireball, fireballSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * fireballSpeed;
        Destroy(fireball, 5f);
    }
}
