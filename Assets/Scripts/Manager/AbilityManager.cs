using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    // Instance doesn't destroy on loading
    public static AbilityManager instance;
    // General cooldown timer
    public TextMeshProUGUI cooldownText;
    private float abilityCooldownTimer = 0f;
    // Groundslam ability
    public int groundSlamDamageLevel { get; set; }
    public int groundSlamRadiusLevel { get; set; }
    public int groundSlamCooldownLevel { get; set; }
    public float groundSlamDamage { get; private set; }
    public float groundSlamRadius { get; private set; }
    public float groundSlamCooldown { get; private set; }

    public int upgradePoints = 100;

    // UI
    public AbilityUpgradeUI abilityUpgradeUI;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // Initialize default values
        groundSlamDamageLevel = 1;
        groundSlamRadiusLevel = 1;
        groundSlamCooldownLevel = 1;

        groundSlamDamage = 30f;
        groundSlamRadius = 2f;
        groundSlamCooldown = 4f;
    }

    void Update()
    {
        //Cooldowntimer
        if (abilityCooldownTimer > 0)
        {
            abilityCooldownTimer -= Time.deltaTime;
            cooldownText.text = "Cooldown: " + abilityCooldownTimer.ToString("F1") + "s";
        }
        else
        {
            cooldownText.text = "";
        }

    }

    //Cooldowntimer 
    public bool CanUseAbility()
    {
        return abilityCooldownTimer <= 0f;
    }
    //Cooldowntimer 
    public void StartAbilityCooldown(float cooldownTime)
    {
        abilityCooldownTimer = cooldownTime;
        cooldownText.text = "Cooldown: " + abilityCooldownTimer.ToString("F1") + "s";
    }

    //Groundslam ability upgrades
    public bool CanAfford(int cost)
    {
        return upgradePoints >= cost;
    }

    public void SpendPoints(int cost)
    {
        upgradePoints -= cost;
    }

    public void UpgradeGroundSlamDamage(float amount)
    {
        groundSlamDamage += amount;
    }

    public void UpgradeGroundSlamRadius(float amount)
    {
        groundSlamRadius += amount;
    }

    public void UpgradeGroundSlamCooldown(float amount)
    {
        groundSlamCooldown -= amount;
    }

    public float GetGroundSlamDamage()
    {
        return groundSlamDamage;
    }

    public float GetGroundSlamRadius()
    {
        return groundSlamRadius;
    }

    public float GetGroundSlamCooldown()
    {
        return groundSlamCooldown;
    }
}
