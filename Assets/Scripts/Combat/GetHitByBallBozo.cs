using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitByBallBozo : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public int currentHealth = 10;
    public int takeDamage = 10;

    public void Start()
    {
        
    }
    private void Update()
    {
        Debug.Log(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        // Subtract the damage from the player's health
        currentHealth -= damage;
        Debug.Log("Super taking damage!");

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fireball")
        {
            Debug.Log("fireball detected, trying to set ablaze");
            TakeDamage(10);
        }
        if (other.gameObject.tag == "Iceball")
        {
            Debug.Log("iceball detected, trying to freeze");
            TakeDamage(10);
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
