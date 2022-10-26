using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float moveSpeed = 12f;
    private Vector3 moveDirection;

    private void Update() 
    {
        Move();
    }

    public void SetMoveDirection(Vector2 moveInput)
    {
        moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
    }

    private void Move()
    {
        characterController.Move(moveSpeed * Time.deltaTime * moveDirection);
    }
}
