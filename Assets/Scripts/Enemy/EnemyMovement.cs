using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5.0f; // speed at which the enemy moves towards the player
    public float detectionRange = 10.0f; // range within which the enemy can detect the player
    private Transform player; // reference to the player's transform

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // calculate the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // if the player is within the detection range
        if (distance <= detectionRange)
        {
            // calculate the direction to move towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // move the enemy towards the player
            transform.position += direction * speed * Time.deltaTime;

            // rotate the enemy to face the player
            transform.LookAt(player);
        }
    }
    public void ApplyTrembleEffect(float trembleDuration)
    {
        StartCoroutine(TrembleEffect(trembleDuration));
    }
    private IEnumerator TrembleEffect(float trembleDuration )
    {
        // Store the initial movement speed
        float originalSpeed = speed;

        // Apply the tremble effect by halving the movement speed
        speed *= 0.5f;
        Debug.Log("enemyspeed: " + speed);
        // Wait for the tremble duration
        yield return new WaitForSeconds(trembleDuration);

        // Reset the movement speed back to its original value
        speed = originalSpeed;
        Debug.Log("enemyspeed reset to: " + speed);
    }
}