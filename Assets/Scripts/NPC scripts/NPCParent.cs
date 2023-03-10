using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCParent : MonoBehaviour
{
    public Transform player;
    public void InteractWithNPC()
    {
        Debug.Log("NPC interacts with player");
        transform.LookAt(player);

        if (gameObject.tag == "ShopNPC")
        {
            GetComponent<ShopNPC>().OnInteraction();
        }
        if (gameObject.tag == "GuideNPC")
        {
            GetComponent<GuideNPC>().OnInteraction();

        }
        if (gameObject.tag == "TalkingNPC")
        {
            GetComponent<TalkingNPC>().OnInteraction();
        }
    }
}
