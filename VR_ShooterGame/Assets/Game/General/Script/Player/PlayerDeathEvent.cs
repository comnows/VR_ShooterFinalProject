using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class PlayerDeathEvent : MonoBehaviour
{
    private RealtimeView _realtimeView;

    void Awake() {
        _realtimeView = GetComponent<RealtimeView>(); 
    }
    public void CheckRespawnPlayers()
    {
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            StartCoroutine(RespawnPlayers());
        }
    }

    private IEnumerator RespawnPlayers()
    {
        yield return new WaitForSeconds (3);

        if (this.GetComponent<PlayerMovement>().enabled == false)
        {
            Vector3 positionToSpawn = FindObjectOfType<RespawnPointData>()._respawnPointPos;
            this.GetComponent<PlayerMovement>().enabled = true;
            this.GetComponent<Gun>().enabled = true;
            this.transform.position = positionToSpawn;
        }
    }
}
