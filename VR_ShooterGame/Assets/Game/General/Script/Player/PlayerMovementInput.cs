using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    PlayerMovement playerMovement;
    [SerializeField] PlayerLookMovement playerLookMovement;

    InputAction moveAction;
    InputAction lookAction;
    InputAction sprintAction;

    Vector2 moveInput;
    Vector2 lookDirection;

    bool isMouse;
    bool isSprint;

    private float lookSensitivity = 100f;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerMovement = GetComponent<PlayerMovement>();
        // playerLookMovement = GetComponentInChildren<PlayerLookMovement>();
    }

    void OnEnable()
    {
        playerInputActions.PlayerControls.Enable();

        AddMovementActionsListener();
    }

    void OnDisable() 
    {
        playerInputActions.PlayerControls.Disable();

        RemoveMovementActionsListener();
    }

    void AddMovementActionsListener()
    {
        AddMoveActionListener();
        AddLookActionListener();
    }

    void AddMoveActionListener()
    {
        moveAction = playerInputActions.PlayerControls.Move;
        moveAction.performed += GetMoveInputAndSetMoveDirection;
        moveAction.canceled += SetZeroMoveDirection;
    }

    void AddLookActionListener()
    {
        lookAction = playerInputActions.PlayerControls.Look;
        lookAction.performed += GetLookInput;
        lookAction.canceled += SetLookInput;
    }

    void AddSprintActionListener()
    {
        sprintAction = playerInputActions.PlayerControls.Sprint;
        sprintAction.performed += GetSprintInput;
    }

    void RemoveMovementActionsListener()
    {
        RemoveMoveActionListener();
        RemoveLookActionListener();
    }

    void RemoveMoveActionListener()
    {
        moveAction = playerInputActions.PlayerControls.Move;
        moveAction.performed -= GetMoveInputAndSetMoveDirection;
        moveAction.canceled -= SetZeroMoveDirection;
    }

    void RemoveLookActionListener()
    {
        lookAction = playerInputActions.PlayerControls.Look;
        lookAction.performed -= GetLookInput;
        lookAction.canceled -= SetLookInput;
    }

    void GetMoveInputAndSetMoveDirection(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        playerMovement.SetMoveDirection(moveInput);
    }

    void SetZeroMoveDirection(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;

        playerMovement.SetMoveDirection(moveInput);
    }
    
    void GetLookInput(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<Vector2>() * lookSensitivity * Time.deltaTime;
        // Debug.Log(lookDirection.x);

        isMouse = context.control.device.name == "Mouse";
        Debug.Log(isMouse);

        playerLookMovement.SetInput(lookDirection);
    }

    void SetLookInput(InputAction.CallbackContext context)
    {
        lookDirection = Vector2.zero;

        playerLookMovement.SetInput(lookDirection);
    }

    void GetSprintInput(InputAction.CallbackContext context)
    {
        isSprint = context.ReadValueAsButton();
    }
}
