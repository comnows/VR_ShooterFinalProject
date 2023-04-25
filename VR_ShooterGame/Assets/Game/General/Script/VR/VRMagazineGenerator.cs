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

    private VRGunMagazine newMagazine;

    void Start()
    {
        CreateNewMagazine();

        magazineInventory.hoverExited.AddListener(CreateNewMagazineOnHoverExited);
    }

    public void CreateNewMagazine()
    {
        CreateMagazine();
        SetupMagazine();
    }

    public void CreateMagazine()
    {
        newMagazine = Instantiate(magazinePrefab, createdTransform.position, Quaternion.identity);
    }

    public void SetupMagazine()
    {
        if(!CanReload()){return;}

        newMagazine.vrGun = vrGun;

        GunData gunData = vrGun.gunData;
        if(IsAmmoLimit())
        {
            newMagazine.bulletCount = Mathf.Min(gunData.magazineSize, gunData.currentStashAmmo);
            RemoveBulletFromStash(newMagazine.bulletCount);
        }
        else
        {
            newMagazine.bulletCount = gunData.magazineSize;
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

    // public void CreateNewMagazineOn

    public void CreateNewMagazineOnHoverExited(HoverExitEventArgs args)
    {
        if(args.interactorObject.hasHover){return;}

        // newMagazine = null;

        CreateNewMagazine();
    }

    public void AssignVRGun(GameObject gun)
    {
        vrGun = gun.GetComponent<VRGun>();
    }
}


