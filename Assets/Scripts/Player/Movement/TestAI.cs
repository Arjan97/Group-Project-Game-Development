//using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TestAI : MonoBehaviour
{
    private Vector3 forwardDirection;

    private float randomAngle;
    private float randomMin = -90f;
    private float randomMax = 90f;

    private Vector3 targetVector;

    float randomDegrees;

    private NavMeshAgent agent;

    private float arrivingRange = 5;

    // Start is called before the first frame update
    void Start()
    {
        randomAngle = Random.Range(randomMin, randomMax);

//        randomDegrees = Random.Range(randomMin, randomMax);
        agent = GetComponent<NavMeshAgent>();

        NewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        //Forward ray
        Debug.DrawLine(transform.position + new Vector3(0, 1), transform.forward * 100, color: Color.red);

        if (Vector3.Distance(transform.position, targetVector) <= arrivingRange)
        {
            /* Call method to get a new destination upon reaching its destination */
            NewDestination();
        }
    }

    private void NewDestination()
    {
        /* Set the targetvector equal to a new random vector, gained by calling the GetNewWaypoint() method */
        targetVector = NewCheckpoint();

        /* Call the SetDestination method with the new targetvector as parameter to move towards the new vector*/
        agent.SetDestination(targetVector);
    }

    private Vector3 NewCheckpoint()
    {
        Vector3 newCheckpoint;

        Ray newDirectionRay = GetRay();

        if (Physics.Raycast(newDirectionRay, out RaycastHit hit, 10))
        {
            newCheckpoint = hit.point;
            Debug.DrawLine(transform.position, newCheckpoint, Color.black);
        }
        else
        {
            newCheckpoint = newDirectionRay.GetPoint(10f);

            Debug.DrawLine(transform.position, newCheckpoint, Color.yellow);
        }

        return newCheckpoint;
    }

    private Ray GetRay()
    {
        float randomDegrees = Random.Range(randomMin, randomMax);

        Vector3 forwardDirection = transform.forward;

        Quaternion rotatedAngle = Quaternion.AngleAxis(randomDegrees, new Vector3(0, 1, 0));

        Vector3 newAngle = rotatedAngle * forwardDirection;

        return new Ray(transform.position, newAngle);
    }
}
