using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilityUpgradeUI : MonoBehaviour
{
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI upgradePointsText;
    public GameObject player; //for disabling mov and setting camera angle
    private bool isOpen = false; //temp.

    [Header("Ground Slam Upgrade")]
    [SerializeField] private GameObject groundSlamUpgradePanel;
    [SerializeField] private TextMeshProUGUI groundSlamDamageText;
    [SerializeField] private TextMeshProUGUI groundSlamRadiusText;
    [SerializeField] private TextMeshProUGUI groundSlamCooldownText;

    private void Start()
    {
        upgradePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isOpen)
        {
            UpdateGroundSlamUpgradeText();
            UpdateUpgradePointsText();
            ShowUpgradePanel();
            isOpen= true;
            player.GetComponent<PlayerInteract>().isInteracting = true;
            player.GetComponent<PlayerInteract>().DisablePlayerActions();
        }
        else if (Input.GetKeyDown(KeyCode.F) && isOpen)
        {
            HideUpgradePanel();
            isOpen = false;
            player.GetComponent<PlayerInteract>().isInteracting = false;
            player.GetComponent<PlayerInteract>().EnablePlayerActions();
        }
    }

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
    }

    public void HideUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }

    public void UpgradeGroundSlamDamage()
    {
        int cost = abilityManager.groundSlamDamageLevel * 10 + 10;
        if (abilityManager.CanAfford(cost))
        {
            abilityManager.SpendPoints(cost);
            abilityManager.groundSlamDamageLevel++;
            abilityManager.UpgradeGroundSlamDamage(5f);
            UpdateUpgradePointsText();
            UpdateGroundSlamUpgradeText();
        }
    }

    public void UpgradeGroundSlamRadius()
    {
        int cost = abilityManager.groundSlamRadiusLevel * 10 + 10;
        if (abilityManager.CanAfford(cost))
        {
            abilityManager.SpendPoints(cost);
            abilityManager.groundSlamRadiusLevel++;
            abilityManager.UpgradeGroundSlamRadius(1f);
            UpdateUpgradePointsText();
            UpdateGroundSlamUpgradeText();
        }
    }

    public void UpgradeGroundSlamCooldown()
    {
        int cost = abilityManager.groundSlamCooldownLevel * 10 + 10;
        if (abilityManager.CanAfford(cost))
        {
            abilityManager.SpendPoints(cost);
            abilityManager.groundSlamCooldownLevel++;
            abilityManager.UpgradeGroundSlamCooldown(0.5f);
            UpdateUpgradePointsText();
            UpdateGroundSlamUpgradeText();
        }
    }

    private void UpdateUpgradePointsText()
    {
        upgradePointsText.text = "Skillpoints: " + abilityManager.upgradePoints;
    }

    private void UpdateGroundSlamUpgradeText()
    {
        groundSlamDamageText.text = "Damage: " + abilityManager.groundSlamDamage +
                                    " (Lv. " + abilityManager.groundSlamDamageLevel + ")";
        groundSlamRadiusText.text = "Radius: " + abilityManager.groundSlamRadius +
                                    " (Lv. " + abilityManager.groundSlamRadiusLevel + ")";
        groundSlamCooldownText.text = "Cooldown: " + abilityManager.groundSlamCooldown +
                                       "s (Lv. " + abilityManager.groundSlamCooldownLevel + ")";
    }
}
