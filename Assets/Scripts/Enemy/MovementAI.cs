using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// Class for AI movement of a GameObject
/// Moves an object to a location within a random radius of the direction it is facing
/// </summary>
public class MovementAI : MonoBehaviour
{
    /* Variable to access the NavMesh component of this GameObject */
    private NavMeshAgent agent;

    public Transform player;

    private EnemyFOV fieldOfView;

    private EnemyAnimationController animationController;

    /* Variable of type Vector3 that this agent will move towards */
    private Vector3 targetVector;

    /* Minimum and maximum values that will determine the new direction to move to */
    [SerializeField] [Tooltip("Minimum angle to determine a new path")] private float angleMin = -90f; 
    [SerializeField] [Tooltip("Maximum angle to determine a new path")] private float angleMax = 90f;

    /* The length of the path of the agent before getting a new path */
    [Tooltip("Length of the moving path")]
    [SerializeField] private float pathLength = 10;

    [SerializeField] private float rotateSpeed = 5f;

    /* Float to store the new angle for a new direction, calculated randomly between angleMin and angleMax */
    private float randomDegrees;

    /* When the agent is within this range of its destination, it gets a new destination */
    private float arrivingRange = 1.5f;

    [Tooltip("Attacks the player when this close")]
    public float withinAttackRange = 3f;
    
    public bool chasingPlayer;
    public bool playerInAttackRange = false;
    public bool patrolling;
    public bool idle;

    void Start()
    {
        /* Get the navMeshAgent component */
        agent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponent<EnemyFOV>();
        animationController = GetComponent<EnemyAnimationController>();

        /* Give the agent a new destination at start */
        GetNewCheckpoint();
    }

    void Update()
    {
        /* Rotate towards the moving direction */
        RotateTowards(targetVector);

        if (fieldOfView.playerInView)
        {
            ChasePlayer();
        }
        else
        {
            /* Get the distance between the targetVector and the current position */
            float distanceFromTarget = Vector3.Distance(transform.position, targetVector);

            /* Check if the agent is within arrivingRange of its target */
            if (distanceFromTarget <= arrivingRange)
            {
                /* Call method to get a new destination if it is close */
                GetNewCheckpoint();
            }
        }

        /* Draw a white line in the editor to see where the targetvector is */
        Debug.DrawLine(transform.position, targetVector, Color.white);
    }

    /// <summary>
    /// Method to call to get a new location to move to
    /// </summary>
    private void GetNewCheckpoint()
    {
        /* Set the targetvector equal to a new random vector, gained by calling the GetNewWaypoint() method */
        targetVector = NewCheckpoint();

        /* Call the SetDestination method with the new targetvector as parameter to move towards the new vector*/
        agent.SetDestination(targetVector);
    }

    private void ChasePlayer()
    {
        targetVector = player.position;
       
        agent.SetDestination(targetVector);

        float distanceFromPlayer = Vector3.Distance(transform.position, targetVector);

        if (distanceFromPlayer < withinAttackRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            AttackPlayer(directionToPlayer);


            Debug.Log("In attack range");
            agent.isStopped = true;
        }
    }

    private void AttackPlayer(Vector3 direction)
    {
        if (!playerInAttackRange)
        {
            animationController.PlayAnimation("Attack");
            playerInAttackRange = true;
        }

        playerInAttackRange = true;

        //Code to fire an ability here
    }

    private IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Debug.Log("test");

    }
    /// <summary>
    /// Method to rotate the object towards the given target
    /// </summary>
    /// <param name="target"></param>
    private void RotateTowards(Vector3 target)
    {
        /* Vector pointing from the position towards the target */
        Vector3 directionVector = target - transform.position;

        /* Store the rotation in a quaternion that is rotated towards the directionVector */
        Quaternion targetRotation = Quaternion.LookRotation(directionVector);

        /* Rotate the object smoothly towards the directionVector */
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Returns a vector that is used as a new checkpoint for this agent
    /// </summary>
    /// <returns></returns>
    private Vector3 NewCheckpoint()
    {
        /* Initialize the vector to return */
        Vector3 newCheckpoint;

        /* Get a new ray by calling the GetRay() method */
        Ray newDirectionRay = GetRay();

        /* See if the new ray hits something */
        if (Physics.Raycast(newDirectionRay, out RaycastHit hit, pathLength))
        {
            /* If it is a hit, set the new checkpoint to the point that hit */
            newCheckpoint = hit.point;
        }
        else /* If the ray does not hit something */
        {
            /* Set the new chekpoint to a point x distance on the new ray */
            newCheckpoint = newDirectionRay.GetPoint(pathLength);
        }

        /* Return the new checkpoint as vector */
        return newCheckpoint;
    }

    /// <summary>
    /// Method to call to get a ray that is rotated a random amount around the Y-axis
    /// </summary>
    /// <returns></returns>
    private Ray GetRay()
    {
        /* Get a random value between given floats to determine a new angle */
        randomDegrees = Random.Range(angleMin, angleMax);

        /* Get the forward direction of the gameobject */
        Vector3 forwardDirection = transform.forward;

        /* Get a rotation of randomDegrees around the Y-axis */
        Quaternion rotatedAngle = Quaternion.AngleAxis(randomDegrees, transform.up);

        /* Get the new angle by rotating rotatedAngle amount in the forward facing direction */
        Vector3 newAngle = rotatedAngle * forwardDirection;

        /* Return a ray from the current position towards the new angle */
        return new Ray(transform.position, newAngle);
    }
}
