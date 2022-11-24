using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AttackTarget
{
    private EnemyData enemyData;   
    //private int health = 50;

    private void Awake() {
        enemyData = GetComponent<EnemyData>();
    }

    public override void ReceiveAttack(int damage)
    {
        TakeDamage(damage);

        // if (health <= 0)
        // {
        //     Die();
        // }
    }

    private void TakeDamage(int damage)
    {
        //health -= damage;
        enemyData.DecreaseEnemyHP(damage);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
