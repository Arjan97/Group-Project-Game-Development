using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : Ability
{
    protected override void Start()
    {
        activationKey = KeyCode.Z; 
        abilityName = "GroundPound";
        base.Start();
    }
    protected override void Activate()
    {
        // Detect nearby enemies and damage them
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);

                // Apply tremble effect to enemy
                EnemyMovement enemy = collider.GetComponent<EnemyMovement>();
                if (enemy != null)
                {
                    enemy.ApplyTrembleEffect(duration);
                }
            }
        }
    }
}
