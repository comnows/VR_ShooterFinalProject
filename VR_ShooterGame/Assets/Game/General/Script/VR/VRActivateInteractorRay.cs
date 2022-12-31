using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRActivateInteractorRay : MonoBehaviour
{
    [SerializeField] private GameObject leftInteractorRay;
    [SerializeField] private GameObject rightInteractorRay;

    [SerializeField] private InputActionProperty leftActivate;
    [SerializeField] private InputActionProperty rightActivate;

    [SerializeField] private InputActionProperty leftSelect;
    [SerializeField] private InputActionProperty rightSelect;

    void Update()
    {
        leftInteractorRay.SetActive(leftSelect.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
        leftInteractorRay.SetActive(rightSelect.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
