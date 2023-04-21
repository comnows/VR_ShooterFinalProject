using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRGun : MonoBehaviour
{
    [SerializeField] private VRGunEffect vrGunEffect;
    [SerializeField] private Transform barrelTransform;

    public GunData gunData;

    public VRGunMagazine magazine;
    public XRBaseInteractor socketInteractor;

    private float shotRange = 100f;

    private float nextTimeToFire = 0f;
    private bool isShoot = false;
    private bool isReload = false;

    public event Action OnGunShoot;

    private void Awake()
    {
        gunData = gunData.Clone();
        gunData.Initialize();

        GunLoadout gunLoadout = GetComponentInParent<GunLoadout>();
        Debug.Log("gunLoadout is " + gunLoadout);
        gunLoadout.AddGun(gunData);
    }

    private void OnEnable()
    {
        OnGunShoot += vrGunEffect.CastFireEffect;
    }

    private void OnDisable()
    {
        OnGunShoot -= vrGunEffect.CastFireEffect;
    }

    // Start is called before the first frame update
    void Start()
    {
        //XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        //grabbable.activated.AddListener(Shoot);

        socketInteractor.selectEntered.AddListener(AddMagazine);
        socketInteractor.selectExited.AddListener(RemoveMagazine);
    }

    public void AddMagazine(SelectEnterEventArgs args)
    {
        magazine = args.interactableObject.transform.GetComponent<VRGunMagazine>();
    }

    public void RemoveMagazine(SelectExitEventArgs args)
    {
        magazine = null;
    }

    public void ShootPressed(ActivateEventArgs arg)
    {
        isShoot = true;
    }

    public void ShootReleased(DeactivateEventArgs arg)
    {
        isShoot = false;
    }

    private void Update()
    {
        if(isShoot)
        {
            if(gunData.isAutoFire)
            {
                if(CanShoot())
                {
                    nextTimeToFire = 1f / gunData.fireRatePerSecond;

                    Shoot();
                }
            }
            else
            {
                isShoot = false;

                Shoot();
            }
        }

        nextTimeToFire -= Time.deltaTime;
    }

    public void Shoot()
    {
        Debug.Log("Gun Shoot");
        RaycastHit hitInfo;

        if(Physics.Raycast(barrelTransform.position, barrelTransform.forward, out hitInfo, shotRange))
        {
            Debug.DrawRay(barrelTransform.position, barrelTransform.forward * shotRange, Color.red, 3);
            AttackTarget target = hitInfo.transform.GetComponent<AttackTarget>();
            Debug.Log(target);

            if (target != null)
            {
                target.ReceiveAttack(gunData.bulletDamage, this.gameObject);
            }
        }

        RemoveBulletFromMagazine();
        OnGunShoot?.Invoke();
    }

    public void RemoveBulletFromMagazine()
    {
        magazine.bulletCount -= 1;
    }

    private bool CanShoot()
    {
        bool canShoot = nextTimeToFire <= 0 && magazine && magazine.bulletCount > 0;

        return canShoot;
    }
}
