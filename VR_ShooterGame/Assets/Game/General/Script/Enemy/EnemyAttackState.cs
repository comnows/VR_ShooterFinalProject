using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBehaviorState
{
    public override EnemyBehaviorState RunCurrentState()
    {
        Debug.Log("Attack");
        return this;
    }
}
