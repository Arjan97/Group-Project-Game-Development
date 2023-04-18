using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveRandom : MonoBehaviour
{
    /* Variable to access the NavMesh component of this GameObject */
    private NavMeshAgent agent;

    /* Variable to get the Mesh of what this agent needs to move on. Made public to drag in the correct mesh in inspector */
    public MeshRenderer terrain;

    /* Variable of type Vector3 that this agent will move towards */
    private Vector3 targetVector;

    /* Variable to determine within what range the object needs to be before getting a new targetvector */
    private float rangeOffset = 10f;

    // Start is called before the first frame update
    void Start()
    {
        /* Get access to the NavMeshAgent component of this object */
        agent = GetComponent<NavMeshAgent>();

        /* Call NewDestination() to move towards a new destination on Start */
        NewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        /* Check to see if this object has reached its target */
        if (Vector3.Distance(transform.position, targetVector) <= rangeOffset)
        {
            /* Call method to get a new destination upon reaching its destination */
            NewDestination();
        }
    }

    /* Method to call to give this gameobject a new destination to move towards */
    private void NewDestination()
    {
        /* Set the targetvector equal to a new random vector, gained by calling the GetNewWaypoint() method */
        targetVector = GetNewWaypoint();

        /* Call the SetDestination method with the new targetvector as parameter to move towards the new vector*/
        agent.SetDestination(targetVector);
    }

    /* Method that returns a random Vector, based on the X and Z min/max values of the terrain variable */
    private Vector3 GetNewWaypoint()
    {
        //Get two random floats based on the terrain
        float randomX = Random.Range(terrain.bounds.min.x, terrain.bounds.max.x); //Get a random number between the Width of the terrain
        float randomZ = Random.Range(terrain.bounds.min.z, terrain.bounds.max.z); //Get a random number between the depth of the terrain 

        //Make a new vector with the randomX and randomZ(Y set to 0 to prevent flying in the air)
        Vector3 newTargetVector = new Vector3(randomX, 0, randomZ);

        //Return the new vector
        return newTargetVector;
    }
}
