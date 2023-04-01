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

    public bool canShoot = true;
    private float iceballCooldownTimer;
    private TextMeshProUGUI cooldownText;

    void Start()
    {
        cooldownText = GameObject.Find("CooldownText").GetComponent<TextMeshProUGUI>();
        cooldownText.text = "";
    }

    void Update()
    {
        if (!canShoot)
        {
            iceballCooldownTimer -= Time.deltaTime;
            cooldownText.text = "Cooldown: " + iceballCooldownTimer.ToString("F1") + "s";

            if (iceballCooldownTimer <= 0f)
            {
                canShoot = true;
                cooldownText.text = "";
            }
        }
    }

    public void FireIceball()
    {
        GameObject iceball = Instantiate(iceballPrefab, iceballSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = iceball.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * iceballSpeed;
        Destroy(iceball, 5f);
        iceballCooldownTimer = iceballCooldownTime;
        canShoot = false;
    }
}
