using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class EnemySyncData : RealtimeComponent<EnemySyncDataModel>
{
    public int _enemyHP;
    public string _enemyBehaviorState;
    private void Awake() 
    {
        _enemyHP = 100;
        _enemyBehaviorState = "Idle";
    }

    protected override void OnRealtimeModelReplaced(EnemySyncDataModel previousModel, EnemySyncDataModel currentModel) 
    {
        if (previousModel != null) {
            // Unregister from events
            previousModel.enemyHPDidChange -= EnemyHPDidChange;
            previousModel.enemyBehaviorStateDidChange -= EnemyBehaviorStateDidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.enemyHP = _enemyHP;
                currentModel.enemyBehaviorState = _enemyBehaviorState;
            // Update the mesh render to match the new model
            UpdateEnemyHP();
            UpdateEnemyBehaviorState();
            // Register for events so we'll know if the color changes later
            currentModel.enemyHPDidChange += EnemyHPDidChange;
            currentModel.enemyBehaviorStateDidChange += EnemyBehaviorStateDidChange;
        }
    }

    private void EnemyHPDidChange(EnemySyncDataModel model, int value) 
    {
        UpdateEnemyHP();
    }
    
    private void EnemyBehaviorStateDidChange(EnemySyncDataModel model, string value) 
    {
        UpdateEnemyBehaviorState();
    }


    private void UpdateEnemyHP() 
    {
        _enemyHP = model.enemyHP;

        Debug.Log("EnemyHP = " + _enemyHP);

         if (_enemyHP <= 0)
        {
            EnemyBehaviorStateManager enemyBehaviorStateManager = gameObject.GetComponent<EnemyBehaviorStateManager>();
            ChangeBehaviorState("Die");
            enemyBehaviorStateManager.Die();
        } 
    }

    private void UpdateEnemyBehaviorState() 
    {
        _enemyBehaviorState = model.enemyBehaviorState;

        Debug.Log("EnemyBehaviorState = " + _enemyBehaviorState);
    }

    public void ChangeEnemyHP(int damage) 
    {
        model.enemyHP -= damage;
    }

    public void ChangeBehaviorState(string state) 
    {
        model.enemyBehaviorState = state;
    }
}
