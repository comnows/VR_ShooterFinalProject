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
        //prev = 1;
        numPlayerInArea = 0;
        LV = 1;
        isEnterOtherMap = false;
    }

    // private void OnTriggerEnter(Collider other) {
    //     numPlayerInArea += 1;
    //     if (other.tag == "Player")
    //     {   
    //         // UIScoreBoard uIScoreBoard = GameObject.Find("Canvas").GetComponent<UIScoreBoard>();
    //         // uIScoreBoard.UpdateScoreBoard(); 

    //         var playersInGame = GameObject.FindGameObjectsWithTag("Player");
    //         if(playersInGame.Length == numPlayerInArea)
    //         {
    //             prev += 1;
    //             EnterNextLevel(other.gameObject);
    //             foreach(GameObject player in playersInGame)
    //             {
    //                 EnterNextLevel(player);
    //             }
    //         }
    //     }
    // }
    
    // private void OnTriggerExit(Collider other) {
    //     if (other.tag == "Player")
    //     {
    //         numPlayerInArea -= 1;
    //     }
    // }

    // private void EnterNextLevel(GameObject player)
    // {
    //     player.GetComponent<RealtimeTransform>().ClearOwnership();
    //     Debug.Log("prev = " + prev);
    //     StartCoroutine(DeleyTeleport(player));
    // }

    // private IEnumerator DeleyTeleport(GameObject player)
    // {
    //     yield return new WaitForSeconds(3);
    //     var spawnPoint = GameObject.Find("SpawnPointLV" + prev);
    //     player.transform.position = spawnPoint.transform.position;
    //     player.GetComponent<RealtimeTransform>().RequestOwnership();
    // }

    private void OnTriggerEnter(Collider other) 
    {
        numPlayerInArea += 1;
        if (other.tag == "Player")
        {   
            // UIScoreBoard uIScoreBoard = GameObject.Find("Canvas").GetComponent<UIScoreBoard>();
            // uIScoreBoard.UpdateScoreBoard(); 
            var playersInGame = GameObject.FindGameObjectsWithTag("Player");
            if(playersInGame.Length == numPlayerInArea)
            {
                Debug.Log("MillInCondition");

                //prev += 1;
                //EnterNextLevel(other.gameObject);
                foreach(GameObject player in playersInGame)
                {
                    //EnterNextLevel(player);
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
            EnterNextLevel(other.gameObject);
        }
    }

    private void EnterNextLevel(GameObject player)
    {
        Debug.Log("MillEnterMyRoom");
        //player.GetComponent<RealtimeTransform>().ClearOwnership();
        //Debug.Log("prev = " + prev);
        //DontDestroyOnLoad(player);
        //player.GetComponent<PlayerSyncData>().PutInDontDestroy();
        //StartCoroutine(DeleyChangePosition(player));
        //realtimeObj.GetComponent<Realtime>().Connect("Map2");
        //Debug.Log("RoomName = " + realtimeObj.GetComponent<Realtime>().room.name);
        //player.GetComponent<Realtime>().room.name;
        SceneManager.LoadScene(1);
        //StartCoroutine(DeleyChangePosition(player));
        //player.transform.position = GameObject.Find("SpawnPointLV" + LV).transform.position;
        //LV += 1;
        //DontDestroyOnLoad(player);
       // _realtime.Connect("MapLV2");
    }

    IEnumerator DeleyChangePosition(GameObject player)
    {
        yield return new WaitForSeconds(1);
        player.transform.position = GameObject.Find("SpawnPointLV" + LV).transform.position;
        LV += 1;
    }

    // private void OnTriggerExit(Collider other) {
    //     numPlayerInArea -= 1;
    // }
}
