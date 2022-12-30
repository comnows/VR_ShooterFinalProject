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
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
