using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class EnemyData : RealtimeComponent<EnemyDataModel>
{
    private int _enemyHP;
    private GameObject _damagesDealer;

    private void Awake() {
        _enemyHP = 100;
    }

    protected override void OnRealtimeModelReplaced(EnemyDataModel previousModel, EnemyDataModel currentModel) 
    {
        if (previousModel != null) {
            previousModel.enemyHPDidChange -= EnemyHPDidChange;
        }
        
        if (currentModel != null) {
            if (currentModel.isFreshModel)
                currentModel.enemyHP = _enemyHP;
            UpdateEnemyHP();
            currentModel.enemyHPDidChange += EnemyHPDidChange;
        }
    }

    private void EnemyHPDidChange(EnemyDataModel model, int value) 
    {
        UpdateEnemyHP();
    }
    
    private void UpdateEnemyHP() 
    {
        _enemyHP = model.enemyHP;
        
        Debug.Log(_enemyHP);

        CheckIfEnemyDie();
    }

    public void CheckIfEnemyDie() 
    {
        if (_enemyHP <= 0)
        {
            if (_damagesDealer != null)
            {
            RealtimeView _realtimeView = _damagesDealer.GetComponent<RealtimeView>();
            Debug.Log("Player ID = " + _realtimeView.ownerIDInHierarchy);
            _damagesDealer.GetComponent<PlayerData>().AddPlayerScore(50);
            }
            
            Enemy enemy = gameObject.GetComponent<Enemy>();
            enemy.Die(_damagesDealer);
        } 
    }

    public void DecreaseEnemyHP(int damage, GameObject damagesDealer) 
    {
        model.enemyHP -= damage;
        _damagesDealer = damagesDealer;
    }

}
