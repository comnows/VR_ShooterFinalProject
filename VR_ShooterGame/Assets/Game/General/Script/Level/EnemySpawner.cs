using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class EnemySpawner : MonoBehaviour
{
    Realtime.InstantiateOptions options;
    [SerializeField] private GameObject normalEnemyPrefab;
    [SerializeField] private GameObject shieldEnemyPrefab;
    [SerializeField] private GameObject shootingEnemyPrefab;
    [SerializeField] private GameObject redRoomEnemyPrefab;
    private int numPlayerInArea;

    private void Start() 
    {
        numPlayerInArea = 0;
    }
    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Player" && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            numPlayerInArea += 1;
            if (other.GetComponent<RealtimeTransform>().ownerIDInHierarchy == 0 && numPlayerInArea == 2)
            {
                Realtime realtime = GameObject.FindGameObjectWithTag("Realtime").GetComponent<Realtime>();
                options = new Realtime.InstantiateOptions {
                ownedByClient            = false,    
                preventOwnershipTakeover = false,    
                useInstance              = realtime 
                };

                SpawnNormalEnemies();
                SpawnShootingEnemies();
                SpawnShieldEnemis();
                SpawnRedRoomEnemies();

                numPlayerInArea = -1;
            }
        }
    }

    private void SpawnNormalEnemies()
    {
        GameObject [] normalEnemiesSPos = GameObject.FindGameObjectsWithTag("NEPos");
        foreach(GameObject spawnpos in normalEnemiesSPos)
        {
            GameObject normalEnemy = Realtime.Instantiate(normalEnemyPrefab.name,options);
            normalEnemy.GetComponent<RealtimeTransform>().RequestOwnership();
            normalEnemy.transform.position = spawnpos.transform.position;
            normalEnemy.transform.rotation = spawnpos.transform.rotation;
        }
    }

    private void SpawnShootingEnemies()
    {
        GameObject [] shootingEnemiesSPos = GameObject.FindGameObjectsWithTag("STEPos");
        foreach(GameObject spawnpos in shootingEnemiesSPos)
        {
            GameObject shootingEnemy = Realtime.Instantiate(shootingEnemyPrefab.name,options);
            shootingEnemy.GetComponent<RealtimeTransform>().RequestOwnership();
            shootingEnemy.transform.position = spawnpos.transform.position;
            shootingEnemy.transform.rotation = spawnpos.transform.rotation;
        }
    }

    private void SpawnShieldEnemis()
    {
        GameObject [] shieldEnemiesSPos = GameObject.FindGameObjectsWithTag("SHEPos");
        foreach(GameObject spawnpos in shieldEnemiesSPos)
        {
            GameObject shieldEnemy = Realtime.Instantiate(shieldEnemyPrefab.name,options);
            shieldEnemy.GetComponent<RealtimeTransform>().RequestOwnership();
            shieldEnemy.transform.position = spawnpos.transform.position;
            shieldEnemy.transform.rotation = spawnpos.transform.rotation;
        }
    }

    private void SpawnRedRoomEnemies()
    {
        GameObject [] normalRedRoomEnemiesSPos = GameObject.FindGameObjectsWithTag("NERPos");
        foreach(GameObject spawnpos in normalRedRoomEnemiesSPos)
        {
            GameObject redRoomEnemy = Realtime.Instantiate(redRoomEnemyPrefab.name,options);
            redRoomEnemy.GetComponent<RealtimeTransform>().RequestOwnership();
            redRoomEnemy.transform.position = spawnpos.transform.position;
            redRoomEnemy.transform.rotation = spawnpos.transform.rotation;
        }
    }
}
