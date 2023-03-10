using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCParent : MonoBehaviour
{
    /* Reference to the player */
    public Transform player;
    public void InteractWithNPC()
    {
        /* Make this NPC look at the player */
        transform.LookAt(player);

        /* Call the OnInteraction method from the specific script of each NPC, depending on their tag */
        if (gameObject.tag == "ShopNPC")
        {
            GetComponent<ShopNPC>().OnInteraction(); 
        }else if (gameObject.tag == "GuideNPC")
        {
            GetComponent<GuideNPC>().OnInteraction();
        } else if (gameObject.tag == "TalkingNPC")
        {
            GetComponent<TalkingNPC>().OnInteraction();
        }
    }
}
