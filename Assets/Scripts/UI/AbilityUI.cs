using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [Header("Misc")]
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI upgradePointsText;
    public GameObject player; //for disabling mov and setting camera angle
    private bool isOpen = false; //temp.
    GameObject activePanel = null;

    //ability trees
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
    //[SerializeField] private GameObject groundSlamSelectButton;
    [SerializeField] private GameObject groundSlamUpgrade;
    [SerializeField] private TextMeshProUGUI groundSlamUpgradeTxt;
    //groundpound
    [SerializeField] private GameObject groundPoundUnlockButton;
    //[SerializeField] private TextMeshProUGUI groundPoundUnlockTxt;
    //[SerializeField] private GameObject groundPoundSelectButton;
    [SerializeField] private GameObject groundPoundUpgrade;
    [SerializeField] private TextMeshProUGUI groundPoundUpgradeTxt;
    //shockslam
    [SerializeField] private GameObject shockSlamUnlockButton;
    //[SerializeField] private TextMeshProUGUI shockSlamUnlockTxt;
    //[SerializeField] private GameObject shockSlamSelectButton;
    [SerializeField] private GameObject shockSlamUpgrade;
    [SerializeField] private TextMeshProUGUI shockSlamUpgradeTxt;
    //swiftstride
    [SerializeField] private GameObject swiftStrideUnlockButton;
    //[SerializeField] private TextMeshProUGUI swiftStrideUnlockTxt;
    //[SerializeField] private GameObject swiftStrideSelectButton;
    [SerializeField] private GameObject swiftStrideUpgrade;
    [SerializeField] private TextMeshProUGUI swiftStrideUpgradeTxt;
    //rumblingrampage
    [SerializeField] private GameObject rumblingRampageUnlockButton;
    //[SerializeField] private TextMeshProUGUI rumblingRampageUnlockTxt;
    //[SerializeField] private GameObject rumblingRampageSelectButton;
    [SerializeField] private GameObject rumblingRampageUpgrade;
    [SerializeField] private TextMeshProUGUI rumblingRampageUpgradeTxt;


    [Header("Fire Tree")]
    [SerializeField] private GameObject firePanel;
    [SerializeField] private GameObject fireballUnlockButton;
    //[SerializeField] private GameObject fireballSelectButton;

    [Header("Ice Tree")]
    [SerializeField] private GameObject icePanel;
    [SerializeField] private GameObject iceballUnlockButton;
    //[SerializeField] private GameObject iceballSelectButton;

    private void Start()
    {
        //inits
        GameObject abilityZoneObject = GameObject.FindGameObjectWithTag("AbilityZone");
        abilityZone = abilityZoneObject.GetComponent<AbilityTreeZoneController>();
        fireTree = abilityManager.fireTree;
        iceTree = abilityManager.iceTree;
        groundTree = abilityManager.groundTree;
        //panels
        upgradePanel.SetActive(false);
        groundPanel.SetActive(false);
        icePanel.SetActive(false);
        firePanel.SetActive(false);
        //selects
        /*
        groundPoundSelectButton.SetActive(false);
        groundSlamSelectButton.SetActive(false);
        shockSlamSelectButton.SetActive(false);
        swiftStrideSelectButton.SetActive(false);
        rumblingRampageSelectButton.SetActive(false);
        fireballSelectButton.SetActive(false);
        iceballSelectButton.SetActive(false);
        */
        //upgrades
        groundSlamUpgrade.SetActive(false);
        groundPoundUpgrade.SetActive(false);
        shockSlamUpgrade.SetActive(false);
        swiftStrideUpgrade.SetActive(false);
        rumblingRampageUpgrade.SetActive(false);
    }

    private void Update()
    {
        UpdateText();
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
        AbilityManager.instance.SetActiveAbilityTree(fireTree);
    }
    public void SwitchGround()
    {
        AbilityManager.instance.SetActiveAbilityTree(groundTree);
    }
    public void SwitchIce()
    {
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
    private void UpdateText()
    {
        //skillpoints
        upgradePointsText.text = "Skillpoints: " + abilityManager.upgradePoints;
        //unlock txts
        //upgrade texts
        groundSlamUpgradeTxt.text = "Upgrade GroundSlam Lvl." + AbilityManager.instance.GetLevel("GroundSlam");
        groundPoundUpgradeTxt.text = "Upgrade GroundPound Lvl." + AbilityManager.instance.GetLevel("GroundPound");
        shockSlamUpgradeTxt.text = "Upgrade ShockSlam Lvl." + AbilityManager.instance.GetLevel("ShockSlam");
        swiftStrideUpgradeTxt.text = "Upgrade SwiftStride Lvl." + AbilityManager.instance.GetLevel("SwiftStride");
        rumblingRampageUpgradeTxt.text = "Upgrade RumblingRampage Lvl." + AbilityManager.instance.GetLevel("RumblingRampage");
    }
    //Unlock abilities
    public void UnlockGroundSlam()
    {
        abilityManager.UnlockAbility("GroundSlam", 0);
        if (abilityManager.IsAbilityUnlocked("GroundSlam")){
            groundSlamUnlockButton.SetActive(false);
            //groundSlamSelectButton.SetActive(true);
            groundSlamUpgrade.SetActive(true);
        }
    }
    public void UnlockShockSlam()
    {
        abilityManager.UnlockAbility("ShockSlam", 40);
        if (abilityManager.IsAbilityUnlocked("ShockSlam"))
        {
            shockSlamUnlockButton.SetActive(false);
            //shockSlamSelectButton.SetActive(true);
            shockSlamUpgrade.SetActive(true);
        }
    }
    public void UnlockGroundPound()
    {
        abilityManager.UnlockAbility("GroundPound", 20);
        if (abilityManager.IsAbilityUnlocked("GroundPound"))
        {
            groundPoundUnlockButton.SetActive(false);
            //groundPoundSelectButton.SetActive(true);
            groundPoundUpgrade.SetActive(true);
        }
    }
    public void UnlockSwiftStride()
    {
        abilityManager.UnlockAbility("SwiftStride", 30);
        if (abilityManager.IsAbilityUnlocked("SwiftStride"))
        {
            swiftStrideUnlockButton.SetActive(false);
            //swiftStrideSelectButton.SetActive(true);
            swiftStrideUpgrade.SetActive(true);
        }
    }
    public void UnlockRumblingRampage()
    {
        abilityManager.UnlockAbility("RumblingRampage", 50);
        if (abilityManager.IsAbilityUnlocked("RumblingRampage"))
        {
            rumblingRampageUnlockButton.SetActive(false);
            //rumblingRampageSelectButton.SetActive(true);
            rumblingRampageUpgrade.SetActive(true);
        }
    }
    public void UnlockFireball()
    {
        abilityManager.UnlockAbility("Fireball", 0);
        fireballUnlockButton.SetActive(false);
        //fireballSelectButton.SetActive(true);
    }
    public void UnlockIceball()
    {
        abilityManager.UnlockAbility("Iceball", 0);
        iceballUnlockButton.SetActive(false);
        //iceballSelectButton.SetActive(true);
    }
    //select abils
    /*
    public void SelectGroundSlam()
    {
        abilityManager.SelectAbility("GroundSlam");
    }
    public void SelectShockSlam()
    {
        abilityManager.SelectAbility("ShockSlam");
    }
    public void SelectGroundPound()
    {
        abilityManager.SelectAbility("GroundPound");
    }
    public void SelectSwiftStride()
    {
        abilityManager.SelectAbility("SwiftStride");
    }
    public void SelectRumblingRampage()
    {
        abilityManager.SelectAbility("RumblingRampage");
    }
    public void SelectFireball()
    {
        abilityManager.SelectAbility("Fireball");
    }
    public void SelectIceball()
    {
        abilityManager.SelectAbility("Iceball");
    } */
    //upgrade abils
    public void UpgradeGroundSlam()
    {
        abilityManager.UpgradeValues("GroundSlam", 5, 1, 1, 0, 0, 0);
    }
    public void UpgradeGroundPound()
    {
        abilityManager.UpgradeValues("GroundPound", 10, 1, 1, 0.5f, 0, 0);
    }
    public void UpgradeShockSlam()
    {
        abilityManager.UpgradeValues("ShockSlam", 5, 1, 1, 0.5f, 0.5f, 0);
    }
    public void UpgradeSwiftStride()
    {
        abilityManager.UpgradeValues("SwiftStride", 5, 1, 1, 0, 0.5f, 0.5f);
    }
    public void UpgradeRumblingRampage()
    {
        abilityManager.UpgradeValues("RumblingRampage", 12, 1, 1, 0, 1, 0);
    }

}
