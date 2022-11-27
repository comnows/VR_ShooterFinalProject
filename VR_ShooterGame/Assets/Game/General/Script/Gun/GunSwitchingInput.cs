using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSwitchingInput : MonoBehaviour
{
    private GunInputActions gunInputActions;

    public GunInputActions.GunSwitchingControlsActions gunSwitchingControlsActions;

    private InputAction scrollSwitchAction;

    public float ScrollSwitchInput{ get; private set; }

    private void Awake()
    {
        gunInputActions = new GunInputActions();

        InitGunSwitchingControlsActions();
        InitInputAction();
    }

    private void InitGunSwitchingControlsActions()
    {
        gunSwitchingControlsActions = gunInputActions.GunSwitchingControls;
    }

    private void InitInputAction()
    {
        scrollSwitchAction = gunSwitchingControlsActions.ScrollSwitching;
    }

    private void OnEnable()
    {
        gunSwitchingControlsActions.Enable();

        AddScrollSwitchListener();
    }
    
    private void OnDisable()
    {
        gunSwitchingControlsActions.Disable();

        RemoveScrollSwitchListener();
    }

    private void AddScrollSwitchListener()
    {
        scrollSwitchAction.performed += SetScrollSwitchInput;
        scrollSwitchAction.canceled += SetScrollSwitchInput;
    }

    private void RemoveScrollSwitchListener()
    {
        scrollSwitchAction.performed -= SetScrollSwitchInput;
        scrollSwitchAction.canceled -= SetScrollSwitchInput;
    }

    private void SetScrollSwitchInput(InputAction.CallbackContext context)
    {
        ScrollSwitchInput = context.ReadValue<float>();
    }
}
