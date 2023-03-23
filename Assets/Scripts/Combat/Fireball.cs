using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 10f;
    public float fireballDamage = 10f;
    public float fireballCooldownTime = 3f;

    private bool canShoot = true;
    private float fireballCooldownTimer;
    private TextMeshProUGUI cooldownText;

    void Start()
    {
        cooldownText = GameObject.Find("CooldownText").GetComponent<TextMeshProUGUI>();
        cooldownText.text = "";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            FireFireball();
            canShoot = false;
            fireballCooldownTimer = fireballCooldownTime;
        }

        if (!canShoot)
        {
            fireballCooldownTimer -= Time.deltaTime;
            cooldownText.text = "Cooldown: " + fireballCooldownTimer.ToString("F1") + "s";

            if (fireballCooldownTimer <= 0f)
            {
                canShoot = true;
                cooldownText.text = "";
            }
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
