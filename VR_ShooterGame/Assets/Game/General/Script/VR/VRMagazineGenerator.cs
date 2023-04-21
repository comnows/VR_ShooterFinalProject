using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMagazineGenerator : MonoBehaviour
{
    public VRGunMagazine magazinePrefab;
    public XRSocketInteractorTag magazineInventory;

    public VRGun vrGun;

    public Transform createdTransform;

    void Start()
    {
        CreateNewMagazine();

        magazineInventory.selectExited.AddListener(CreateNewMagazineOnSelectExited);
    }

    public void CreateNewMagazine()
    {
        VRGunMagazine newMagazine = CreateMagazine();
        SetupMagazine(newMagazine);
    }

    public VRGunMagazine CreateMagazine()
    {
        VRGunMagazine newMagazine = Instantiate(magazinePrefab, createdTransform.position, Quaternion.identity);

        return newMagazine;
    }

    public void SetupMagazine(VRGunMagazine magazine)
    {
        if(!CanReload()){return;}

        magazine.vrGun = vrGun;

        GunData gunData = vrGun.gunData;
        if(IsAmmoLimit())
        {
            magazine.bulletCount = Mathf.Min(gunData.magazineSize, gunData.currentStashAmmo);
            RemoveBulletFromStash(magazine.bulletCount);
        }
        else
        {
            magazine.bulletCount = gunData.magazineSize;
        }
    }

    public bool CanReload()
    {
        if(IsAmmoLimit())
        {
            return vrGun.gunData.currentStashAmmo > 0;
        }

        return true;
    }

    public bool IsAmmoLimit()
    {
        return vrGun.gunData.isAmmoLimited;
    }

    public void RemoveBulletFromStash(int amount)
    {
        vrGun.gunData.currentStashAmmo -= amount;
    }

    public void CreateNewMagazineOnSelectExited(SelectExitEventArgs args)
    {
        if(magazineInventory.hasSelection){return;}

        CreateNewMagazine();
    }

}
