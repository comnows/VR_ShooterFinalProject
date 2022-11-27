using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunInput gunInput;
    [SerializeField] private Camera playerCamera;

    private RealtimeView _realtimeView;

    private int damage = 10;
    private float fireRate = 10f;
    private float shotRange = 100;

    private float nextTimeToFire = 0f;

    private void Awake() 
    {
        _realtimeView = GetComponent<RealtimeView>();
    }

    void Update()
    {   
        if (_realtimeView.isOwnedLocallyInHierarchy)
            if(gunInput.ShootInput && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
    }

    void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, shotRange))
        {
            AttackTarget target = hitInfo.transform.GetComponent<AttackTarget>();
            Debug.Log(target);
            if (target != null)
            {
                target.ReceiveAttack(damage, this.gameObject);
            }
        }
    }
}
