using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftStride : Ability
{
    protected override void Start()
    {
        activationKey = KeyCode.X;
        abilityName = "SwiftStride";
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

        // Apply speed boost to player
        PlayerMovement2 player = GetComponent<PlayerMovement2>();
        player.ApplySpeedBoost(duration, boostAmount);
    }
}
