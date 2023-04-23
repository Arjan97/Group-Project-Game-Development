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
    private float maxAbilityLevel;
    public KeyCode activationKey;
    public string abilityName;
    protected virtual void Start()
    {
        maxAbilityLevel = 5;

        AbilityManager.instance.SetValues("GroundSlam", 40, 4, 5, 1, 0, 0, 0);
        AbilityManager.instance.SetValues("ShockSlam", 20, 4, 5, 1, 3, 5, 0);
        AbilityManager.instance.SetValues("GroundPound", 40, 5, 8, 1, 5, 0, 0);
        AbilityManager.instance.SetValues("RumblingRampage", 60, 4, 8, 1, 0, 20, 0);
        AbilityManager.instance.SetValues("SwiftStride", 20, 5, 8, 1, 4, 0, 10);

        duration = AbilityManager.instance.GetDuration(abilityName);
        knockbackForce= AbilityManager.instance.GetKnockBackForce(abilityName);
        cooldownTime = AbilityManager.instance.GetCooldown(abilityName);
        damage = AbilityManager.instance.GetDamage(abilityName);
        radius= AbilityManager.instance.GetRadius(abilityName);
        level = AbilityManager.instance.GetLevel(abilityName);
        boostAmount = AbilityManager.instance.GetBoostAmount(abilityName);
    }

    protected virtual void Update()
    {
        Use();
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
    public void UpgradeStat(string statName, float upgradeAmount)
    {
        if (level < maxAbilityLevel)
        {
            switch (statName)
            {
                case "CooldownTime":
                    cooldownTime -= upgradeAmount;
                    break;
                case "Damage":
                    damage += upgradeAmount;
                    break;
                case "Radius":
                    radius += upgradeAmount;
                    break;
                default:
                    Debug.LogWarning("Invalid stat name: " + statName);
                    break;
            }

            level++;
        }
    }
    public string GetAbility()
    {
        return abilityName;
    }
    protected abstract void Activate();
}
