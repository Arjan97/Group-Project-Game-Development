using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to let the player interact with NPCs
/// Checks if a NPC is within range after receiving input
/// Calls the interaction method of the NPC and disables movement and rotation on interaction
/// Calls the exit interaction method of the NPC and enables movement and rotation after interacting
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    /* Get access to the movement of the player */
    private PlayerMovement2 movementScript;

    /* Made public to be accesible in CameraStateController script */
    [HideInInspector]
    public bool isInteracting;
    public void Awake()
    {
        movementScript = GetComponent<PlayerMovement2>();
    }
    void Update()
    {
        /* If player presses the specified button */
        if (Input.GetKeyDown(KeyCode.E))
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
                    if (!isInteracting)
                    {
                        /* Call the method to start interacting if the player is not already interacting */
                        OnInteractionStart(npcInteractable);
                    } else
                    {
                        /* If the player is already interacting, it has to stop doing so */
                        OnInteractionExit(npcInteractable);
                    }
                }
            }
        }      
    }

    /// <summary>
    /// Method to call when the player starts interacting with a NPC
    /// Calls the interaction method of the NPC
    /// Disables movement and rotation of the player
    /// </summary>
    /// <param name="npc"></param>
    private void OnInteractionStart(NPCParent npc)
    {
        npc.StartInteraction();

        if (npc.interactable)
        {
            isInteracting = true;

            DisablePlayerActions();
        }
    }

    /// <summary>
    /// Method to call when the payer has to stop interacting with a NPC
    /// Calls the exit interaction method of the NPC
    /// Enables movement and rotation
    /// </summary>
    private void OnInteractionExit(NPCParent npc)
    {
        npc.ExitInteraction();

        /* Set the isInteracting bool to false to adjust the camera after interaction */
        isInteracting = false;

        EnablePlayerActions();
    }

    /// <summary>
    /// Method that disables the movement and rotation of the player
    /// Gets called when interacting with a NPC
    /// </summary>
    private void DisablePlayerActions()
    {
        /* Set the locked bool to not locked */
        movementScript.locked = !movementScript.locked;

        /* Then call the method to unlock the cursor in the game window */
        movementScript.LockCursor();

        /* Get the rigidbody of the player and set its velocity to 0 */
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        /* Call the Idle method from the movementScript to start the idle animation */
        movementScript.Idle();

        /* Disable the movement script to stop the player from moving and rotating while interacting */
        movementScript.enabled = false;
    }

    /// <summary>
    /// Method that enables the movement and rotation of the player after it has been disabled
    /// Gets called when exiting a interaction with a NPC
    /// </summary>
    private void EnablePlayerActions()
    {
        /* Lock the mouse cursor again */
        movementScript.locked = true;

        /* Enable the movement script after interacting to let the player move and rotate again */
        movementScript.enabled = true;
    }
}
