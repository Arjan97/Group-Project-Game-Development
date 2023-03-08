using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AImovement : MonoBehaviour
{
    private NavMeshAgent agent;

    public MeshRenderer terrain;

    private Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetVector) <= 10)
        {
            Debug.Log("Get a new destination");

            NewDestination();
        }
    }

    private void NewDestination()
    {
        targetVector = GetNewWaypoint();

        agent.SetDestination(targetVector);
    }
    private Vector3 GetNewWaypoint()
    {
        float randomX = Random.Range(terrain.bounds.min.x, terrain.bounds.max.x);
        float randomZ = Random.Range(terrain.bounds.min.z, terrain.bounds.max.z);

        Vector3 newTargetVector = new Vector3(randomX, 0, randomZ);

        return newTargetVector;
    }
}
