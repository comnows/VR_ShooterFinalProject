using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRGun : MonoBehaviour
{
    [SerializeField] private Transform barrelTransform;

    public GunData gunData;

    private float shotRange = 100f;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Shoot);
    }

    public void Shoot(ActivateEventArgs arg)
    {
        Debug.Log("Gun Shoot");
        RaycastHit hitInfo;
        if(Physics.Raycast(barrelTransform.position, barrelTransform.forward, out hitInfo, shotRange))
        {
            Debug.DrawRay(barrelTransform.position, barrelTransform.forward * shotRange, Color.red, 3);
            AttackTarget target = hitInfo.transform.GetComponent<AttackTarget>();
            Debug.Log(target);

            if (target != null)
            {
                target.ReceiveAttack(gunData.bulletDamage, this.gameObject);
            }
        }
    }
}
