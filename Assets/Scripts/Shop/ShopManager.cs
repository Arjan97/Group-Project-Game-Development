using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public int coins = 300;
    public Upgrade[] upgrades;

    public TextMeshProUGUI coinText;
    public GameObject shopUI;
    public Transform shopContent;
    public GameObject itemPrefab;
    //public PlayerMovement player; for speed need to check movement class

    private void Awake()
    {
        //if instance already exist it gets removed
        if (instance == null) instance = this;
        else Destroy(gameObject);

        //perserve object during scene loading
        DontDestroyOnLoad(gameObject);
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
            //item.GetComponent<Button>().onClick.AddListener(() => { BuyUpgrade(upgrade) };
        }
        
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        if(coins >= upgrade.cost)
        {
            coins -= upgrade.cost;
            upgrade.quantity++;
            upgrade.itemRef.transform.GetChild(0).GetComponent<Text>().text = upgrade.quantity.ToString();
           
            //apply upgrade

        }
    }

    /*public void ToggleShop()
    {
        //if shopUI is active, it turns inactive
        shopUI.SetActive(!shopUI.activeSelf);
    }*/

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