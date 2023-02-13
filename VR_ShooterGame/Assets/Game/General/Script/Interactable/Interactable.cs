using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;
    
    public abstract void Interact(GameObject player);
    public abstract void VRInteract(SelectEnterEventArgs args);
}
