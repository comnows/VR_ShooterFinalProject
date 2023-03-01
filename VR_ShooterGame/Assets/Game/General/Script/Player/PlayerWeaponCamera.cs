using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponCamera : MonoBehaviour
{
  void Awake()
    {
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.layer = LayerIgnoreRaycast;
        Debug.Log("Current layer: " + gameObject.layer);
    }
}
