using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    public float radius;
    public float damage;
    public float groundPoundCooldownTime;
    public float trembleDuration;
    public float trembleMagnitude;
    private AbilityManager abilityManager;

    void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && abilityManager.CanUseAbility("GroundPound"))
        {
            abilityManager.StartAbilityCooldown(groundPoundCooldownTime);
            GroundPounder();
        }

        radius = AbilityManager.instance.GetGroundPoundRadius();
        damage = AbilityManager.instance.GetGroundPoundDamage();
        groundPoundCooldownTime = AbilityManager.instance.GetGroundPoundCooldown();
        trembleDuration = AbilityManager.instance.GetGroundPoundTrembleDuration();
        trembleMagnitude = AbilityManager.instance.GetGroundPoundTrembleMagnitude();
    }

    void GroundPounder()
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
                    enemy.ApplyTrembleEffect(trembleDuration, trembleMagnitude);
                }
            }
        }
    }
}
