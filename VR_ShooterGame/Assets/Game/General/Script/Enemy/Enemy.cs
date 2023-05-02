using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Enemy : AttackTarget
{
    public EnemySyncData enemySyncData;
    //private RealtimeTransform _realtimeTransform;

    // public void Awake() 
    // {
    //     //_realtimeTransform = GetComponent<RealtimeTransform>();
    //     enemySyncData = GetComponent<EnemySyncData>();
    // }
    

    public override void ReceiveAttack(int damage, GameObject damagesDealer)
    {
        TakeDamage(damage,damagesDealer);
        EnemyBehaviorStateManager enemyBehaviorStateManager = gameObject.GetComponent<EnemyBehaviorStateManager>();
        if (enemyBehaviorStateManager != null) 
        {
            if (enemyBehaviorStateManager.player == null)
            {
                enemyBehaviorStateManager.SetTarget(damagesDealer);
            }
        }
        else
        {
            EnemyTypeShootBehaviorStateManager enemyTypeShootBehaviorStateManager = gameObject.GetComponent<EnemyTypeShootBehaviorStateManager>();
            if (enemyTypeShootBehaviorStateManager.player == null)
            {
                enemyTypeShootBehaviorStateManager.SetTarget(damagesDealer);
            }
        }
    }
    public virtual void TakeDamage(int damage, GameObject damagesDealer)
    {
        Debug.Log("MillTakeDamage");
        enemySyncData = GetComponent<EnemySyncData>();
        if (enemySyncData._enemyHP > 0)
        {
            Debug.Log("Enemy HP Clien" + enemySyncData._enemyHP);
            enemySyncData.ChangeEnemyHP(damage,damagesDealer);
        }
    
    }
}
