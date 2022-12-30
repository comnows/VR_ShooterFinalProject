using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorStateManager : MonoBehaviour
{
    EnemyBehaviorState currentState;
 
    // Update is called once per frame
    void Update()
    {
      RunStateMachine();  
    }

    private void RunStateMachine()
    {
        EnemyBehaviorState nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(EnemyBehaviorState nextState)
    {
        currentState = nextState;
    }
}
