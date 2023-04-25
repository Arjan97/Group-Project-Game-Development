using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockSlam : Ability
{
    protected override void Start()
    {
        activationKey = KeyCode.C;
        abilityName = "ShockSlam";
        base.Start();
    }
    protected override void Activate()
    {
        // Detect nearby enemies and damage them
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            EnemyMovement enemyMovement = collider.GetComponent<EnemyMovement>();
            if (enemyHealth != null && enemyMovement != null)
            {
                // Damage enemy
                enemyHealth.TakeDamage(damage);

                // Apply knockback force and stun to enemy
                enemyMovement.ApplyKnockbackEffect(knockbackForce);
                enemyMovement.ApplyStunEffect(duration);
            }
        }
    }
}