using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlayerVRStatus : MonoBehaviour
{
    PlayerSyncData playerSyncData;
    public int playerHP;
    [SerializeField] private GameObject helpMeText;
    //[SerializeField] private GameObject weaponModel;
    RealtimeView _realTimeView; 
    private void Start()
    {
        playerSyncData = GetComponent<PlayerSyncData>();
        _realTimeView = GetComponent<RealtimeView>();
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
        //GetComponent<>().enabled = false;
        GetComponent<PlayerLookMovement>().enabled = false;
        GetComponent<Gun>().enabled = false;
        helpMeText.SetActive(true);
    }

    private void ChangePlayerStatusToAlive()
    {
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
