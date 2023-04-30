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
    [SerializeField] private GameObject normalEnemySpawnPoint;
    [SerializeField] private GameObject shieldEnemySpawnPoint;
    [SerializeField] private GameObject shootingEnemySpawnPoint;
    private GameObject normalEnemy;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Debug.Log("MillSpawnLoveLove");
            Realtime realtime = GameObject.FindGameObjectWithTag("Realtime").GetComponent<Realtime>();
            options = new Realtime.InstantiateOptions {
            ownedByClient            = true,    // Make sure the RealtimeView on this prefab is owned by this client.
            preventOwnershipTakeover = false,    // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
            useInstance              = realtime // Use the instance of Realtime that fired the didConnectToRoom event.
            };
            normalEnemy = Realtime.Instantiate(normalEnemyPrefab.name,options);
            normalEnemy.GetComponent<RealtimeTransform>().RequestOwnership();
            //normalEnemy.transform.position = normalEnemySpawnPoint.transform.position;

            Invoke(nameof(ChangeBossPos),1);
        }
    }

    private void ChangeBossPos()
    {
        normalEnemy.transform.position = normalEnemySpawnPoint.transform.position;
        // boss.transform.parent = bossSpawnPoint.transform;
        // boss.GetComponent<RealtimeTransform>().RequestOwnership();
    }
}
