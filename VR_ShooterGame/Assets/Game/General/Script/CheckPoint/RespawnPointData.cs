using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class RespawnPointData : RealtimeComponent<RespawnPointDataModel>
{
    public Vector3 _respawnPointPos;

    private void Awake() 
    {
        _respawnPointPos = GameObject.Find("SpawnPoint_1").transform.position;
    }

    protected override void OnRealtimeModelReplaced(RespawnPointDataModel previousModel, RespawnPointDataModel currentModel) 
    {
        if (previousModel != null) {
            previousModel.respawnPointPosDidChange -= RespawnPointDidChange;
        }
        
        if (currentModel != null) {
            if (currentModel.isFreshModel)
                currentModel.respawnPointPos = _respawnPointPos;
            UpdateRespawnPoint();
            currentModel.respawnPointPosDidChange += RespawnPointDidChange;
        }
    }

    private void RespawnPointDidChange(RespawnPointDataModel model, Vector3 value) 
    {
        UpdateRespawnPoint();
    }
    
    private void UpdateRespawnPoint() 
    {
        _respawnPointPos = model.respawnPointPos;
        
        Debug.Log(_respawnPointPos);
    }

    public void SetNewRespawnPoint(Vector3 newRespawnPointPos) 
    {
        model.respawnPointPos = newRespawnPointPos;
    }
}
