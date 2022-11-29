using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class Enemy : AttackTarget
{
    public event Action<Vector3> OnEnemyDie;
    private EnemyData enemyData;   

    private RealtimeView _realtimeView;
    private void Awake() 
    {
        _realtimeView = GetComponent<RealtimeView>();
        enemyData = GetComponent<EnemyData>();
    }

    public override void ReceiveAttack(int damage, GameObject damagesDealer)
    {
        enemyData.DecreaseEnemyHP(damage,damagesDealer);
    }

    public void Die(GameObject damageDealer)
    {
        if (_realtimeView.isUnownedInHierarchy)
        _realtimeView.SetOwnership(damageDealer.GetComponent<RealtimeView>().ownerIDInHierarchy);

        if (_realtimeView.isOwnedLocallyInHierarchy)
        OnEnemyDie?.Invoke(this.transform.position);
        
        Destroy(gameObject);
    }
}
