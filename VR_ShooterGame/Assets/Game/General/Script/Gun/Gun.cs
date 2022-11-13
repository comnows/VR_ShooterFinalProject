using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunInput gunInput;
    [SerializeField] private Camera playerCamera;

    private int damage = 10;
    private float fireRate = 10f;
    private float shotRange = 100;

    private float nextTimeToFire = 0f;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
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
                target.ReceiveAttack(damage);
            }
        }
    }
}
