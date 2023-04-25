using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float cooldownTime;
    public float damage;
    public float radius;
    public float duration;
    public float knockbackForce;
    public float level;
    public float boostAmount;
    public KeyCode activationKey;
    public string abilityName;


    protected virtual void Start()
    {
        //Ground tree
        AbilityManager.instance.SetValues("GroundSlam", 40, 4, 5, 1, 0, 0, 0);
        AbilityManager.instance.SetValues("ShockSlam", 20, 4, 5, 1, 3, 5, 0);
        AbilityManager.instance.SetValues("GroundPound", 40, 5, 8, 1, 5, 0, 0);
        AbilityManager.instance.SetValues("RumblingRampage", 60, 4, 8, 1, 0, 20, 0);
        AbilityManager.instance.SetValues("SwiftStride", 20, 5, 8, 1, 4, 0, 10);
        //Ice tree
        AbilityManager.instance.SetValues("Fireball", 0, 0, 5, 1, 0, 0, 0);
        //Fire tree
        AbilityManager.instance.SetValues("Iceball", 0, 0, 5, 1, 0, 0, 0);
        //todo: add fire and ice tree ability stats
    }

    protected virtual void Update()
    {
        Use();
        GetStats();
    }
    public virtual void GetStats()
    {
        cooldownTime = AbilityManager.instance.GetCooldown(abilityName);
        damage = AbilityManager.instance.GetDamage(abilityName);
        duration = AbilityManager.instance.GetDuration(abilityName);
        level = AbilityManager.instance.GetLevel(abilityName);
        boostAmount = AbilityManager.instance.GetBoostAmount(abilityName);
        knockbackForce = AbilityManager.instance.GetKnockBackForce(abilityName);
        radius = AbilityManager.instance.GetRadius(abilityName);
    }
    public virtual void Use()
    {
        if (Input.GetKeyDown(activationKey) && AbilityManager.instance.CanUseAbility(abilityName))
        {
            Activate();
            AbilityManager.instance.StartAbilityCooldown(cooldownTime);
            Debug.Log("aha.. using ability" + abilityName);
        }
    }
    public string GetAbility()
    {
        return abilityName;
    }
    protected abstract void Activate();
}
