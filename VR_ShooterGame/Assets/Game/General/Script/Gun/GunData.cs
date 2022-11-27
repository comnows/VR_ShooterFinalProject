using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ScriptableObject
{
    public new string name;
    public int maxStashAmmo;
    public int magazineSize;

    public int bulletDamage;
    public float fireRatePerSecond;
    public float reloadTime;
    
    // [HideInInspector] 
    public int currentStashAmmo;
    // [HideInInspector] 
    public int currentMagazineAmmo;

    // public bool isReload;

    public void Initialize()
    {
        currentStashAmmo = maxStashAmmo;
        currentMagazineAmmo = magazineSize;
    }

    public void RemoveCurrentMagazineAmmo()
    {
        currentMagazineAmmo--;
    }

    public void Reload()
    {
        currentStashAmmo += currentMagazineAmmo;
        currentMagazineAmmo = Mathf.Min(currentStashAmmo, magazineSize);
        currentStashAmmo -= currentMagazineAmmo;
    }

    public void AddAmmo(int ammoAmount)
    {
        currentStashAmmo += ammoAmount;
        Debug.Log("Ammo added");
    }

    public bool IsAmmoFull()
    {
        return currentStashAmmo + currentMagazineAmmo >= maxStashAmmo + magazineSize;
    }

}
