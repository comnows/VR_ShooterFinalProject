using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Enemy : AttackTarget
{
    private EnemySyncData enemySyncData;
    private RealtimeTransform _realtimeTransform;
    public Animator animator;

    private void Awake() 
    {
        _realtimeTransform = GetComponent<RealtimeTransform>();
        enemySyncData = GetComponent<EnemySyncData>();
        animator = GetComponentInChildren<Animator>();
    }

    public override void ReceiveAttack(int damage, GameObject damagesDealer)
    {
        TakeDamage(damage);
    }
    private void TakeDamage(int damage)
    {
        if (enemySyncData._enemyHP > 0)
        {
            enemySyncData.ChangeEnemyHP(damage);
        }
    
    }
}
