using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Enemy : AttackTarget
{
    private EnemySyncData enemySyncData;

    private RealtimeTransform _realtimeTransform;

    private void Awake() 
    {
        _realtimeTransform = GetComponent<RealtimeTransform>();
        enemySyncData = GetComponent<EnemySyncData>();
    }

    public override void ReceiveAttack(int damage, GameObject damagesDealer)
    {
        if (enemySyncData._enemyHP > 0)
        {
            enemySyncData.DecreaseEnemyHP(damage, damagesDealer);
        }
    }

    public void DeleyDeath()
    {
        StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        if (!_realtimeTransform.isUnownedInHierarchy)
        {
            _realtimeTransform.ClearOwnership();
            Destroy(gameObject);
        }
    }
}
