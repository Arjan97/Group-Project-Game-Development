using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class Ability : MonoBehaviour
{
    public float cooldownTime;
    public int level = 1;
    public int maxLevel = 5;
    public int upgradeCost = 10;
    private AbilityManager abilityManager;

    protected virtual void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }


    protected void StartAbilityCooldown()
    {
        abilityManager.StartAbilityCooldown(cooldownTime);
    }
    public abstract void Upgrade();
    public abstract void Activate();
}
