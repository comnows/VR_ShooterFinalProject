using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    Camera mainCamera;
    Ray ray;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaySetup();

        Physics.Raycast(ray, out hitInfo);

        SetHitPosition();
    }

    private void RaySetup()
    {
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;
    }

    private void SetHitPosition()
    {
        transform.position = hitInfo.point;
    }
}
