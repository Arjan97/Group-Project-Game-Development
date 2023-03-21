using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool isInteracting;
    void Update()
    {
        /* If player presses the specified button */
        if (Input.GetKeyDown(KeyCode.E) )
        {
            if (isInteracting == false)
            {
                /* Set the range of from where the player can interact with */
                float interactRange = 2f;

                /* Make an array of collider rays that shoot from the player position towards every direction with length of interactRange */
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

                /* Foreach ray that hit another collider */
                foreach (Collider collider in colliderArray)
                {
                    /* Get the NPCParent script reference */
                    if (collider.TryGetComponent(out NPCParent npcInteractable))
                    {
                        isInteracting = true;
                        /* Call the InteractWithNPC method from the NPC that the ray hits */
                        npcInteractable.InteractWithNPC();
                    }
                }
            }  else if (isInteracting)
            {
                isInteracting = false; 
            }
        }
    }
}
