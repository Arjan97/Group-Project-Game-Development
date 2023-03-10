using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingNPC : MonoBehaviour
{
    /// <summary>
    /// Method to call when the player interacts with this npc
    /// </summary>
    public void OnInteraction()
    {
        Debug.Log("Player interacts with the talking NPC");
    }
}
