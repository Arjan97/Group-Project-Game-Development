using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Fireball : Ability
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 10f;

    protected override void Start()
    {
        activationKey = KeyCode.Space;
        abilityName = "Fireball";
        base.Start();
    }
    protected override void Activate()
    {
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * fireballSpeed;
        Destroy(fireball, 5f);
    }
}
