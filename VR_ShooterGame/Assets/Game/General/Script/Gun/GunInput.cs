using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunInput : MonoBehaviour
{
    GunInputActions gunInputActions;
    Gun gun;

    InputAction shootAction;
    InputAction aimingAction;
    InputAction reloadAction;

    public bool ShootInput { get; private set; } = false;
    public bool ReloadInput { get; private set; } = false;

    private void Awake() 
    {
        gunInputActions = new GunInputActions();

        shootAction = gunInputActions.GunControls.Shoot;
        reloadAction = gunInputActions.GunControls.Reload;
    }

    private void OnEnable() 
    {
        gunInputActions.GunControls.Enable();

        AddGunActionsListener();
    }

    private void OnDisable() 
    {
        gunInputActions.GunControls.Disable();

        RemoveGunActionsListener();
    }

    void AddGunActionsListener()
    {
        AddShootActionListener();
        AddReloadActionListener();
    }

    void RemoveGunActionsListener()
    {
        RemoveShootActionListener();
        RemoveReloadActionListener();
    }

    void AddShootActionListener()
    {
        shootAction.performed += SetShootInput;
        shootAction.canceled += SetShootInput;
    }

    void AddReloadActionListener()
    {
        reloadAction.performed += SetReloadInput;
        reloadAction.canceled += SetReloadInput;
    }

    void RemoveShootActionListener()
    {
        shootAction.performed -= SetShootInput;
        shootAction.canceled -= SetShootInput;
    }

    void RemoveReloadActionListener()
    {
        reloadAction.performed -= SetReloadInput;
        reloadAction.canceled -= SetReloadInput;
    }

    void SetShootInput(InputAction.CallbackContext context)
    {
        if(context.ReadValue<float>() > 0)
        {
            ShootInput = true;
            Debug.Log("Shoot pressed");
        }
        else
        {
            ShootInput = false;
            Debug.Log("Shoot released");
        }
    }

    void SetReloadInput(InputAction.CallbackContext context)
    {
        ReloadInput = context.ReadValueAsButton();
        Debug.Log("Reload Input = " + ReloadInput);
    }


}
