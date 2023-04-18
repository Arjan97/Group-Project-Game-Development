using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    public float radius;
    public float damage;
    public float groundslamCooldownTime;

    private AbilityManager abilityManager;

    void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && abilityManager.CanUseAbility("GroundSlam"))
        {
            abilityManager.StartAbilityCooldown(groundslamCooldownTime);
            GroundSlammer();
        }

        radius = AbilityManager.instance.GetGroundSlamRadius();
        damage = AbilityManager.instance.GetGroundSlamDamage();
        groundslamCooldownTime = AbilityManager.instance.GetGroundSlamCooldown();
    }

    void GroundSlammer()
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
