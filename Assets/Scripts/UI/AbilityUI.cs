using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilityUI : MonoBehaviour
{
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI upgradePointsText;
    public GameObject player; //for disabling mov and setting camera angle
    private bool isOpen = false; //temp.
    GameObject activePanel = null;

    //ability trees
    public TMP_Dropdown dropdown;
    private AbilityTree fireTree;
    private AbilityTree iceTree;
    private AbilityTree groundTree;
    //abilities
    [Header("Ground Tree")]
    [SerializeField] private GameObject groundPanel;
    [SerializeField] private GameObject groundSlamUnlockButton;
    [SerializeField] private TextMeshProUGUI groundSlamUnlockText;
    [SerializeField] private GameObject groundSlamUpgradePanel;
    [SerializeField] private TextMeshProUGUI groundSlamDamageText;
    [SerializeField] private TextMeshProUGUI groundSlamRadiusText;
    [SerializeField] private TextMeshProUGUI groundSlamCooldownText;
    [SerializeField] private GameObject groundPoundUnlockButton;
    [Header("Fire Tree")]
    [SerializeField] private GameObject firePanel;
    [SerializeField] private GameObject fireballUnlockButton;

    [Header("Ice Tree")]
    [SerializeField] private GameObject icePanel;
    [SerializeField] private GameObject iceballUnlockButton;

    private void Start()
    {
        upgradePanel.SetActive(false);
        groundPanel.SetActive(false);
        icePanel.SetActive(false);
        firePanel.SetActive(false);

        fireTree = abilityManager.fireTree;
        iceTree = abilityManager.iceTree;
        groundTree = abilityManager.groundTree;

    }

    private void Update()
    {
        UpdateGroundSlamUpgradeText();
        UpdateUpgradePointsText();
        SwitchTreePanel();
        OpenTree();
    }
    public void OpenTree()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isOpen)
        {
            ShowUpgradePanel();
            isOpen = true;
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
    public void SwitchTree()
    {
        Debug.Log("SwitchTree function called");

        switch (dropdown.value)
        {

            case 0:
                // Fire tree selected
                AbilityManager.instance.SetActiveAbilityTree(fireTree);
                Debug.Log("chose fire");
                break;
            case 1:
                // Ice tree selected
                AbilityManager.instance.SetActiveAbilityTree(iceTree);
                Debug.Log("chose ice");
                break;
            case 2:
                // Ground tree selected
                AbilityManager.instance.SetActiveAbilityTree(groundTree);
                Debug.Log("chose earth");
                break;
        }
    }

    public void SwitchTreePanel()
    {
        switch (abilityManager.GetActiveTree().treeName)
        {
            case "Ground":
                activePanel = groundPanel;
                break;
            case "Ice":
                activePanel = icePanel;
                break;
            case "Fire":
                activePanel = firePanel;
                break;
        }

        if (activePanel != null && isOpen)
        {
            groundPanel.SetActive(activePanel == groundPanel);
            icePanel.SetActive(activePanel == icePanel);
            firePanel.SetActive(activePanel == firePanel);
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

    //Unluck abilities
    public void UnlockGroundSlam()
    {
      abilityManager.UnlockAbility("GroundSlam", 0);
      groundSlamUnlockButton.SetActive(false);
    }
    public void UnlockGroundPound()
    {
        abilityManager.UnlockAbility("GroundPound", 20);
        groundPoundUnlockButton.SetActive(false);
    }
    public void UnlockFireball()
    {
        abilityManager.UnlockAbility("Fireball", 0);
        fireballUnlockButton.SetActive(false);
    }
    public void UnlockIceball()
    {
        abilityManager.UnlockAbility("Iceball", 0);
        iceballUnlockButton.SetActive(false);
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
        if (!abilityManager.IsAbilityUnlocked("GroundSlam"))
        {
            // If the ability is not unlocked, disable the upgrade panel and the button
            groundSlamUnlockButton.SetActive(true);
            groundSlamUpgradePanel.SetActive(false);
            groundSlamUnlockText.text = "Unlock GroundSlam";
        }
        else
        {
            // If the ability is unlocked, enable the upgrade panel and the texts, and disable the button
            groundSlamDamageText.text = "Groundslam Damage: " + abilityManager.groundSlamDamage +
                                        " (Lv. " + abilityManager.groundSlamDamageLevel + ")";
            groundSlamRadiusText.text = "Groundslam Radius: " + abilityManager.groundSlamRadius +
                                        " (Lv. " + abilityManager.groundSlamRadiusLevel + ")";
            groundSlamCooldownText.text = "Groundslam Cooldown: " + abilityManager.groundSlamCooldown +
                                           "s (Lv. " + abilityManager.groundSlamCooldownLevel + ")";
            groundSlamUpgradePanel.SetActive(true);
            groundSlamUnlockButton.SetActive(false);
        }
    }
}
