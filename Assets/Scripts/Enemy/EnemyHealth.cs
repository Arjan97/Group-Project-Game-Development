using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    //Health
    [SerializeField] private float currentHealth;
    public float maxHealth = 100f;
    //fireball
    public float fireballDamage = 30f;
    public float fireballDamageOvertime = 4f;
    private bool isOnFire = false;
    public List<int> burnTickTimers = new List<int>();

    //iceball
    public float iceballDamage = 25f;
    public float iceballSlowDuration = 6f;
    public float iceballSlowAmount = 0.5f;
    private bool isSlowed = false;
    //UI
    public Slider slider;
    public GameObject healthBarUI;
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
        StartCoroutine(GetsDamaged());
        Debug.Log("enemy taking damage, amount:" + damageAmount);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fireball")
        {
            Debug.Log("fireball detected, trying to set ablaze");
            if (!isOnFire)
            {
                isOnFire = true;
                ApplyBurn(5);
            }
        }
        if (other.gameObject.tag == "Iceball")
        {
            Debug.Log("iceball detected, trying to freeze");
            // Apply slowing effect to the enemy
            if (!isSlowed)
            {
                isSlowed = true;
                StartCoroutine(SlowEnemy());
            }
        }
    }
    IEnumerator GetsDamaged()
    {
        Color originalColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = originalColor;

    }

    public void ApplyBurn(int ticks)
    {
        if (burnTickTimers.Count <= 0) { 
            burnTickTimers.Add(ticks);
            StartCoroutine(OnFireEnemy());
        }
        else
        {
            burnTickTimers.Add(ticks);
        }
    }
    IEnumerator OnFireEnemy()
    {

        Color originalColor = GetComponent<Renderer>().material.color;
        isOnFire = true;
        TakeDamage(fireballDamage);

        while (burnTickTimers.Count > 0)
        {
            for(int i = 0; i < burnTickTimers.Count; i++)
            {
                burnTickTimers[i]--;
            }
            TakeDamage(fireballDamageOvertime);
            burnTickTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(0.75f);
        }
        isOnFire = false;
        GetComponent<Renderer>().material.color = originalColor;
    }
    IEnumerator SlowEnemy()
    {
        Color originalColor = GetComponent<Renderer>().material.color;
        TakeDamage(iceballDamage);
        yield return new WaitForSeconds(0.2f);
        // Apply slowing effect to the enemy
        float originalSpeed = GetComponent<EnemyMovement>().speed;
        GetComponent<EnemyMovement>().speed *= iceballSlowAmount;
        GetComponent<Renderer>().material.color = Color.blue;

        // Wait for the slow duration to elapse
        yield return new WaitForSeconds(iceballSlowDuration);
        GetComponent<Renderer>().material.color = originalColor;
        // Revert the slowing effect on the enemy
        GetComponent<EnemyMovement>().speed = originalSpeed;
        isSlowed = false;
    }
}
