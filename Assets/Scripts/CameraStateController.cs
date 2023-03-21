using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateController : MonoBehaviour
{
    Animator animator;
    public GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("ThirdPerson", true);
        animator.SetBool("Interacting", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerInteract>().isInteracting)
        {
            animator.SetBool("ThirdPerson", false);
            animator.SetBool("Interacting", true);
        } else
        {
            animator.SetBool("ThirdPerson", true);
            animator.SetBool("Interacting", false);
        }
    }
}
