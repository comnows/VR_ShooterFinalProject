using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AttackTarget
{
    private EnemyData enemyData;   

    private void Awake() 
    {
        enemyData = GetComponent<EnemyData>();
    }

    public override void ReceiveAttack(int damage, GameObject damagesDealer)
    {
        enemyData.DecreaseEnemyHP(damage,damagesDealer);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
