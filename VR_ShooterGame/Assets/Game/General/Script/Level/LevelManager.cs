using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class LevelManager : MonoBehaviour
{
    private int prev;
    private int numPlayerInArea;

    //public GameObject spawnPoint;
    private void Start() 
    {
        prev = 1;
        numPlayerInArea = 0;
    }

    private void OnTriggerEnter(Collider other) {
        numPlayerInArea += 1;
        if (other.tag == "Player")
        {   
            UIScoreBoard uIScoreBoard = GameObject.Find("Canvas").GetComponent<UIScoreBoard>();
            uIScoreBoard.UpdateScoreBoard(); 

            var playersInGame = GameObject.FindGameObjectsWithTag("Player");
            if(playersInGame.Length == numPlayerInArea)
            {
                prev += 1;
                EnterNextLevel(other.gameObject);
                foreach(GameObject player in playersInGame)
                {
                    EnterNextLevel(player);
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            numPlayerInArea -= 1;
        }
    }

    private void EnterNextLevel(GameObject player)
    {
        player.GetComponent<RealtimeTransform>().ClearOwnership();
        Debug.Log("prev = " + prev);
        StartCoroutine(DeleyTeleport(player));
    }

    private IEnumerator DeleyTeleport(GameObject player)
    {
        yield return new WaitForSeconds(3);
        var spawnPoint = GameObject.Find("SpawnPointLV" + prev);
        player.transform.position = spawnPoint.transform.position;
        player.GetComponent<RealtimeTransform>().RequestOwnership();
        Debug.Log("LoveMill");
    }
}
