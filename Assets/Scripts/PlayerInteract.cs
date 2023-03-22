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
            /* When receiving input first check if the player is not already interacting with the NPC */
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
                        /* Call the method to interact with the npc, with the NPC that got hit as parameter */
                        OnInteraction(npcInteractable);
                    }
                }

                /* If the player is already interacting when receiving input, the player has to stop interacting with the NPC*/
            }  else if (isInteracting)
            {
                OnInteractionExit();
            }
        }


    }
    /// <summary>
    /// Method to call when the player starts interacting with a NPC
    /// Gets called if one of the colliders hit an GameObject with the NPCParent script attached to it
    /// </summary>
    /// <param name="npc"></param>
    private void OnInteraction(NPCParent npc)
    {
        /* Set the isInteracting bool to true to handle camera states */
        isInteracting = true;

        /* Get the rigidbody and stop its movement by setting velocity to 0 */
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        /* Then call the idle method from the movement script to start the idle animation */
        GetComponent<PlayerMovement2>().Idle();

        /* Disable the movement script to stop players from moving and rotating the camera when interacting with a NPC */
        GetComponent<PlayerMovement2>().enabled = false;

        /* Call the method from the NPC to interact with the NPC*/
        npc.InteractWithNPC();
    }

    /// <summary>
    /// Method to call when receiving input to stop interacting with a NPC
    /// Gets called when pressing E when the player is already interacting
    /// </summary>
    private void OnInteractionExit()
    {
        /* Set the isInteracting to false to handle camera state*/
        isInteracting = false;

        /* Enable the player movement script again to start moving and rotating again */
        GetComponent<PlayerMovement2>().enabled = true;

    }
}
