using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCParent : MonoBehaviour
{
    /* Reference to the player */
    public Transform player;

    [HideInInspector]
    public string npcName;

    public virtual void StartInteraction()
    {
        transform.LookAt(player);
    }

    public virtual void ExitInteraction()
    {
      
        
    }
}
