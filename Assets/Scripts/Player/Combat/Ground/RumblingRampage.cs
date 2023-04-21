using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumblingRampage : MonoBehaviour
{
    public float radius;
    public float damage;
    public float knockbackForce;
    public float cooldownTime;

    private AbilityManager abilityManager;

    private void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && abilityManager.CanUseAbility("RumblingRampage"))
        {
            abilityManager.StartAbilityCooldown(cooldownTime);
            Rumble();
        }
        radius = AbilityManager.instance.GetRumblingRampageRadius();
        damage = AbilityManager.instance.GetRumblingRampageDamage();
        knockbackForce = AbilityManager.instance.GetRumblingRampageKnockBackForce();
        cooldownTime = AbilityManager.instance.GetRumblingRampageCooldown();
    }

    private void Rumble()
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
