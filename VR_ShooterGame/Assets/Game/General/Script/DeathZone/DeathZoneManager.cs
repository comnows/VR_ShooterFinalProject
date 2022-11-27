using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Normal.Realtime;

public class DeathZoneManager : MonoBehaviour
{
    //private RealtimeView _realtimeView;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        DeactivatePlayer(other.gameObject);
    }

    // private void DeactivatePlayer(GameObject player)
    // {
    //     player.SetActive(false);
    //     StartCoroutine(FindObjectOfType<PlayerDeathEvent>().RespawnPlayer(player));
    // }

    private void DeactivatePlayer(GameObject player)
    {
       // _realtimeView = player.GetComponent<RealtimeView>();

        //if (_realtimeView.isOwnedLocallyInHierarchy)
        //{
        player.GetComponent<PlayerMovement>().enabled = false;
        //player.GetComponent<PlayerLookMovement>().enabled = false;
        player.GetComponent<Gun>().enabled = false;
        //}
    }
}
