using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the TalkingNPCs that derives from NPCparent class
/// </summary>
public class TalkingNPC : NPCParent
{
    /// <summary>
    /// Method gets called when the script instance is being loaded
    /// Sets the name and tag of this NPC to the given parameter
    /// </summary>
    public void Awake()
    {
        SetName("TalkingNPC");
    }

    /// <summary>
    /// Method to call when the player interacts with this NPC
    /// First checks if the tag corresponds with the name of this object 
    /// Sets interactable to true to make the player be in the interacting state
    /// </summary>
    public override void StartInteraction()
    {
        if (TagEqualsName())
        {
            base.StartInteraction();
            interactable = true;
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
        }
    }
}
