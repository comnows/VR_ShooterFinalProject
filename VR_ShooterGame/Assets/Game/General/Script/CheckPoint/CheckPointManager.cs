using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private RespawnPointData respawnPointData;   

    private void Awake() 
    {
        respawnPointData = FindObjectOfType<RespawnPointData>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            GameObject spawnPoint = this.gameObject.transform.GetChild(0).gameObject;
            respawnPointData.SetNewRespawnPoint(spawnPoint.transform.position);
            StartCoroutine(FindObjectOfType<PlayerDeathEvent>().RespawnPlayers());
            Destroy(this.gameObject);
        }
    }
}
