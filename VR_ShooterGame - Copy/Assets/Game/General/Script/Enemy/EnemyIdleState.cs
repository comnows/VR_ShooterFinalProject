using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBehaviorState
{
    public EnemyChaseState chaseState;
    public bool canSeeThePlayer;

    public override EnemyBehaviorState RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
