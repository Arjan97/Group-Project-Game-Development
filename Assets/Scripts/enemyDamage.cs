using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount;
    // Start is called before the first frame update
    void Start()
    {
        damageAmount = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            health.TakeDamage(damageAmount);
        }
    }
}
