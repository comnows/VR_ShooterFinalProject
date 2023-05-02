using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    //private int prev;
    private int numPlayerInArea;
    private int LV;
    private bool isEnterOtherMap;
    [SerializeField] private GameObject realtimeObj;
    // //public GameObject spawnPoint;
    private void Start() 
    {
        numPlayerInArea = 0;
        isEnterOtherMap = false;
    }

    
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            numPlayerInArea -= 1;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        numPlayerInArea += 1;
        if (other.tag == "Player")
        {   
            var playersInGame = GameObject.FindGameObjectsWithTag("Player");
            if(playersInGame.Length == numPlayerInArea)
            {
                Debug.Log("MillInCondition");

                foreach(GameObject player in playersInGame)
                {

                    player.GetComponent<PlayerSyncData>().ChangedIsCanEnterNextLV(true);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player" && other.GetComponent<PlayerSyncData>()._playerIsCanEnterNextLV && !isEnterOtherMap)
        {
            Debug.Log("MillEnterMyRoom");
            isEnterOtherMap = true;
            ActivateBlackBG();
            var playersInGame = GameObject.FindGameObjectsWithTag("Player");
            other.GetComponent<PlayerSyncData>().ChangedIsCanEnterNextLV(false);
            EnterNextLevel(other.gameObject);
        }
    }

    private void EnterNextLevel(GameObject player)
    {
        isEnterOtherMap = false;
        numPlayerInArea = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ActivateBlackBG()
    {
        GameObject.Find("Canvas").GetComponent<LevelChangingEffect>().ActiveBlackBG();
    }
}
