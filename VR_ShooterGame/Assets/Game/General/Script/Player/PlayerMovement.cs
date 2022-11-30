using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    private PlayerMovementInput playerMovementInput;

    [SerializeField] private float moveSpeed = 12f;
    private Vector3 moveDirection;

    [SerializeField] private float jumpHeight = 1.2f;

    [SerializeField] private float gravityForce = -9.81f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.04f;
    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        playerMovementInput = GetComponent<PlayerMovementInput>();
    }

    private void Update() 
    {
        SetMoveDirection(playerMovementInput.MoveInput);
        ResetGravity();
        Move();
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

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Move()
    {
        characterController.Move(moveSpeed * Time.deltaTime * moveDirection);
    }

    private void Jump()
    {
        if(playerMovementInput.jumpAction.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityForce);
        }
    }

    private void SetGravity()
    {
        velocity.y += gravityForce * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
