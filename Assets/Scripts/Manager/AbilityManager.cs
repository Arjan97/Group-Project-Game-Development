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
    { "RumblingRampage", false },
    { "Iceball", false },
    { "Fireball", false }};

    private Dictionary<string, float> damageValues = new Dictionary<string, float>();
    private Dictionary<string, float> radiusValues = new Dictionary<string, float>();
    private Dictionary<string, float> cooldownValues = new Dictionary<string, float>();
    private Dictionary<string, float> levelValues = new Dictionary<string, float>();
    private Dictionary<string, float> durationValues = new Dictionary<string, float>();
    private Dictionary<string, float> knockBackForceValues = new Dictionary<string, float>();
    private Dictionary<string, float> boostAmountValues = new Dictionary<string, float>();

    private int maxAbilityLevel;
    public float defaultDamage = 20f;
    public float defaultRadius = 3f;
    public float defaultCooldown = 5f;
    public float defaultLevel = 1;
    public float defaultDuration = 0;
    public float defaultKnockBackForce = 0;
    public float defaultBoostAmount = 0;
    // Ability trees
    public AbilityTree groundTree;
    public AbilityTree iceTree;
    public AbilityTree fireTree;
    public AbilityTree playerAbilityTree;
    // Ability selection
    public List<string> selectedAbilities = new List<string>();
    public int maxSelectedAbilities = 2;
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

        // Create ability trees and add abilities
        groundTree = new AbilityTree("Ground");
        groundTree.AddAbility("GroundSlam");
        groundTree.AddAbility("GroundPound");
        groundTree.AddAbility("ShockSlam");
        groundTree.AddAbility("SwiftStride");
        groundTree.AddAbility("RumblingRampage");

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
    public void SetDefaultValues(string abilityName)
    {
        damageValues[abilityName] = defaultDamage;
        radiusValues[abilityName] = defaultRadius;
        cooldownValues[abilityName] = defaultCooldown;
        levelValues[abilityName] = defaultLevel;
        durationValues[abilityName] = defaultDuration;
        knockBackForceValues[abilityName] = defaultKnockBackForce;
        boostAmountValues[abilityName] = defaultBoostAmount;
    }
    public void SetValues(string abilityName, float damage, float radius, float cooldown, float level, float duration, float knockBackForce, float boostAmount)
    {
        damageValues[abilityName] = damage;
        radiusValues[abilityName] = radius;
        cooldownValues[abilityName] = cooldown;
        levelValues[abilityName] = level;
        durationValues[abilityName] = duration;
        knockBackForceValues[abilityName] = knockBackForce;
        boostAmountValues[abilityName] = boostAmount;
    }
    public void UpgradeAbility(string abilityName, Ability abil)
    {
        if (upgradePoints > 0 && unlockedAbilities[abilityName])
        {
            abilityName = abil.GetAbility();
            if (abilityName != null)
            {
                if (abil.level < maxAbilityLevel)
                {
                    // Call the UpgradeStat function with the desired stat and amount to upgrade
                    abil.UpgradeStat("Damage", 10f);
                    abil.UpgradeStat("Radius", 1f);
                    abil.UpgradeStat("CooldownTime", -1f);

                    upgradePoints--;
                    Debug.Log("Upgraded " + abilityName + " damage to level " + abil.level);
                }
                else
                {
                    Debug.Log(abilityName + " is already at max level");
                }
            }
            else
            {
                Debug.Log("Invalid ability name");
            }
        }
        else if (!unlockedAbilities[abilityName])
        {
            Debug.Log("You have not unlocked " + abilityName + " yet");
        }
        else
        {
            Debug.Log("You do not have enough upgrade points");
        }
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
    // Get the values for a specific ability
    public float GetDamage(string abilityName)
    {
        if (damageValues.ContainsKey(abilityName))
        {
            return damageValues[abilityName];
        }
        else
        {
            Debug.Log("Ability not found: " + abilityName);
            return defaultDamage;
        }
    }
    public float GetRadius(string abilityName)
    {
        if (radiusValues.ContainsKey(abilityName))
        {
            return radiusValues[abilityName];
        }
        else
        {
            Debug.Log("Ability not found: " + abilityName);
            return defaultRadius;
        }
    }
    public float GetCooldown(string abilityName)
    {
        if (cooldownValues.ContainsKey(abilityName))
        {
            return cooldownValues[abilityName];
        }
        else
        {
            Debug.Log("Ability not found: " + abilityName);
            return defaultCooldown;
        }
    }
    public float GetLevel(string abilityName)
    {
        if (levelValues.ContainsKey(abilityName))
        {
            return levelValues[abilityName];
        }
        else
        {
            Debug.Log("Ability not found: " + abilityName);
            return defaultLevel;
        }
    }
    public float GetDuration(string abilityName)
    {
        if (durationValues.ContainsKey(abilityName))
        {
            return durationValues[abilityName];
        }
        else
        {
            Debug.Log("Ability not found: " + abilityName);
            return defaultDuration;
        }
    }
    public float GetKnockBackForce(string abilityName)
    {
        if (knockBackForceValues.ContainsKey(abilityName))
        {
            return knockBackForceValues[abilityName];
        }
        else
        {
            Debug.Log("Ability not found: " + abilityName);
            return defaultKnockBackForce;
        }
    }
    public float GetBoostAmount(string abilityName)
    {
        if (boostAmountValues.ContainsKey(abilityName))
        {
            return boostAmountValues[abilityName];
        }
        else
        {
            Debug.Log("Ability not found: " + abilityName);
            return defaultBoostAmount;
        }
    }
}
