using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class scr_MovementComponent : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 5;

    [SerializeField]
    float runSpeed = 10;

    [SerializeField]
    float jumpForce = 5;

    // Components 
    private scr_PlayerController playerController;
    Rigidbody rb;
    Animator playerAnimator;

    // References
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    // animator hashes
    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");


    private void Awake()
    {

        playerController = GetComponent<scr_PlayerController>();
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

    }

    private void Update()
    {
        if(playerController.isJumping)
        {
            return;
        }

        if (!(inputVector.magnitude > 0))
        {
            moveDirection = Vector3.zero;
        }

        moveDirection = (transform.forward * inputVector.y) + (transform.right * inputVector.x);
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;
        
        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);
        
        transform.position += movementDirection;

    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        playerAnimator.SetFloat(movementXHash, inputVector.x);
        playerAnimator.SetFloat(movementYHash, inputVector.y);

    }
    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, playerController.isRunning);

    }

    public void OnJump(InputValue value)
    {
        if (!playerController.isJumping)
        {
            playerController.isJumping = value.isPressed;

            rb.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
            playerAnimator.SetBool(isJumpingHash, playerController.isJumping);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);

    }







}