using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerStatus : MonoBehaviour
{
    Animator animator;
    Rig rig;
    PlayerSyncData playerSyncData;
    private GameObject weaponCamera,rigObj;
    public int playerHP;
    [SerializeField] private GameObject helpMeText;
    private void Start()
    {
        playerSyncData = GetComponent<PlayerSyncData>();

        GameObject nonVRController = transform.GetChild(0).gameObject;
        GameObject cameraHolder = nonVRController.transform.GetChild(1).gameObject;
        GameObject soldierModel = nonVRController.transform.GetChild(3).gameObject;
        GameObject cameraRecoil = cameraHolder.transform.GetChild(0).gameObject;
        weaponCamera = cameraRecoil.transform.GetChild(0).gameObject;
        rigObj = soldierModel.transform.GetChild(4).gameObject;

        rig = rigObj.GetComponent<Rig>(); 

        animator = soldierModel.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        CheckPlayerHP();
        CheckPlayerStatus();
    }

    private void CheckPlayerHP()
    {
        playerHP = playerSyncData._playerHP;
    }

    private void CheckPlayerStatus()
    {
        if (playerHP <= 0)
        {
            ChangePlayerStatusToDead();
        }
        else
        {
            ChangePlayerStatusToAlive();    
        }
    }

    private void ChangePlayerStatusToDead()
    {
        weaponCamera.SetActive(false);
        rig.weight = 0;
        animator.SetBool("IsDead",true);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerLookMovement>().enabled = false;
        GetComponent<Gun>().enabled = false;
        helpMeText.SetActive(true);
    }

    private void ChangePlayerStatusToAlive()
    {
        animator.SetBool("IsDead",false);
        weaponCamera.SetActive(true);
        rig.weight = 100;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerLookMovement>().enabled = true;
        GetComponent<Gun>().enabled = true;
        helpMeText.SetActive(false);
    }

    public void RevivingPlayer()
    {
        playerSyncData.AddPlayerHP(200);
    }
}
