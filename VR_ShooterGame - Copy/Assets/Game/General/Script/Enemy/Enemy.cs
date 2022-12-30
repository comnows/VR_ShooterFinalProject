using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AttackTarget
{
    private int health = 50;

    public override void ReceiveAttack(int damage)
    {
        TakeDamage(damage);

        if (health <= 0)
        {
            Die();
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
