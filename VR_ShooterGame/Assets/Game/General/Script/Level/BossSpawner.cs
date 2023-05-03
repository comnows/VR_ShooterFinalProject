using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject bossSpawnPoint;
    [SerializeField] private GameObject bossAreaManager;
    private GameObject boss;
    private int numPlayerInArea;
    
    private void Start() 
    {
        numPlayerInArea = 0;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && GameObject.FindGameObjectsWithTag("Boss").Length == 0)
        {
            numPlayerInArea += 1;

            if (other.GetComponent<RealtimeTransform>().isOwnedLocallySelf && numPlayerInArea == 2) 
            {
                Realtime realtime = GameObject.FindGameObjectWithTag("Realtime").GetComponent<Realtime>();
                var options = new Realtime.InstantiateOptions {
                ownedByClient            = true,    
                preventOwnershipTakeover = true,    
                useInstance              = realtime 
                };
                boss = Realtime.Instantiate(bossPrefab.name,options);
                boss.GetComponent<RealtimeView>().ClearOwnership();
                boss.GetComponent<RealtimeTransform>().RequestOwnership();
                boss.transform.position = bossSpawnPoint.transform.position;
                
                GameObject bossArea = Realtime.Instantiate(bossAreaManager.name,options);
                numPlayerInArea = -1;
            }
        }
    }
}
