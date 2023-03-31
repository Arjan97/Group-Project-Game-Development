using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the shop NPCs that derives from the NPCparent class
/// Opens the shop window for the player upon interaction
/// </summary>
public class ShopNPC : NPCParent
{
    /* Drag the ShopUI in the inspector to be able to open/close the shop window */
    public GameObject shopUI;

    /// <summary>
    /// Method gets called when the script instance is being loaded
    /// Sets the name and tag of this NPC to the given parameter
    /// </summary>
    public void Awake()
    {
        SetName("ShopNPC");
    }

    /// <summary>
    /// Method to call when the player interacts with this NPC
    /// First checks if the tag corresponds with the name of this object 
    /// Sets interactable to true to make the player be in the interacting state
    /// Opens the shop window for the player
    /// </summary>
    public override void StartInteraction()
    {
        if (TagEqualsName())
        {
            base.StartInteraction();
            interactable = true;

            ToggleShop();
        }
    }

    /// <summary>
    /// Method gets called when the player exits the interaction with this NPC
    /// First checks if the tag is the same as the name before exiting
    /// Sets interactable bool to false to stop the player's interacting state
    /// Closes the shop window 
    /// </summary>
    public override void ExitInteraction()
    {
        if (TagEqualsName())
        {
            base.ExitInteraction();
            interactable = false;

            ToggleShop();
        }
    }

    /// <summary>
    /// Method to call to toggle ths shop active / inactive
    /// Gets called upon player interaction with this object
    /// </summary>
    private void ToggleShop()
    {
        shopUI.SetActive(!shopUI.activeSelf);
    }
}
