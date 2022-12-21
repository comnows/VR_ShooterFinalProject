using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    private PlayerMovementInput playerMovementInput;

    public Animator animator;

    [SerializeField] private float moveSpeed = 12f;
    private Vector3 moveDirection;

    [SerializeField] private float jumpHeight = 1.2f;

    [SerializeField] private float gravityForce = -9.81f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.04f;
    [SerializeField] private LayerMask groundMask;

    float moveSpeedMultiplier;
    private Vector3 gravityVelocity;
    private bool isGrounded;

    private void Awake()
    {
        playerMovementInput = GetComponent<PlayerMovementInput>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update() 
    {
        SetMoveDirection(playerMovementInput.MoveInput);
        ResetGravity();
        Move();
        SetMoveAnimation(playerMovementInput.MoveInput);
        Jump();
        SetGravity();
    }

    public void SetMoveDirection(Vector2 moveInput)
    {
        moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
    }

    private void ResetGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }
    }

    private void Move()
    {
        moveSpeedMultiplier = playerMovementInput.IsSprint && playerMovementInput.MoveInput.y > 0f ? 1.5f : 1f;
        characterController.Move(moveSpeed * moveSpeedMultiplier * Time.deltaTime * moveDirection);
    }

    private void SetMoveAnimation(Vector2 moveInput)
    {
        animator.SetFloat("HorizontalMove", moveInput.x * moveSpeedMultiplier, 0.05f, Time.deltaTime);
        animator.SetFloat("VerticalMove", moveInput.y * moveSpeedMultiplier, 0.05f, Time.deltaTime);
    }

    private void Jump()
    {
        if(playerMovementInput.jumpAction.triggered && isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityForce);
        }
    }

    private void SetGravity()
    {
        gravityVelocity.y += gravityForce * Time.deltaTime;

        characterController.Move(gravityVelocity * Time.deltaTime);
    }
}
