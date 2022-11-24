using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private PlayerInteractInput playerInteractInput;

    private Ray interactRay;
    private RaycastHit hitInfo;

    private Color rayColor = Color.blue;
    
    [SerializeField] private float interactRange = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerInteractInput = GetComponent<PlayerInteractInput>();
    }

    // Update is called once per frame
    void Update()
    {
        rayColor = Color.blue;
        
        interactRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        //can add masking for raycast
        if(Physics.Raycast(interactRay, out hitInfo, interactRange))
        {
            Interactable interactable = hitInfo.transform.GetComponent<Interactable>();
            if (interactable != null)
            {
                //don't forget if key is pressed
                if(playerInteractInput.playerInteractActions.Interact.triggered)
                {
                    interactable.Interact();
                }
            }

            rayColor = Color.yellow;
            Debug.Log(hitInfo.transform.name);
        }

        Debug.DrawRay(interactRay.origin, interactRay.direction * interactRange, rayColor);
    }
}
