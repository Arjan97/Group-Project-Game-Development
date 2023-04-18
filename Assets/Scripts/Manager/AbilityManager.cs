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
    {"GroundPound", false },
    { "Iceball", false },
    { "Fireball", false }};
    // Ability trees
    public AbilityTree groundTree;
    public AbilityTree iceTree;
    public AbilityTree fireTree;
    public AbilityTree playerAbilityTree;

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
    public float groundPoundTrembleMagnitude { get; private set; }

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

        groundPoundDamage= 10f;
        groundPoundRadius= 1;
        groundPoundCooldown = 8f;

        // Create ability trees and add abilities
        groundTree = new AbilityTree("Ground");
        groundTree.AddAbility("GroundSlam");

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
        return abilityCooldownTimer <= 0f && unlockedAbilities[abilityName] && playerAbilityTree.IsAbilityInActiveTree(abilityName);
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

    public int GetGroundPoundTrembleDuration()
    {
        return groundPoundTrembleDuration;
    }

    public float GetGroundPoundTrembleMagnitude()
    {
        return groundPoundTrembleMagnitude;
    }
}
