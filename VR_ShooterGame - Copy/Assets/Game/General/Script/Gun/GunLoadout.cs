using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLoadout : MonoBehaviour
{
    public GunData[] guns;

    private void Awake()
    {
        InitAllGuns(); //remove in real game, do this on start the new game only
    }

    private void InitAllGuns()
    {
        foreach(GunData gun in guns)
        {
            gun.Initialize();
        }
    }
}
