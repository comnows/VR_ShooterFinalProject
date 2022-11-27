using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Inventory Gun Data")]
public class InventoryGunData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
}
