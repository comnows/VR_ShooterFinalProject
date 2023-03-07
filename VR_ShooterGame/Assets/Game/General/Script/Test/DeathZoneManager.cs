using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        DeactivatePlayer(other.gameObject);
    }

    private void DeactivatePlayer(GameObject player)
    {
        player.GetComponent<PlayerSyncData>().DecreasePlayerHP(100);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Gun>().enabled = false;
    }
}
