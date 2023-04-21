using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftStride : MonoBehaviour
{
    public float damage;
    public float radius;
    public float speedBoostDuration;
    public float speedBoostAmount;
    public float swiftStrideCooldownTime;

    private AbilityManager abilityManager;

    void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && abilityManager.CanUseAbility("SwiftStride"))
        {
            abilityManager.StartAbilityCooldown(swiftStrideCooldownTime);
            SwiftStrider();
        }
        radius = AbilityManager.instance.GetSwiftStrideRadius();
        damage = AbilityManager.instance.GetSwiftStrideDamage();
        speedBoostDuration = AbilityManager.instance.GetSwiftStrideSpeedBoostDuration();
        speedBoostAmount = AbilityManager.instance.GetSwiftStrideSpeedBoostAmount();
        swiftStrideCooldownTime = AbilityManager.instance.GetSwiftStrideCooldown();
    }

    void SwiftStrider()
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
        player.ApplySpeedBoost(speedBoostDuration, speedBoostAmount);
    }
}
