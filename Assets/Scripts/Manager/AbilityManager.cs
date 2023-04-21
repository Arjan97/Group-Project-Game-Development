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
    public int upgradePoints = 100;
    //Dictionaries
    private Dictionary<string, bool> unlockedAbilities = new Dictionary<string, bool> {
    { "GroundSlam", false },
    { "GroundPound", false },
    { "ShockSlam", false },
    { "SwiftStride", false },
    { "Iceball", false },
    { "Fireball", false }};
    // Ability trees
    public AbilityTree groundTree;
    public AbilityTree iceTree;
    public AbilityTree fireTree;
    public AbilityTree playerAbilityTree;
    // Ability selection
    public List<string> selectedAbilities = new List<string>();
    public int maxSelectedAbilities = 2;
    // Groundslam ability
    public int groundSlamDamageLevel { get; set; }
    public int groundSlamRadiusLevel { get; set; }
    public int groundSlamCooldownLevel { get; set; }
    public float groundSlamDamage { get; private set; }
    public float groundSlamRadius { get; private set; }
    public float groundSlamCooldown { get; private set; }

    //GroundPound abil
    public int groundPoundDamageLevel { get; set; }
    public int groundPoundRadiusLevel { get; set; }
    public int groundPoundCooldownLevel { get; set; }
    public int groundPoundTrembleDurationLevel { get; set; }
    public float groundPoundDamage { get; private set; }
    public float groundPoundRadius { get; private set; }
    public float groundPoundCooldown { get; private set; }
    public int groundPoundTrembleDuration { get; private set; }

    //shockslam abil
    public int shockSlamDamageLevel { get; set; }
    public int shockSlamRadiusLevel { get; set; }
    public int shockSlamCooldownLevel { get; set; }
    public int shockSlamStunDurationLevel { get; set; }
    public int shockSlamKnockbackForceLevel { get; set; }
    public float shockSlamDamage { get; private set; }
    public float shockSlamRadius { get; private set; }
    public float shockSlamCooldown { get; private set; }
    public float shockSlamStunDuration { get; private set; }
    public float shockSlamKnockbackForce { get; private set; }

    //swiftstride abil 
    public int swiftStrideDamageLevel { get; set; }
    public int swiftStrideRadiusLevel { get; set; }
    public int swiftStrideCooldownLevel { get; set; }
    public int swiftStrideSpeedBoostDurationLevel { get; set; }
    public int swiftStrideSpeedBoostAmountLevel { get; set; }
    public float swiftStrideDamage { get; private set; }
    public float swiftStrideRadius { get; private set; }
    public float swiftStrideCooldown { get; private set; }
    public float swiftStrideSpeedBoostDuration { get; private set; }
    public float swiftStrideSpeedBoostAmount { get; private set; }

    // UI
    public AbilityUI abilityUpgradeUI;
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
        playerAbilityTree = groundTree;

        groundSlamDamageLevel = 1;
        groundSlamRadiusLevel = 1;
        groundSlamCooldownLevel = 1;

        groundSlamDamage = 30f;
        groundSlamRadius = 2f;
        groundSlamCooldown = 4f;

        groundPoundCooldownLevel= 1;
        groundPoundDamageLevel= 1;
        groundPoundRadiusLevel= 1;
        groundPoundTrembleDurationLevel= 1;

        groundPoundDamage= 15f;
        groundPoundRadius= 4;
        groundPoundCooldown = 10f;
        groundPoundTrembleDuration = 5;

        shockSlamDamage = 20f;
        shockSlamRadius = 3;
        shockSlamCooldown = 8;

        swiftStrideDamageLevel = 1;
        swiftStrideRadiusLevel=1;
        swiftStrideCooldownLevel = 1;
        swiftStrideSpeedBoostAmountLevel = 1;
        swiftStrideSpeedBoostDurationLevel= 1;
        swiftStrideCooldown = 6;
        swiftStrideDamage = 20;
        swiftStrideRadius = 5;
        swiftStrideSpeedBoostAmount = 20; 
        swiftStrideSpeedBoostDuration=3;

        // Create ability trees and add abilities
        groundTree = new AbilityTree("Ground");
        groundTree.AddAbility("GroundSlam");
        groundTree.AddAbility("GroundPound");
        groundTree.AddAbility("ShockSlam");
        groundTree.AddAbility("SwiftStride");

        iceTree = new AbilityTree("Ice");
        iceTree.AddAbility("Iceball");

        fireTree = new AbilityTree("Fire");
        fireTree.AddAbility("Fireball");
    }

    void Update()
    {
        CoolDown();
        playerAbilityTree = GetActiveTree();

    }

    public void CoolDown()
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
    public void SetActiveAbilityTree(AbilityTree abilityTree)
    {
        playerAbilityTree = abilityTree;
    }

    public AbilityTree GetActiveTree()
    {
        return playerAbilityTree;
    }
    public bool CanUseAbility(string abilityName)
    {
        return abilityCooldownTimer <= 0f && unlockedAbilities[abilityName] && playerAbilityTree.IsAbilityInActiveTree(abilityName) && selectedAbilities.Contains(abilityName);
    }
    public bool IsAbilityUnlocked(string abilityName)
    {
        bool isUnlocked;
        if (unlockedAbilities.TryGetValue(abilityName, out isUnlocked))
        {
            return isUnlocked;
        }
        return false; // if the ability name is not found in the dictionary, assume it's not unlocked
    }
    public void UnlockAbility(string abilityName, int cost)
    {
        if (unlockedAbilities.ContainsKey(abilityName))
        {
            if (CanAfford(cost))
            {
                SpendPoints(cost);
                unlockedAbilities[abilityName] = true;

                Debug.Log("ability: " + abilityName + " unlocked");
            }
            else
            {
                Debug.LogWarning("Insufficient points to unlock ability: " + abilityName);
            }
        }
        else
        {
            Debug.LogWarning("Attempted to unlock unknown ability: " + abilityName);
        }
    }
    //Cooldowntimer 
    public void StartAbilityCooldown(float cooldownTime)
    {
        abilityCooldownTimer = cooldownTime;
        cooldownText.text = "Cooldown: " + abilityCooldownTimer.ToString("F1") + "s";
    }
    public bool CanAfford(int cost)
    {
        return upgradePoints >= cost;
    }

    public void SpendPoints(int cost)
    {
        upgradePoints -= cost;
    }
    public void SelectAbility(string abilityName)
    {
        if (!selectedAbilities.Contains(abilityName))
        {
            if (selectedAbilities.Count < maxSelectedAbilities)
            {
                selectedAbilities.Add(abilityName);
                Debug.Log("Selected ability: " + abilityName);
            }
            else
            {
                Debug.Log("Cannot select more than " + maxSelectedAbilities + " abilities");
            }
        }
        else
        {
            selectedAbilities.Remove(abilityName);
            Debug.Log("Deselected ability: " + abilityName);
        }
    }

    //Groundslam ability upgrades

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

    //groundpound 
    public float GetGroundPoundDamage()
    {
        return groundPoundDamage;
    }

    public float GetGroundPoundRadius()
    {
        return groundPoundRadius;
    }

    public float GetGroundPoundCooldown()
    {
        return groundPoundCooldown;
    }

    public float GetGroundPoundTrembleDuration()
    {
        return groundPoundTrembleDuration;
    }

    //shockslam

    public float GetShockSlamRadius() {
        return shockSlamRadius;
    }
    public float GetShockSlamDamage() {
    return shockSlamDamage;
    }
    public float GetShockSlamKnockbackForce()
    {
return shockSlamKnockbackForce;
    }
    public float GetShockSlamStunDuration()
    {
        return shockSlamStunDuration;
    }
    public float GetShockSlamCooldown()
    {
        return shockSlamCooldown;
    }

    //swiftstride
    public float GetSwiftStrideRadius()
    {
        return swiftStrideRadius;
    }
    public float GetSwiftStrideCooldown()
    {
        return swiftStrideCooldown;
    }
    public float GetSwiftStrideDamage()
    {
        return swiftStrideDamage;
    }
    public float GetSwiftStrideSpeedBoostDuration()
    {
        return swiftStrideSpeedBoostDuration;
    }
    public float GetSwiftStrideSpeedBoostAmount()
    {
        return swiftStrideSpeedBoostAmount;
    }

}
