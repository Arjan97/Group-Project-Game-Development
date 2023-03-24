using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public int coins = 325;
    public Upgrade[] upgrades;

    public TextMeshProUGUI coinText;
    public GameObject shopUI;
    public Transform shopContent;
    public GameObject itemPrefab;
    public PlayerHealth playerHealth;
    public HealthBar healthBar;

    //public PlayerMovement player; for speed need to check movement class

    private void Awake()
    {
        //if instance already exist it gets removed
        if (instance == null) instance = this;
        else Destroy(gameObject);

        //perserve object during scene loading
        DontDestroyOnLoad(gameObject);

        shopUI.SetActive(false);

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        healthBar = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<HealthBar>();
    }

    private void Start()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            //instantiate new item
            GameObject item = Instantiate(itemPrefab, shopContent);

            upgrade.itemRef = item;

            foreach (Transform child in item.transform)
            {
                if (child.gameObject.name == "Quantity") child.gameObject.GetComponent<TextMeshProUGUI>().text = upgrade.quantity.ToString();
                else if (child.gameObject.name == "Cost") child.gameObject.GetComponent<TextMeshProUGUI>().text = "$" + upgrade.cost.ToString();
                else if (child.gameObject.name == "Name") child.gameObject.GetComponent<TextMeshProUGUI>().text = upgrade.name;
                else if (child.gameObject.name == "Image") child.gameObject.GetComponent<Image>().sprite = upgrade.sprite;
            }
            item.GetComponent<Button>().onClick.AddListener(() => { BuyUpgrade(upgrade); });
        }
        
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        //if(coins >= upgrade.cost)
        {
            // coins -= upgrade.cost;
            //upgrade.quantity--;
            //upgrade.itemRef.transform.GetChild(0).GetComponent<Text>().text = upgrade.quantity.ToString();
            //text = upgrade.quantity.ToString();

            //apply upgrade
            if (playerHealth.currentHealth < playerHealth.maxHealth)
                ApplyUpgrade(upgrade);

        }
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        Debug.Log("You are in applyupgrade");
        switch (upgrade.name)
        {
            case "Health":
                //PlayerHealth healHealth = gameObject.GetComponent<PlayerHealth>();
                //playerHealth.currentHealth += playerHealth.currentHealth += 20;
                playerHealth.currentHealth = playerHealth.maxHealth;
                //healthBar.SetHealth(playerHealth.currentHealth);
                Debug.Log("player gained health by ship");
                Debug.Log(playerHealth.currentHealth);
                break;
        }
    }

    private void OnGUI()
    {
        coinText.text = "Coins: " + coins.ToString();
    }
}

[System.Serializable]
public class Upgrade
{
    public string name;
    public int cost;
    public Sprite sprite;
    //makes int quantity not show up in the inspector
    [HideInInspector] public int quantity;
    [HideInInspector] public GameObject itemRef;
}