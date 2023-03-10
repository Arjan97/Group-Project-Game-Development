using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject shopUI;

    public void OnInteraction()
    {
        shopUI.SetActive(!shopUI.activeSelf);
    }
}
