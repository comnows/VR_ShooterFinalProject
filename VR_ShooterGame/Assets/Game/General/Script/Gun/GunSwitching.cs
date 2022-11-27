using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitching : MonoBehaviour
{
    private GunSwitchingInput gunSwitchingInput;
    //private GunLoadout gunLoadout;

    private int selectedGun = 0;

    private void Awake()
    {
        gunSwitchingInput = GetComponent<GunSwitchingInput>();
        //gunLoadout = GetComponent<GunLoadout>();
    }

    // Update is called once per frame
    void Update()
    {
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
            // selectedGun %= gunLoadout.guns.Length;
            selectedGun %= 2;

            Debug.Log("Selected gun = " + selectedGun);
        }

        if(gunSwitchingInput.ScrollSwitchInput < 0f)
        {
            selectedGun--;

            if(selectedGun < 0)
            {
                // selectedGun = gunLoadout.guns.Length - 1;
                selectedGun = 1;
            }

            Debug.Log("Selected gun = " + selectedGun);
        }
    }

    // private void SelectGun()
    // {
    //     int i = 0;

    //     foreach(GunData gun in gunLoadout.guns)
    //     {
    //         if(i == selectedGun)
    //         {
    //             //destroy current gun

    //             //create selected gun
    //         }

    //         i++;
    //     }
    // }
}
