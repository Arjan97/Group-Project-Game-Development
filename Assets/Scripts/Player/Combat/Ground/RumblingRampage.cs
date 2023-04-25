using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumblingRampage : Ability
{
    protected override void Start()
    {
        activationKey = KeyCode.B;
        abilityName = "RumblingRampage";
        base.Start();
    }
    protected override void Activate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            EnemyMovement enemyMovement = collider.GetComponent<EnemyMovement>();
            if (enemyHealth != null && enemyMovement != null)
            {
                enemyHealth.TakeDamage(damage);
                enemyMovement.ApplyKnockbackEffect(knockbackForce);
            }
        }
    }
}
