using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperShoot : MonoBehaviour
{
    [SerializeField] Transform player;
    private Fireball fireball;

    private float fireballCooldownTime = 3f;

    private int ShootCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        fireball = GetComponent<Fireball>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootCounter++;
        transform.LookAt(player);

        if (ShootCounter >= fireballCooldownTime)
        {
            fireball.FireFireball();
            ShootCounter = 0;
        }
    }
}
