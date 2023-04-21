using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    InputMaster inputMaster;
    Animator animator;

    public Rigidbody rb;
    public float force;


    public float sensitivity = 0.1f;
    private float lookRotation;
    private float movementSpeed;

    private bool sprinting;
    public bool locked = true;

    public float speedBoost = 10f;
    private void Awake()
    {
        inputMaster = new InputMaster();
        animator = GetComponent<Animator>();

        inputMaster.Player.SprintStart.performed += x => SprintPressed();
        inputMaster.Player.SprintFinish.performed += x => SprintReleased();

    }


    private void OnEnable()
    {
        inputMaster.Player.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Player.Disable();
    }

    private void FixedUpdate()
    {
        Idle();
        Move();
    }
    private void LateUpdate()
    {
        LockCursor();
    }

    private void Move()
    {
        Vector2 input = inputMaster.Player.Movement.ReadValue<Vector2>();
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y) * movementSpeed;

        targetVelocity = transform.TransformDirection(targetVelocity);
        Vector3 velocityChange = (targetVelocity - rb.velocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        // Apply speed boost
        if (sprinting && rb.velocity != Vector3.zero)
        {
            movementSpeed = speedBoost;
            animator.SetBool("goWalk", false);
            animator.SetBool("goRun", true);
        }
        else
        {
            movementSpeed = 5f;
            animator.SetBool("goWalk", true);
            animator.SetBool("goRun", false);
        }

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public void Idle()
    {
        Vector2 input = inputMaster.Player.Movement.ReadValue<Vector2>();

        if (input == Vector2.zero) { animator.SetBool("goIdle", true); animator.SetBool("goWalk", false); animator.SetBool("goRun", false); }
        else { animator.SetBool("goIdle", false); animator.SetBool("goWalk", true); }
        if (rb.velocity == Vector3.zero) { animator.SetBool("goIdle", true); animator.SetBool("goWalk", false); animator.SetBool("goRun", false); }
        else { animator.SetBool("goIdle", false); animator.SetBool("goWalk", true); }
    }
    void SprintPressed()
    {
        sprinting = true;
    }

    void SprintReleased()
    {
        sprinting = false;
        animator.SetBool("goRun", false);
    }

    void Look()
    {

        Vector2 look = inputMaster.Player.Look.ReadValue<Vector2>();

        transform.Rotate(Vector3.up * look.x * sensitivity);

        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -30, 10);

        //cameraHead.transform.eulerAngles = new Vector3(lookRotation, cameraHead.transform.eulerAngles.y, cameraHead.transform.eulerAngles.z);
    }
    public void LockCursor()
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Look();
            Idle();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            movementSpeed = 0;
            animator.SetBool("goWalk", false);
            animator.SetBool("goRun", false);
            animator.SetBool("goIdle", true) ;
        }
    }

    public void ApplySpeedBoost(float duration, float amount)
    {
        speedBoost = amount;

        // Start a coroutine to revert the speed boost after a certain duration
        StartCoroutine(RevertSpeedBoost(duration));
    }

    private IEnumerator RevertSpeedBoost(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Revert the speed boost
        speedBoost = 10f;
    }
}
