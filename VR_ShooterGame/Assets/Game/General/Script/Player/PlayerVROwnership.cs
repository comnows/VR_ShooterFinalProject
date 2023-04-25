using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class PlayerVROwnership : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject leftArmIK_target;
    [SerializeField] private GameObject rightArmIK_target;
    [SerializeField] private GameObject head_Constraint;
    [SerializeField] private GameObject inventorySocket;
    RealtimeTransform _realtimeTransform;
    RealtimeView _realtimeView;
    void Start()
    {
        _realtimeView = GetComponent<RealtimeView>();
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            SetOwnerShip();
        }
    }

    void SetOwnerShip()
    {
        _realtimeTransform = GetComponent<RealtimeTransform>();
        _realtimeTransform.RequestOwnership();
        characterController.GetComponent<RealtimeTransform>().RequestOwnership();

        inventorySocket.GetComponent<RealtimeTransform>().RequestOwnership();

        GameObject cameraOffset = transform.GetChild(0).gameObject;
        GameObject playerModel = transform.GetChild(1).gameObject;

        playerModel.GetComponent<RealtimeTransform>().RequestOwnership();
        playerModel.GetComponent<RealtimeView>().RequestOwnership();

        cameraOffset.GetComponent<RealtimeTransform>().RequestOwnership();
        cameraOffset.GetComponent<RealtimeView>().RequestOwnership();

        for (int i = 0; i < 5; i++)
        {
            GameObject cameraOffsetChild = cameraOffset.transform.GetChild(i).gameObject;
            if (i == 0)
            {
                cameraOffsetChild.SetActive(true);
            }
            cameraOffsetChild.GetComponent<RealtimeTransform>().RequestOwnership();
            cameraOffsetChild.GetComponent<RealtimeView>().RequestOwnership();
        } 

        leftArmIK_target.GetComponent<RealtimeTransform>().RequestOwnership();
        leftArmIK_target.GetComponent<RealtimeView>().RequestOwnership();
        rightArmIK_target.GetComponent<RealtimeTransform>().RequestOwnership();
        rightArmIK_target.GetComponent<RealtimeView>().RequestOwnership();
        head_Constraint.GetComponent<RealtimeTransform>().RequestOwnership();
        head_Constraint.GetComponent<RealtimeView>().RequestOwnership();
    }
}
