using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Parent class for all NPCs in this project
/// </summary>
public class NPCParent : MonoBehaviour
{
    /* Each NPC has their own name that can be used for interactions and dialogue */
    [HideInInspector]
    public string npcName;

    /* Boolean to see if the child NPC is able to interact upon calling the StartInteraction method */
    [HideInInspector]
    public bool interactable = false;

    /* Reference to where the NPC should look at, set in the inspector */
    public Transform LookAtTarget;

    /// <summary>
    /// Method to call when the player starts to interact with the NPC
    /// Makes the NPC child look at the target
    /// </summary>
    public virtual void StartInteraction()
    {
        Debug.Log("Now interacting with the " + npcName);

        transform.LookAt(LookAtTarget);
    }

    /// <summary>
    /// Method to call when the player exits the interaction with the NPC
    /// </summary>
    public virtual void ExitInteraction()
    {
        Debug.Log("Exit interaction with the " + npcName);       
    }

    /// <summary>
    /// Method to call to give the NPC a name and tag if untagged
    /// Gets called in the Awake method of the child
    /// </summary>
    /// <param name="name"></param>
    public virtual void SetName(string name)
    {
        npcName = name;

        /* Give the NPC the tag that is the same as their name if they are untagged */
        if (gameObject.tag == "Untagged") gameObject.tag = npcName;
    }

    /// <summary>
    /// Bool that checks if the name of the NPC corresponds with its tag
    /// Used to allow / not allow interaction with the NPC depending on its return when the StartInteraction method is called
    /// </summary>
    /// <returns></returns>
    public virtual bool TagEqualsName()
    {
        if (gameObject.tag != npcName)
        {
            return false;
        } else { return true; }
    }
}
