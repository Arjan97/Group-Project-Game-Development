using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public PlayerSpawn respawnPoint;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Subtract the damage from the player's health
        currentHealth -= damage;
        Debug.Log("Player taking damage!");
        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
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
