using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VerticalSlidingDoor : Interactable
{
    public bool isOpen = false;

    [SerializeField] private float slideAmount = 1.0f;
    [SerializeField] private float slideSpeed = 1.0f;

    private Vector3 startPosition;
    private Vector3 slideDirection = Vector3.up;

    private void Start()
    {
        startPosition = transform.position;
    }

    public override void Interact(GameObject player)
    {
        Debug.Log("Door opening");

        if (isOpen) return;

        StartCoroutine(DoorOpen());
    }

    public override void VRInteract(SelectEnterEventArgs args)
    {
        Debug.Log("Door opening");

        if (isOpen) return;

        StartCoroutine(DoorOpen());
    }

    private IEnumerator DoorOpen()
    {
        Vector3 endPosition = startPosition + slideAmount * slideDirection;

        float timeInSecond = 0;

        isOpen = true;
        
        while (timeInSecond < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, timeInSecond);

            yield return null;

            timeInSecond += Time.deltaTime * slideSpeed;
        }
    }
}
