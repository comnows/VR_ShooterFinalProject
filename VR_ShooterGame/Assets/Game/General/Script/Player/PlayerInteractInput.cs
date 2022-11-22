using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    InputAction interactAction;

    public bool InteractInput { get; private set; } = false;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        InitInteractAction();
    }

    private void OnEnable()
    {
        playerInputActions.PlayerInteract.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.PlayerInteract.Disable();
    }

    void InitInteractAction()
    {
        interactAction = playerInputActions.PlayerInteract.Interact;
        interactAction.performed += SetInteractInput;
    }

    void SetInteractInput(InputAction.CallbackContext context)
    {
        InteractInput = context.ReadValueAsButton();
    }
}
