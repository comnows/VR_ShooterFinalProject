using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
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
    }
    public override void TakeDamage(int damage,GameObject damagesDealer)
    {
        if (enemySyncData._enemyHP > 0)
        {
            enemySyncData.ChangeEnemyHP(damage,damagesDealer);
            BossHPUI bossHPUI = GetComponent<BossHPUI>();
            bossHPUI.UpdateBossHPText();
        }
    }
}
