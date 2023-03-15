using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEnterChecker : MonoBehaviour
{
    private CheckPointManager checkPointManager;
    private GameObject spawnPoint;
    private void Awake() 
    {
        checkPointManager = GameObject.Find("CheckPointManager").GetComponent<CheckPointManager>();
        spawnPoint =  this.gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            Debug.Log("EnterCheckPointChecker");
            checkPointManager.CheckUpdateCheckPoint(spawnPoint.transform.position);
            Destroy(this.gameObject);
        }
    }
}
