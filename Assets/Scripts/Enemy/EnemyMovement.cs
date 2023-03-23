using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float stoppingDistance = 1f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Move the enemy towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Rotate the enemy towards the player
        transform.LookAt(player);

        // Stop moving if the enemy is close enough to the player
        if (Vector3.Distance(transform.position, player.position) < stoppingDistance)
        {
            speed = 0;
        }
    }
}