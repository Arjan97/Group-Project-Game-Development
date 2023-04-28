using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public float radius;

    [Range(0, 360)]
    public float angle;

    public GameObject player;

    public LayerMask playerMask;
    public LayerMask obstructionMask;

    public bool playerInView;

    //Bools for editing to show the radius, angle and direction to player
    public bool showRadius;
    public bool showAngle;
    public bool showPlayerInView;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        showRadius = true;
        showAngle = true;
        showPlayerInView = true;
    }

    private IEnumerator FOVRoutine()
    {
        float waitTime = 0.2f;

        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            LookForPlayer();
        }
    }

    private void LookForPlayer()
    {
        Vector3 posOffset = new Vector3(0, 1, 0);
        Collider[] rangeRays = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (rangeRays.Length != 0)
        {
            Transform playerTransform = rangeRays[0].transform;

            Vector3 direction = (playerTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direction) < angle / 2)
            {
                float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

                if (!Physics.Raycast(transform.position + posOffset, direction, distanceToPlayer, obstructionMask) ||
                    Physics.Raycast(transform.position + posOffset, direction, distanceToPlayer, playerMask))
                {
                    playerInView = true;
                }
                else
                {
                    playerInView = false;
                }

            }
            else
                playerInView = false;
        }
        else if (playerInView)
            playerInView = false;
    }
}
