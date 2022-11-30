using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitching : MonoBehaviour
{
    private GunSwitchingInput gunSwitchingInput;
    private GunLoadout gunLoadout;

    [SerializeField] private Transform gunHolder;

    private int selectedGun = 0;

    private void Awake()
    {
        gunSwitchingInput = GetComponent<GunSwitchingInput>();
        gunLoadout = GetComponent<GunLoadout>();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedGun = selectedGun;
        if(gunSwitchingInput.gunSwitchingControlsActions.PrimaryWeapon.triggered)
        {
            selectedGun = 0;
        }

        if(gunSwitchingInput.gunSwitchingControlsActions.SecondaryWeapon.triggered)
        {
            selectedGun = 1;
        }

        if(gunSwitchingInput.ScrollSwitchInput > 0f)
        {
            selectedGun++;
            selectedGun %= gunLoadout.guns.Length;
            // selectedGun %= 2;

            Debug.Log("Selected gun = " + selectedGun);
        }

        if(gunSwitchingInput.ScrollSwitchInput < 0f)
        {
            selectedGun--;

            if(selectedGun < 0)
            {
                selectedGun = gunLoadout.guns.Length - 1;
                // selectedGun = 1;
            }

            Debug.Log("Selected gun = " + selectedGun);
        }

        if(previousSelectedGun != selectedGun)
        {
            SelectGun();
        }
    }

    private void SelectGun()
    {
        int gunLoadoutIndex = 0;

        foreach(GunData gun in gunLoadout.guns)
        {
            if(gunLoadoutIndex == selectedGun)
            {
                //destroy current gun
                GameObject destroyedGun = gunHolder.GetChild(0).gameObject;
                Destroy(destroyedGun);

                //create selected gun
                GameObject createdGun = Instantiate(gun.prefab, gunHolder.position, gunHolder.rotation, gunHolder);
            }

            gunLoadoutIndex++;
        }
    }
}
