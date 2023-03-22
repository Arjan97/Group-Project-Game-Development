using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //[SerializeField]
    public PlayerHealth playerHealth;
    public int healthGain = -10;
    public float rotateSpeed = 1000f;

    private void Awake()
    {
        //Debug.Log("current health = " + PlayerHealth.currentHealth);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Debug.Log(playerHealth);
    }

    private void Update()
    {
        transform.Rotate(0f, 10f* rotateSpeed * Time.deltaTime, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerHealth.currentHealth < 100)
            {
                Destroy(gameObject);
                //PlayerHealth.currentHealth = PlayerHealth.currentHealth + healthGain;
                playerHealth.GainHealth(25);
                Debug.Log("Gained " + healthGain + " health");
                //Debug.Log("total health = " + PlayerHealth.currentHealth);
            }
        }
    }
}
