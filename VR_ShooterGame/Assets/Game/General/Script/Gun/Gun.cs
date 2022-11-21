using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunInput gunInput;
    [SerializeField] private Camera playerCamera;

    public GunData gunData;

    private int damage = 10;
    private float fireRate = 10f;
    private float shotRange = 100;

    private float nextTimeToFire = 0f;
    private bool isReload = false;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if(gunInput.ShootInput && Time.time >= nextTimeToFire && gunData.currentMagazineAmmo > 0)
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

        gunData.currentMagazineAmmo--;
    }

    IEnumerator Reload()
    {
        isReload = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.Reload();

        isReload = false;
    }

    void SetGunProperties()
    {
        damage = gunData.bulletDamage;
        fireRate = gunData.fireRate;

    }
}
