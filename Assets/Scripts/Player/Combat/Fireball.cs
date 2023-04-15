using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 10f;
    public float fireballCooldownTime = 3f;

    private AbilityManager abilityManager;

    void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && abilityManager.CanUseAbility())
        {
            FireFireball();
            abilityManager.StartAbilityCooldown(fireballCooldownTime);
        }
    }
    void FireFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * fireballSpeed;
        Destroy(fireball, 5f);
    }
}
