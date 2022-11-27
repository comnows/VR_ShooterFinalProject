using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlayerData : RealtimeComponent<PlayerDataModel>
{
    private int _playerHP;
    private int _playerScore;
    
    private void Awake() 
    {
        _playerHP = 100;
        _playerScore = 0;
    }

    protected override void OnRealtimeModelReplaced(PlayerDataModel previousModel, PlayerDataModel currentModel) 
    {
        if (previousModel != null) {
            // Unregister from events
            previousModel.playerHPDidChange -= PlayerHPDidChange;
            previousModel.playerScoreDidChange -= PlayerScoreDidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.playerHP = _playerHP;
                currentModel.playerScore = _playerScore;
            // Update the mesh render to match the new model
            UpdatePlayerHP();
            UpdatePlayerScore();
            // Register for events so we'll know if the color changes later
            currentModel.playerHPDidChange += PlayerHPDidChange;
            currentModel.playerScoreDidChange += PlayerScoreDidChange;
        }
    }

    private void PlayerHPDidChange(PlayerDataModel model, int value) 
    {
        UpdatePlayerHP();
    }
    
    private void PlayerScoreDidChange(PlayerDataModel model, int value) 
    {
        UpdatePlayerScore();
    }

    private void UpdatePlayerHP() 
    {
        _playerHP = model.playerHP;
    }

    private void UpdatePlayerScore() 
    {
        _playerScore = model.playerScore;

        Debug.Log("PlayerScore = " + _playerScore);
    }

    public void AddPlayerHP(int hp) 
    {
        model.playerHP += hp;
    }

    public void DecreasePlayerHP(int hp) 
    {
        model.playerHP -= hp;
    }

    public void AddPlayerScore(int scoreToAdd) 
    {
        model.playerScore += scoreToAdd;
    }
}
