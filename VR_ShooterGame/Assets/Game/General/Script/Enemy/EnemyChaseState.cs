using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBehaviorState
{
    public EnemyAttackState attackState;
    public bool isInAttackRange;

    public override EnemyBehaviorState RunCurrentState()
    {
        if (isInAttackRange)
        {
            Debug.Log("MillNarak");
            return attackState;
        }
        else
        {
            Debug.Log("MillSoCute");
            return this;
        }
    }
}
