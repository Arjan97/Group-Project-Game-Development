using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour 
{
    public float speed = 5f;
    public float jumpHeight = 2f;

    private bool isGrounded = true;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get horizontal and vertical input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move the player horizontally and vertically
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Jump if the player is grounded and the jump key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}