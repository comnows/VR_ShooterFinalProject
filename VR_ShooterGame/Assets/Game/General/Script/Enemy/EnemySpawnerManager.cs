using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class EnemySpawnerManager : MonoBehaviour
{
    void Awake() {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        foreach(Enemy enemy in enemies)
        {
            enemy.OnEnemyDie += SpawnNewEnemy;
        }
    }
    public void SpawnNewEnemy(Vector3 enemy)
    {
        StartCoroutine(DelaySpawnNewEnemy(enemy));
    }

    IEnumerator DelaySpawnNewEnemy(Vector3 enemy)
    {
        yield return new WaitForSeconds (2);
        var options = new Realtime.InstantiateOptions 
        {
            ownedByClient = true,    
            preventOwnershipTakeover = true,    
        };
        GameObject enemyGameObject = Realtime.Instantiate("Enemy",options);
        enemyGameObject.GetComponent<RealtimeTransform>().RequestOwnership();
        enemyGameObject.transform.position = enemy;
        enemyGameObject.GetComponent<Enemy>().OnEnemyDie += SpawnNewEnemy;
    }
}
