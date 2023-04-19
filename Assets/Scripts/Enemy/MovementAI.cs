using System.Collections;
using System.Collections.Generic;
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

    /* Variable of type Vector3 that this agent will move towards */
    private Vector3 targetVector;

    /* Minimum and maximum values that will determine the new direction to move to */
    [SerializeField] private float angleMin = -90f;
    [SerializeField] private float angleMax = 90f;

    /* The length of the path of the agent before getting a new path */
    [SerializeField] private float pathLength = 10f;

    /* When the agent is within this range of its destination, it gets a new destination */
    private float arrivingRange = 5;

    void Start()
    {
        /* Get the navMeshAgent component */
        agent = GetComponent<NavMeshAgent>();

        /* Give the agent a new destination at start */
        NewDestination();
    }

    void Update()
    {
        /* Draw a ray in the forward moving direction */ 
        Debug.DrawLine(transform.position + new Vector3(0, 1), transform.forward, color: Color.blue);

        /* Check if the agent is within arrivingRange of its target */
        if (Vector3.Distance(transform.position, targetVector) <= arrivingRange)
        {
            /* Call method to get a new destination upon reaching its destination */
            NewDestination();
        }
    }

    /// <summary>
    /// Method to call to give this object a new location to move to
    /// </summary>
    private void NewDestination()
    {
        /* Set the targetvector equal to a new random vector, gained by calling the GetNewWaypoint() method */
        targetVector = NewCheckpoint();

        /* Call the SetDestination method with the new targetvector as parameter to move towards the new vector*/
        agent.SetDestination(targetVector);
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

            /* Draw a black line to see what it hit */
            Debug.DrawLine(transform.position, newCheckpoint, Color.black);
        }
        else /* If the ray does not hit something */
        {
            /* Set the new chekpoint to a point x distance on the new ray */
            newCheckpoint = newDirectionRay.GetPoint(pathLength);

            /* Draw a yellow line to see where the checkpoint is */
            Debug.DrawLine(transform.position, newCheckpoint, Color.yellow);
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
        float randomDegrees = Random.Range(angleMin, angleMax);

        /* Get the forward direction of the gameobject */
        Vector3 forwardDirection = transform.forward;

        /* Get a rotation of randomDegrees around the Y-axis */
        Quaternion rotatedAngle = Quaternion.AngleAxis(randomDegrees, new Vector3(0, 1, 0));

        /* Get the new angle by rotating rotatedAngle amount in the forward facing direction */
        Vector3 newAngle = rotatedAngle * forwardDirection;

        /* Return a ray from the current position towards the new angle */
        return new Ray(transform.position, newAngle);
    }
}
