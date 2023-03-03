using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
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
            playerHealth health = collision.gameObject.GetComponent<playerHealth>();
            health.TakeDamage(damageAmount);
        }
    }
}
