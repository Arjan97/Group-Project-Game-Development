using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : Ability
{
    protected override void Start()
    {
        activationKey = KeyCode.Space;
        abilityName = "GroundSlam";
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
            }
        }
    }
}
