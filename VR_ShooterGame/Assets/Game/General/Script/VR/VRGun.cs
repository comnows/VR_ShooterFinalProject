using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Normal.Realtime;
public class VRGun : MonoBehaviour
{
    [SerializeField] private VRGunEffect vrGunEffect;
    [SerializeField] private Transform barrelTransform;

    [SerializeField] private GameObject player;

    public GunData gunData;

    public VRGunMagazine magazine;
    public XRBaseInteractor socketInteractor;

    private float shotRange = 100f;

    private float nextTimeToFire = 0f;
    private bool isShoot = false;
    private bool isReload = false;
    private RealtimeView _realtimeView;
    private RealtimeTransform _realtimeTransform;
    private PlayerSyncData playerSyncData;
    public event Action OnGunShoot;

    private void Awake()
    {
        gunData = gunData.Clone();
        gunData.Initialize();

        // GunLoadout gunLoadout = GetComponentInParent<GunLoadout>();
        // Debug.Log("gunLoadout is " + gunLoadout);
        // gunLoadout.AddGun(gunData);

        _realtimeView = GetComponent<RealtimeView>();
        _realtimeTransform = GetComponent<RealtimeTransform>();

        // _realtimeView.RequestOwnership();
        // _realtimeTransform.RequestOwnership();

        //playerSyncData = player.GetComponent<PlayerSyncData>();
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
        _realtimeView.RequestOwnership();
        _realtimeTransform.RequestOwnership();
        
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
        // if (_realtimeView.isOwnedLocallyInHierarchy)
        // {
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
            // }

            nextTimeToFire -= Time.deltaTime;
        }

        // if (playerSyncData._playerIsCanShootGunEffect)
        // {
            //OnGunShoot?.Invoke();
            //playerSyncData.ChangeIsCanShootGunEffect(false);
        // }
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
        
        //playerSyncData.ChangeIsCanShootGunEffect(true);
        //OnGunShoot?.Invoke();
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
