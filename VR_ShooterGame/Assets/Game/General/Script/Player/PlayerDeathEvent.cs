using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEvent : MonoBehaviour
{
//    public IEnumerator RespawnPlayer(GameObject player)
//    {
//     yield return new WaitForSeconds (3);
//     var positionToSpawn = GetComponent<RespawnPointData>()._respawnPointPos;
//     player.transform.position = positionToSpawn;
//     player.SetActive(true);
//    }

    public IEnumerator RespawnPlayers()
    {
    yield return new WaitForSeconds (3);
    Vector3 positionToSpawn = GetComponent<RespawnPointData>()._respawnPointPos;
    List<GameObject> playerList = FindObjectOfType<PlayerManager>()._playerList;
    foreach (GameObject player in playerList)
        {
        //GameObject playerObject = player.transform.GetChild(0).gameObject;
        //GameObject playerObject2 = player.transform.GetChild(1).gameObject;
        //if (!player.activeSelf)
        player.transform.position = positionToSpawn;
        player.GetComponent<PlayerMovement>().enabled = true;
        //player.GetComponent<PlayerLookMovement>().enabled = true;
        player.GetComponent<Gun>().enabled = true;
        //playerObject.SetActive(true);
        //.SetActive(true);
        //player.SetActive(true);
        }
    }
}
