using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class DoorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject bossSpawnPoint;
    private GameObject boss;
    private void OnTriggerEnter(Collider other) {
        Debug.Log("MillTriggerSpawnLoveLove");
        if (other.tag == "Player" && GameObject.FindGameObjectsWithTag("Boss").Length == 0)
        {
            Debug.Log("MillSpawnLoveLove");
            Realtime realtime = GameObject.FindGameObjectWithTag("Realtime").GetComponent<Realtime>();
            var options = new Realtime.InstantiateOptions {
            ownedByClient            = true,    // Make sure the RealtimeView on this prefab is owned by this client.
            preventOwnershipTakeover = false,    // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
            useInstance              = realtime // Use the instance of Realtime that fired the didConnectToRoom event.
            };
            boss = Realtime.Instantiate(bossPrefab.name,options);
            boss.GetComponent<RealtimeTransform>().RequestOwnership();
            Invoke(nameof(ChangeBossPos),1);
            //boss.transform.position = bossSpawnPoint.transform.position;
        }
    }

    private void ChangeBossPos()
    {
        //boss.transform.parent = bossSpawnPoint.transform;
        Vector3 objectScale = boss.transform.localScale;
        boss.transform.localScale = new Vector3(objectScale.x*2,  objectScale.y*2, objectScale.z*2);
        //boss.transform.position = new Vector3(0,0,0);
        boss.transform.position = bossSpawnPoint.transform.position;
        //Invoke(nameof(ClearParent),2);
        //boss.transform.parent = null;
        //boss.GetComponent<RealtimeTransform>().RequestOwnership();
    }

    private void ClearParent()
    {
        boss.transform.parent = null;
    }

}
