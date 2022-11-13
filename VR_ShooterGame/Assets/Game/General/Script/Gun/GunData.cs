using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ScriptableObject
{
    public new string name;
    public int bulletDamage;
    public float fireRate;
    public int ammo;
    public int magazineSize;

}
