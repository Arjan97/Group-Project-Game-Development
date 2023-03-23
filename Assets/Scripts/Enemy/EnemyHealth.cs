using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float fireballDamage = 50f;
    public Slider slider;
    public GameObject healthBarUI;
    [SerializeField] private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        slider.value = CalculateHealth();
    }

    private void Update()
    {
        slider.value = CalculateHealth();
        if (currentHealth < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    float CalculateHealth()
    {
       return currentHealth / maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("enemy taking damage, amount:" + damageAmount);
        if (currentHealth <= 0f)
        {
            //Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fireball")
        {
            Debug.Log("fireball detected, trying to damage");

            //Fireball fireball = other.gameObject.GetComponent<Fireball>();
            TakeDamage(fireballDamage);
            //Destroy(gameObject);
        }
    }

    void Die()
    {
        // Here you can add code for what happens when the enemy dies,
        // such as playing an animation or spawning loot.
        Destroy(gameObject);
    }
}
