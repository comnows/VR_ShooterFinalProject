using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRGunMagazine : MonoBehaviour
{
    public int bulletCount = 30;

    public XRGrabInteractable gunMagazineGrabInteractable;
    public VRGun vrGun;

    private void Awake()
    {
        gunMagazineGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        gunMagazineGrabInteractable.selectEntered.AddListener(CancelDestroyMagazineOnSelectEntered);
        gunMagazineGrabInteractable.selectExited.AddListener(DestroyMagazineOnSelectExited);
    }

    public void CancelDestroyMagazineOnSelectEntered(SelectEnterEventArgs args)
    {
        StopCoroutine(DestroyMagazineIn(5));
    }

    public void DestroyMagazineOnSelectExited(SelectExitEventArgs args)
    {
        if(args.interactableObject.isSelected){return;}

        StartCoroutine(DestroyMagazineIn(5));
    }

    public IEnumerator DestroyMagazineIn(float timeInSecond)
    {
        yield return new WaitForSeconds(timeInSecond);

        AddBulletToGunData();

        Destroy(this.gameObject);
    }

    public void AddBulletToGunData()
    {
        if(bulletCount <= 0){return;}
        if(vrGun.gunData.currentStashAmmo >= vrGun.gunData.maxStashAmmo){return;}

        int stashAmmo = vrGun.gunData.currentStashAmmo + bulletCount;
        vrGun.gunData.currentStashAmmo = Mathf.Min(stashAmmo, vrGun.gunData.maxStashAmmo);
    }
}
