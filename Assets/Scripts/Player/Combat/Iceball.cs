using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Iceball : MonoBehaviour
{
    public GameObject iceballPrefab;
    public Transform iceballSpawnPoint;
    public float iceballSpeed = 10f;
    public float iceballCooldownTime = 3f;

    private AbilityManager abilityManager;


    void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && abilityManager.CanUseAbility("Iceball"))
        {
            FireIceball();
            abilityManager.StartAbilityCooldown(iceballCooldownTime);
        }
    }

    void FireIceball()
    {
        GameObject iceball = Instantiate(iceballPrefab, iceballSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = iceball.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * iceballSpeed;
        Destroy(iceball, 5f);
    }
}
