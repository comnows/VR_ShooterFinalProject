using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    public PlayerInputActions.PlayerInteractActions playerInteractActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        InitPlayerInteractActions();
    }

    private void InitPlayerInteractActions()
    {
        playerInteractActions = playerInputActions.PlayerInteract;
    }
    private void OnEnable()
    {
        playerInteractActions.Enable();
    }

    private void OnDisable()
    {
        playerInteractActions.Disable();
    }

}
