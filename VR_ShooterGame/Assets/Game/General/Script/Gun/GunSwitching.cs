using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class GunSwitching : MonoBehaviour
{
//     public event Action OnGunSwitch;

//     private GunSwitchingInput gunSwitchingInput;
//     private GunLoadout gunLoadout;
//     private Gun gun;
//     private GunEffect gunEffect;

//     [SerializeField] private Animator rigControllerAnimator;
//     //private AnimatorOverrideController animatorOverrideController;

//     public Transform weaponLeftGrip;
//     public Transform weaponRightGrip;

//     [SerializeField] private Transform gunHolder;

//     private int selectedGun = 0;

//     private void Awake()
//     {
//         gunSwitchingInput = GetComponent<GunSwitchingInput>();
//         gunLoadout = GetComponent<GunLoadout>();
//         gun = GetComponent<Gun>();

//         // animator = GetComponentInChildren<Animator>();
//         // animatorOverrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         int previousSelectedGun = selectedGun;
//         if(gunSwitchingInput.gunSwitchingControlsActions.PrimaryWeapon.triggered)
//         {
//             selectedGun = 0;
//         }

//         if(gunSwitchingInput.gunSwitchingControlsActions.SecondaryWeapon.triggered)
//         {
//             selectedGun = 1;
//         }

//         if(gunSwitchingInput.ScrollSwitchInput > 0f)
//         {
//             selectedGun++;
//             selectedGun %= gunLoadout.guns.Length;
//             // selectedGun %= 2;

//             Debug.Log("Selected gun = " + selectedGun);
//         }

//         if(gunSwitchingInput.ScrollSwitchInput < 0f)
//         {
//             selectedGun--;

//             if(selectedGun < 0)
//             {
//                 selectedGun = gunLoadout.guns.Length - 1;
//                 // selectedGun = 1;
//             }

//             Debug.Log("Selected gun = " + selectedGun);
//         }

//         if(previousSelectedGun != selectedGun)
//         {
//             SelectGun();
//         }
//     }

//     private void SelectGun()
//     {
//         int gunLoadoutIndex = 0;

//         foreach(GunData gunData in gunLoadout.guns)
//         {
//             if(gunLoadoutIndex == selectedGun)
//             {
//                 //destroy current gun
//                 // GameObject destroyedGun = gunHolder.GetChild(0).gameObject;
//                 // Destroy(destroyedGun);

//                 //create selected gun
//                 GameObject createdGun = Instantiate(gunData.prefab);
//                 EquipWeapon(createdGun, gunData);
//                 // gun.currentGun = createdGun;
//                 // gun.gunData = gunData;

//                 OnGunSwitch?.Invoke();
//             }

//             gunLoadoutIndex++;
//         }
//     }

//     private void EquipWeapon(GameObject newGun, GunData newGunData)
//     {
//         DestroyGun();

//         SetupNewGun(newGun, newGunData);

//         SetGunTransform();

//         SetupNewGunEffects(newGun);

//         // Invoke(nameof(SetAnimationDelayed), 0.001f);
//         //PlayEquipAnimation();
//     }

//     private void DestroyGun()
//     {
//         if(gun.currentGun)
//         {
//             Destroy(gun.currentGun);
//         }
//     }

//     private void SetupNewGun(GameObject newGun, GunData newGunData)
//     {
//         gun.currentGun = newGun;
//         gun.gunData = newGunData;
//     }

//     private void SetGunTransform()
//     {
//         gun.currentGun.transform.parent = gun.weaponHolder.transform;
//         gun.currentGun.transform.localPosition = Vector3.zero;
//         gun.currentGun.transform.localRotation = Quaternion.identity;
//     }

//     private void SetupNewGunEffects(GameObject newGun)
//     {
//         Transform raycastOrigin = newGun.transform.Find("RaycastOrigin");
//         gunEffect.SetRaycastOrigin(raycastOrigin);

//         Transform effectsTransform = newGun.transform.Find("Effects");
//         ParticleSystem muzzleFlash = effectsTransform.Find("MuzzleFlash").GetComponent<ParticleSystem>();
//         ParticleSystem hitEffect = effectsTransform.Find("HitEffect").GetComponent<ParticleSystem>();
//         gunEffect.SetGunEffect(muzzleFlash, hitEffect);
//     }

//     private void PlayEquipAnimation()
//     {
//         rigControllerAnimator.Play(gun.gunData.name + "_Equip");
//     }

//     // private void SetAnimationDelayed()
//     // {
//     //     animatorOverrideController["AssualtRifle_Hold"] = gun.gunData.gunHoldAnimation;
//     // }

//     // [ContextMenu("Save weapon pose")]
//     // private void SaveWeaponPose()
//     // {
//     //     GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
//     //     recorder.BindComponentsOfType<Transform>(gun.weaponHolder, false);
//     //     recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
//     //     recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
//     //     recorder.TakeSnapshot(0.0f);
//     //     recorder.SaveToClip(gun.gunData.gunHoldAnimation);
//     //     UnityEditor.AssetDatabase.SaveAssets();
//     // }
}
