using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : Interactable
{
    [SerializeField] private int stashAmmo = 30;

    public override void Interact(GameObject player)
    {
        Gun gun = player.GetComponent<Gun>();
        GunData gunData = gun.gunData;

        if(gunData.IsAmmoFull()) return;

        int stashSpace = (gunData.maxStashAmmo + gunData.magazineSize) - (gunData.currentStashAmmo + gunData.currentMagazineAmmo);

        if(stashSpace < stashAmmo)
        {
            gunData.AddAmmo(stashSpace);

            stashAmmo -= stashSpace;
            
            //ammo added event trigger for UI or else
        }
        else
        {
            gunData.AddAmmo(stashAmmo);

            stashAmmo -= stashAmmo;

            //ammo added event trigger for UI or else
        }

        gun.OnGunReload?.Invoke(gun.gunData.currentMagazineAmmo, gun.gunData.currentStashAmmo);

        if(stashAmmo <= 0)
        {
            Destroy(gameObject);
        }
    }
}
