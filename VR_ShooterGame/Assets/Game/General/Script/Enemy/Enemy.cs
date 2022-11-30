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

    public void Die(int damageDealerID)
    {
        print("damageDealerID = " + damageDealerID);
        Vector3 thisPosition = this.transform.position;
        if (_realtimeView.isUnownedInHierarchy)
        _realtimeView.SetOwnership(damageDealerID);

        Debug.Log("_realtimeView.ownerIDInHierarchy = " + _realtimeView.ownerIDInHierarchy);

        if (_realtimeView.ownerIDInHierarchy == damageDealerID)
        {
        OnEnemyDie?.Invoke(thisPosition);
        }
        Destroy(gameObject);
    }
}
