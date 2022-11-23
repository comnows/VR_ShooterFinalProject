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

        InitShootAction();
        InitReloadAction();
    }

    private void OnEnable() 
    {
        gunInputActions.GunControls.Enable();
    }

    private void OnDisable() 
    {
        gunInputActions.GunControls.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGunActions()
    {

    }

    void InitShootAction()
    {
        shootAction.performed += SetShootInput;
        shootAction.canceled += SetShootInput;
    }

    void InitReloadAction()
    {
        reloadAction.performed += SetReloadInput;
        reloadAction.canceled += SetReloadInput;
    }

    void TerminateReloadAction()
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
    }


}
