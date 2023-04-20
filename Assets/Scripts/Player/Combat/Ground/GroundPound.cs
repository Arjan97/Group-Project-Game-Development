using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    public float radius;
    public float damage;
    public float groundPoundCooldownTime;
    public float trembleDuration;
    private AbilityManager abilityManager;

    void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && abilityManager.CanUseAbility("GroundPound"))
        {
            Debug.Log("using ground pound");
            abilityManager.StartAbilityCooldown(groundPoundCooldownTime);
            GroundPounder();
        }

        radius = AbilityManager.instance.GetGroundPoundRadius();
        damage = AbilityManager.instance.GetGroundPoundDamage();
        groundPoundCooldownTime = AbilityManager.instance.GetGroundPoundCooldown();
        trembleDuration = AbilityManager.instance.GetGroundPoundTrembleDuration();
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
                    enemy.ApplyTrembleEffect(trembleDuration);
                }
            }
        }
    }
}
