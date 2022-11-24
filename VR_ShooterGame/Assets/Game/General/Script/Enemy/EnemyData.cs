using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class EnemyData : RealtimeComponent<EnemyDataModel>
{
    private int _enemyHP;
    
    private void Awake() {
        _enemyHP = 100;
    }

    protected override void OnRealtimeModelReplaced(EnemyDataModel previousModel, EnemyDataModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.enemyHPDidChange -= EnemyHPDidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.enemyHP = _enemyHP;
            // Update the mesh render to match the new model
            UpdateEnemyHP();
            // Register for events so we'll know if the color changes later
            currentModel.enemyHPDidChange += EnemyHPDidChange;
        }
    }

    private void EnemyHPDidChange(EnemyDataModel model, int value) {
        // Update the mesh renderer
        UpdateEnemyHP();
    }
    
    private void UpdateEnemyHP() {
        // Get the color from the model and set it on the mesh renderer.
        _enemyHP = model.enemyHP;
        
        Debug.Log(_enemyHP);

        if (_enemyHP <= 0)
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            enemy.Die();
        } 
        
    }

    public void DecreaseEnemyHP(int hp) {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.enemyHP -= hp;
    }

}
