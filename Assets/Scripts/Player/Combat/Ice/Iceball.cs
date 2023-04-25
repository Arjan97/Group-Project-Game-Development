using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Iceball : Ability
{
    public GameObject iceballPrefab;
    public Transform iceballSpawnPoint;
    public float iceballSpeed = 10f;
    protected override void Start()
    {
        activationKey = KeyCode.Space;
        abilityName = "Iceball";
        base.Start();
    }
    protected override void Activate()
    {
        GameObject iceball = Instantiate(iceballPrefab, iceballSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = iceball.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * iceballSpeed;
        Destroy(iceball, 5f);
    }
}
