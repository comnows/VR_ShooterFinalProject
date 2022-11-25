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

    void Start()
    {
        gunData.Initialize();
    }

    void Update()
    {
        if(gunInput.ShootInput && CanShoot())
        {
            nextTimeToFire = Time.time + 1f / gunData.fireRatePerSecond;

            Shoot();
        }

        if(gunInput.reloadAction.triggered && CanReload())
        {
            Debug.Log("Gun Reload");
            StartCoroutine(Reload());
        }
    }

    void DoShoot()
    {
        
    }

    bool CanShoot()
    {
        bool canShoot = Time.time >= nextTimeToFire && gunData.currentMagazineAmmo > 0 && !isReload;

        return canShoot;
    }

    bool CanReload()
    {
        bool canReload = !isReload && gunData.currentMagazineAmmo < gunData.magazineSize && gunData.currentStashAmmo > 0;

        return canReload;
    }

    void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, shotRange))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * shotRange, Color.red, 3);
            AttackTarget target = hitInfo.transform.GetComponent<AttackTarget>();
            Debug.Log(target);

            if (target != null)
            {
                target.ReceiveAttack(gunData.bulletDamage);
            }
        }

        gunData.RemoveCurrentMagazineAmmo();
    }

    IEnumerator Reload()
    {
        isReload = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.Reload();

        isReload = false;
    }

    void CancelReload()
    {
        StopCoroutine(Reload());

        isReload = false;
    }
}
