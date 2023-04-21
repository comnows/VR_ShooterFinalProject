using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRGunMagazine : MonoBehaviour
{
    public int bulletCount = 30;

    public XRGrabInteractable gunMagazineGrabInteractable;
    public VRGun vrGun;

    private IEnumerator coroutine;

    private void Awake()
    {
        gunMagazineGrabInteractable = GetComponent<XRGrabInteractable>();
        coroutine = DestroyMagazineIn(5f);
    }

    private void Start()
    {
        gunMagazineGrabInteractable.hoverEntered.AddListener(CancelDestroyMagazineOnHoverEntered);
        gunMagazineGrabInteractable.hoverExited.AddListener(DestroyMagazineOnHoverExited);
    }

    public void CancelDestroyMagazineOnHoverEntered(HoverEnterEventArgs args)
    {
        StopCoroutine(coroutine);
    }

    public void DestroyMagazineOnHoverExited(HoverExitEventArgs args)
    {
        if(args.interactableObject.isHovered)
        {
            Debug.Log("is hovered by something");
            return;
        }

        Debug.Log("isnt hover by anything and start coroutine");

        StartCoroutine(coroutine);
    }

    IEnumerator DestroyMagazineIn(float timeInSecond)
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
