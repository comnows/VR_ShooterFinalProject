using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviorState : MonoBehaviour
{
    public abstract EnemyBehaviorState RunCurrentState();
}
