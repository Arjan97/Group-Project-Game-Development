using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    //abil zone
    private AbilityTreeZoneController abilityZone;
    //abilities
    [Header("Ground Tree")]
    [SerializeField] private GameObject groundPanel;
    //groundslam
    [SerializeField] private GameObject groundSlamUnlockButton;
    [SerializeField] private GameObject groundSlamSelectButton;
    [SerializeField] private TextMeshProUGUI groundSlamUnlockText;
    [SerializeField] private GameObject groundSlamUpgradePanel;
    [SerializeField] private TextMeshProUGUI groundSlamDamageText;
    [SerializeField] private TextMeshProUGUI groundSlamRadiusText;
    [SerializeField] private TextMeshProUGUI groundSlamCooldownText;
    //groundpound
    [SerializeField] private GameObject groundPoundUnlockButton;
    [SerializeField] private GameObject groundPoundSelectButton;
    //shockslam
    [SerializeField] private GameObject shockSlamUnlockButton;
    [SerializeField] private GameObject shockSlamSelectButton;
    //swiftstride
    [SerializeField] private GameObject swiftStrideUnlockButton;
    [SerializeField] private GameObject swiftStrideSelectButton;
    //rumblingrampage
    [SerializeField] private GameObject rumblingRampageUnlockButton;
    [SerializeField] private GameObject rumblingRampageSelectButton;

    [Header("Fire Tree")]
    [SerializeField] private GameObject firePanel;
    [SerializeField] private GameObject fireballUnlockButton;

    [Header("Ice Tree")]
    [SerializeField] private GameObject icePanel;
    [SerializeField] private GameObject iceballUnlockButton;

    private void Start()
    {
        GameObject abilityZoneObject = GameObject.FindGameObjectWithTag("AbilityZone");
        abilityZone = abilityZoneObject.GetComponent<AbilityTreeZoneController>();

        upgradePanel.SetActive(false);
        groundPanel.SetActive(false);
        icePanel.SetActive(false);
        firePanel.SetActive(false);

        groundPoundSelectButton.SetActive(false);
        groundSlamSelectButton.SetActive(false);
        shockSlamSelectButton.SetActive(false);
        swiftStrideSelectButton.SetActive(false);
        rumblingRampageSelectButton.SetActive(false);

        fireTree = abilityManager.fireTree;
        iceTree = abilityManager.iceTree;
        groundTree = abilityManager.groundTree;
    }

    private void Update()
    {
        UpdateUpgradePointsText();
        SwitchTreePanel();
        OpenTree();
    }
    public void OpenTree()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isOpen && abilityZone.canAccessAbilityTrees)
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
    public void SwitchFire()
    {
        // Fire tree selected
        AbilityManager.instance.SetActiveAbilityTree(fireTree);
    }
    public void SwitchGround()
    {
        // Fire tree selected
        AbilityManager.instance.SetActiveAbilityTree(groundTree);
    }
    public void SwitchIce()
    {
        // Fire tree selected
        AbilityManager.instance.SetActiveAbilityTree(iceTree);
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

    //Unlock abilities
    public void UnlockGroundSlam()
    {
        abilityManager.UnlockAbility("GroundSlam", 0);
        //abilityManager.SelectAbility("GroundSlam");
        groundSlamUnlockButton.SetActive(false);
        groundSlamSelectButton.SetActive(true);
    }
    public void UnlockShockSlam()
    {
        abilityManager.UnlockAbility("ShockSlam", 40);
        shockSlamUnlockButton.SetActive(false);
        shockSlamSelectButton.SetActive(true);
    }
    public void UnlockGroundPound()
    {
        abilityManager.UnlockAbility("GroundPound", 30);
        groundPoundUnlockButton.SetActive(false);
        groundPoundSelectButton.SetActive(true);
    }
    public void UnlockSwiftStride()
    {
        abilityManager.UnlockAbility("SwiftStride", 30);
        swiftStrideUnlockButton.SetActive(false);
        swiftStrideSelectButton.SetActive(true);
    }
    public void UnlockRumblingRampage()
    {
        abilityManager.UnlockAbility("RumblingRampage", 60);
        rumblingRampageUnlockButton.SetActive(false);
        rumblingRampageSelectButton.SetActive(true);
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
    //select abils
    public void SelectGroundSlam()
    {
        abilityManager.SelectAbility("GroundSlam");
        if (abilityManager.selectedAbilities.Contains("GroundSlam"))
        {
            ColorBlock colors = groundSlamSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.green;
            groundSlamSelectButton.GetComponent<Button>().colors = colors;
        }
        else
        {
            ColorBlock colors = groundSlamSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.red;
            groundSlamSelectButton.GetComponent<Button>().colors = colors;
        }
    }
    public void SelectShockSlam()
    {
        abilityManager.SelectAbility("ShockSlam");
        if (abilityManager.selectedAbilities.Contains("ShockSlam"))
        {
            ColorBlock colors = shockSlamSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.green;
            shockSlamSelectButton.GetComponent<Button>().colors = colors;
        }
        else
        {
            ColorBlock colors = shockSlamSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.red;
            shockSlamSelectButton.GetComponent<Button>().colors = colors;
        }
    }
    public void SelectGroundPound()
    {
        abilityManager.SelectAbility("GroundPound");
        if (abilityManager.selectedAbilities.Contains("GroundPound"))
        {
            ColorBlock colors = groundPoundSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.green;
            groundPoundSelectButton.GetComponent<Button>().colors = colors;
        }
        else
        {
            ColorBlock colors = groundPoundSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.red;
            groundPoundSelectButton.GetComponent<Button>().colors = colors;
        }
    }
    public void SelectSwiftStride()
    {
        abilityManager.SelectAbility("SwiftStride");
        if (abilityManager.selectedAbilities.Contains("SwiftStride"))
        {
            ColorBlock colors = swiftStrideSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.green;
            swiftStrideSelectButton.GetComponent<Button>().colors = colors;
        }
        else
        {
            ColorBlock colors = swiftStrideSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.red;
            swiftStrideSelectButton.GetComponent<Button>().colors = colors;
        }
    }
    public void SelectRumblingRampage()
    {
        abilityManager.SelectAbility("RumblingRampage");
        if (abilityManager.selectedAbilities.Contains("RumblingRampage"))
        {
            ColorBlock colors = rumblingRampageSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.green;
            rumblingRampageSelectButton.GetComponent<Button>().colors = colors;
        }
        else
        {
            ColorBlock colors = rumblingRampageSelectButton.GetComponent<Button>().colors;
            colors.normalColor = Color.red;
            rumblingRampageSelectButton.GetComponent<Button>().colors = colors;
        }
    }
    //upgrade abils

    private void UpdateUpgradePointsText()
    {
        upgradePointsText.text = "Skillpoints: " + abilityManager.upgradePoints;
    }

}
