using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class CheckPointManager : MonoBehaviour
{
    private Vector3 currentRespawnPointPos;
    private CheckPointSyncData checkPointSyncData;
    private GameObject[] players;
    
    void Awake()
    {
        checkPointSyncData = GetComponent<CheckPointSyncData>();
        currentRespawnPointPos = checkPointSyncData._respawnPointPos;
        DontDestroyOnLoad(gameObject);
    }

    public void CheckUpdateCheckPoint(Vector3 enteredCheckPointPos)
    {
        currentRespawnPointPos = checkPointSyncData._respawnPointPos;
        Debug.Log("BeforePos = " + currentRespawnPointPos);
        if (currentRespawnPointPos != enteredCheckPointPos)
        {
            checkPointSyncData.SetNewRespawnPoint(enteredCheckPointPos);
            currentRespawnPointPos = checkPointSyncData._respawnPointPos; 
            Debug.Log("After Pos = " + currentRespawnPointPos);
        }
    }

    public void CheckRespawnPlayers()
    {
        StartCoroutine(RespawnPlayers());
    }

    public void CheckRespawnVRPlayers(GameObject player)
    {
        if (player.GetComponent<RealtimeView>().isOwnedLocallyInHierarchy)
        {
        StartCoroutine(RespawnVRPlayers(player));
        }
    }

    private IEnumerator RespawnPlayers()
    {
        yield return new WaitForSeconds (3);

        players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if (player.transform.GetComponent<PlayerStatus>() != null)
            {
            player.GetComponent<PlayerStatus>().RevivingPlayer();
            RealtimeTransform _realtimeTransform = player.GetComponent<RealtimeTransform>();
            _realtimeTransform.ClearOwnership();
            player.transform.position = currentRespawnPointPos;
            _realtimeTransform.RequestOwnership();
            }
        }
    }

    private IEnumerator RespawnVRPlayers(GameObject player)
    {
        yield return new WaitForSeconds (3);
        RealtimeTransform _realtimeTransform = player.GetComponent<RealtimeTransform>();
        _realtimeTransform.ClearOwnership();
        player.transform.position = currentRespawnPointPos;
        _realtimeTransform.RequestOwnership();

        GameObject inventorySockets = player.transform.GetChild(2).gameObject;
        GameObject arInventory = inventorySockets.transform.GetChild(0).gameObject;
        GameObject arMagazineInventory = inventorySockets.transform.GetChild(1).gameObject;

        GameObject arGun = GameObject.FindGameObjectWithTag("ARGun");
        GameObject arMagazine = GameObject.FindGameObjectWithTag("ARMagazine");

        arGun.GetComponent<RealtimeTransform>().ClearOwnership();
        arMagazine.GetComponent<RealtimeTransform>().ClearOwnership();

        arGun.transform.position = arInventory.transform.GetChild(0).gameObject.transform.position;
        arMagazine.transform.position = arMagazineInventory.transform.position;

        arGun.GetComponent<RealtimeTransform>().RequestOwnership();
        arMagazine.GetComponent<RealtimeTransform>().RequestOwnership();
    }
}
