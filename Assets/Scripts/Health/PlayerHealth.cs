using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public int maxHealth = 100;
    public int currentHealth;
    public int takeDamage = 10;
    public PlayerSpawn respawnPoint;
    public HealthBar healthBar;


    private void Start()
    {
        //start with maximum health & healthbar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(takeDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        // Subtract the damage from the player's health
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Player taking damage!");

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void GainHealth(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
        //Debug.Log("player Gained health of: " + heal);
    }

    private void Die()
    {
        // Handle player death
        Debug.Log("Player died!");
        // Respawn the player at the respawn point
        respawnPoint.RespawnPlayer(gameObject);

        // Reset the player's health
        currentHealth = maxHealth;
    }
}
