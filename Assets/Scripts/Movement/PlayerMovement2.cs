using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    InputMaster inputMaster;
    Animator animator;

    public Rigidbody rb;
    public GameObject cameraHead;
    public float force;
    

    public float sensitivity = 0.1f;
    private float lookRotation;
    private float movementSpeed;

    private bool sprinting;
   
  

    private void Awake()
    {
        inputMaster = new InputMaster();
        animator = GetComponent<Animator>();

        inputMaster.Player.SprintStart.performed += x =>SprintPressed();
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
        Look();
    }

    private void Move()
    {
        
        Vector2 input = inputMaster.Player.Movement.ReadValue<Vector2>();

        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity *= movementSpeed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        if (sprinting && rb.velocity != Vector3.zero) { movementSpeed = 10f; animator.SetBool("goWalk", false); animator.SetBool("goRun", true); }
        else if (!sprinting) { movementSpeed = 5;}

        rb.AddForce(velocityChange, ForceMode.VelocityChange);


    }

    void Idle()
    {
        Vector2 input = inputMaster.Player.Movement.ReadValue<Vector2>();

        if (input == Vector2.zero) { animator.SetBool("goIdle", true); animator.SetBool("goWalk", false); animator.SetBool("goRun", false);}
        else {animator.SetBool("goIdle", false); animator.SetBool("goWalk", true); }
    }
    void SprintPressed()
    {
       sprinting= true;
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

        cameraHead.transform.eulerAngles = new Vector3(lookRotation, cameraHead.transform.eulerAngles.y, cameraHead.transform.eulerAngles.z);
        Cursor.lockState = CursorLockMode.Locked;
    }



}
