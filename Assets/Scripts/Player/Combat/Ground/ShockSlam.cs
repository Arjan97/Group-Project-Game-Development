using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockSlam : MonoBehaviour
{
    public float radius;
    public float damage;
    public float knockbackForce;
    public float stunDuration;
    public float shockSlamCooldownTime;
    private AbilityManager abilityManager;

    void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && abilityManager.CanUseAbility("ShockSlam"))
        {
            Debug.Log("using shock slam");
            abilityManager.StartAbilityCooldown(shockSlamCooldownTime);
            ShockSlammer();
        }

        radius = AbilityManager.instance.GetShockSlamRadius();
        damage = AbilityManager.instance.GetShockSlamDamage();
        knockbackForce = AbilityManager.instance.GetShockSlamKnockbackForce();
        stunDuration = AbilityManager.instance.GetShockSlamStunDuration();
        shockSlamCooldownTime = AbilityManager.instance.GetShockSlamCooldown();
    }

    void ShockSlammer()
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
                enemyMovement.ApplyStunEffect(stunDuration);
            }
        }
    }
}