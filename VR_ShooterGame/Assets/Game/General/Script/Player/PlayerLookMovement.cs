using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlayerLookMovement : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    private RealtimeView _realtimeView;

    private float xInput;
    private float yInput;

    private float mouseSensitivity = 100f;
    private float xRotation = 0f;

    private void Awake() {
        _realtimeView = GetComponent<RealtimeView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (_realtimeView.isOwnedLocallyInHierarchy)
            LocalUpdate();
    }

    void LocalUpdate()
    {
        SetRotation();
        Rotate();
        RotateBody();
    }

    public void SetInput(Vector2 lookInput)
    {
        xInput = lookInput.x;
        yInput = lookInput.y;
    }

    public void SetRotation()
    {
        xRotation -= yInput;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    private void Rotate()
    {
        // transform.localEulerAngles = Vector3.right * xRotation;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void RotateBody()
    {
        playerBody.Rotate(Vector3.up * xInput);
    }
}
