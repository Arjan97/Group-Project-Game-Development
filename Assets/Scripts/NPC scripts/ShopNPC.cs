using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject shopUI;

    /// <summary>
    /// Method to call when the player interacts with this npc
    /// </summary>
    public void OnInteraction()
    {
        Debug.Log("Player interacts with shop NPC");

        /* Enable/disable the shop by setting the shopUI to active/inactive  */
        shopUI.SetActive(!shopUI.activeSelf);
    }
}
