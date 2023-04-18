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
    private bool isInvincible = false;
    private float invincibilityTime = 1f;
    private float invincibilityTimer = 0f;
    public Color normalColor;
    public Color invincibleColor;
    private void Start()
    {
        //start with maximum health & healthbar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        // Assign the normal color to the child object's renderer component
        Transform child = transform.Find("Ch45");
        if (child != null)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = normalColor;
            }
        }
    }

    void Update()
    {
        healthBar.SetHealth(currentHealth);
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(takeDamage);
        }
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                // Change the child object's color back to normal
                Transform child = transform.Find("Ch45");
                if (child != null)
                {
                    Renderer renderer = child.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = normalColor;
                    }
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            // Subtract the damage from the player's health
            currentHealth -= damage;
            Debug.Log("Player taking damage! Amount: " + damage);

            // Check if the player is dead
            if (currentHealth <= 0)
            {
                Die();
            }
            // Activate invincibility
            isInvincible = true;
            invincibilityTimer = invincibilityTime;
            // Change the child object's color
            Transform child = transform.Find("Ch45");
            if (child != null)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = invincibleColor;
                }
            }
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
