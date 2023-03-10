using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        Debug.Log("NPC interacts with player");
        transform.LookAt(player);

        if (gameObject.tag == "ShopNPC")
        {
            //
        }
        if (gameObject.tag == "GuideNPC")
        {
            //
        }
        if (gameObject.tag == "TalkingNPC")
        {
            //
        }
    }
}
