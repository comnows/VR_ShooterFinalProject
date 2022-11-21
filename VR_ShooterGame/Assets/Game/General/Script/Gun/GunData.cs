using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ScriptableObject
{
    public new string name;
    public int startAmmo;
    public int magazineSize;

    public int bulletDamage;
    public float fireRate;
    public int currentStashAmmo;
    public int currentMagazineAmmo;
    public float reloadTime;

    // public bool isReload;

    public void Reload()
    {
        currentStashAmmo += currentMagazineAmmo;
        currentMagazineAmmo = Mathf.Min(currentStashAmmo, magazineSize);
        currentStashAmmo -= currentMagazineAmmo;
    }

}
