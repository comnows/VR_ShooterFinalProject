using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Normal.Realtime;
public class BossArea : MonoBehaviour
{
    private Realtime _realtime;
    private RealtimeTransform _realtimeTransform;
    public GameObject bossHPUI;
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private GameObject [] spawnPoints;
    private bool canCheckSpawnEnemy, isSpawning;
    private GameObject [] allPlayerInGame;

    private void Start()
    {
        _realtime = GetComponent<Realtime>();
    }
    private void Update() 
    {
        if (canCheckSpawnEnemy && !isSpawning)
        {
            CheckSpawnEnemy();
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            //bossHPUI.SetActive(true);
            canCheckSpawnEnemy = true;
        }
    }

    private void CheckSpawnEnemy()
    {
        GameObject enemyInBossArea = GameObject.FindGameObjectWithTag("BossGuard");
        
        if (enemyInBossArea == null)
        {
            isSpawning = true;
            Invoke(nameof(SpawnEnemy),3);
        }
    }

    private void SpawnEnemy()
    {
        allPlayerInGame = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 1; i <= allPlayerInGame.Length * 2; i++)
        {
            var options = new Realtime.InstantiateOptions {
            ownedByClient            = true,   
            preventOwnershipTakeover = true,    
            useInstance              = _realtime 
            };

            GameObject bossGuard = Realtime.Instantiate(enemyToSpawn.name, options);
            bossGuard.tag = "BossGuard";
            _realtimeTransform = bossGuard.GetComponent<RealtimeTransform>();
            _realtimeTransform.SetOwnership(0);
            bossGuard.transform.position = spawnPoints[i+1].transform.position;
            SetTarget(bossGuard);
        }

        isSpawning = false;
    }

    private void SetTarget(GameObject bossGuard)
    {
        allPlayerInGame = GameObject.FindGameObjectsWithTag("Player");
        int randNum = Random.Range(0,allPlayerInGame.Length - 1);
        EnemyBehaviorStateManager enemyBehaviorStateManager = bossGuard.GetComponent<EnemyBehaviorStateManager>();
        if (enemyBehaviorStateManager != null) 
        {
            if (enemyBehaviorStateManager.player == null)
            {
                enemyBehaviorStateManager.SetTarget(allPlayerInGame[randNum]);
            }
        }
    }
}
