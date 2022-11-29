using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    private PlayerMovementInput playerMovementInput;

    [SerializeField] private Transform _character = default;

    [SerializeField] private float moveSpeed = 12f;
    
    private Vector3 moveDirection;

    private RealtimeView _realtimeView;

    private void Awake() 
    {
        _realtimeView = GetComponent<RealtimeView>();    
        playerMovementInput = GetComponent<PlayerMovementInput>();
    }

    private void Start() {
        if (_realtimeView.isOwnedLocallyInHierarchy)
            LocalStart();
    }
    private void LocalStart() {
        // Request ownership of the Player and the character RealtimeTransforms
        GetComponent<RealtimeTransform>().RequestOwnership();
        characterController.GetComponent<RealtimeTransform>().RequestOwnership();
    }

    private void Update() 
    {
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            SetMoveDirection(playerMovementInput.MoveInput);
            Move();
        }
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
