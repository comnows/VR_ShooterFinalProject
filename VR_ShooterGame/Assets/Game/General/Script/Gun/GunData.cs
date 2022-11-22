using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ScriptableObject
{
    public new string name;
    public int startAmmo;
    public int magazineSize;

    public int bulletDamage;
    public float fireRatePerSecond;
    public float reloadTime;
    
    [HideInInspector] public int currentStashAmmo;
    [HideInInspector] public int currentMagazineAmmo;

    // public bool isReload;

    public void Initialize()
    {
        currentStashAmmo = startAmmo;
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

}
