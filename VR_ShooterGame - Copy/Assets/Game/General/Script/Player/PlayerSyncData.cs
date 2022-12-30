using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlayerSyncData : RealtimeComponent<PlayerSyncDataModel>
{
    private int _playerHP;
    private int _playerScore;
    private Vector2 _playerMoveInput;
    private float _playerMoveSpeedMultiplier;
    public Animator animator;

    private void Awake() 
    {
        _playerHP = 100;
        _playerScore = 0;
        _playerMoveInput = new Vector2(0.0f,0.0f);
        _playerMoveSpeedMultiplier = 0; 
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //SetMoveAnimation(_playerMoveInput);

        this.GetComponent<PlayerMovement>().SetMoveAnimation(_playerMoveInput,_playerMoveSpeedMultiplier);
    }

    protected override void OnRealtimeModelReplaced(PlayerSyncDataModel previousModel, PlayerSyncDataModel currentModel) 
    {
        if (previousModel != null) {
            // Unregister from events
            previousModel.playerHPDidChange -= PlayerHPDidChange;
            previousModel.playerScoreDidChange -= PlayerScoreDidChange;
            previousModel.playerMoveInputDidChange -= PlayerMoveInputDidChange;
            previousModel.playerMoveSpeedMultiplierDidChange -= PlayerMoveSpeedMultiplierDidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.playerHP = _playerHP;
                currentModel.playerScore = _playerScore;
                currentModel.playerMoveInput = _playerMoveInput;
                currentModel.playerMoveSpeedMultiplier = _playerMoveSpeedMultiplier;
            // Update the mesh render to match the new model
            UpdatePlayerHP();
            UpdatePlayerScore();
            UpdatePlayerMoveInput();
            UpdatePlayerMoveSpeedMultiplier();
            // Register for events so we'll know if the color changes later
            currentModel.playerHPDidChange += PlayerHPDidChange;
            currentModel.playerScoreDidChange += PlayerScoreDidChange;
            currentModel.playerMoveInputDidChange += PlayerMoveInputDidChange;
            currentModel.playerMoveSpeedMultiplierDidChange += PlayerMoveSpeedMultiplierDidChange;
        }
    }

    private void PlayerHPDidChange(PlayerSyncDataModel model, int value) 
    {
        UpdatePlayerHP();
    }
    
    private void PlayerScoreDidChange(PlayerSyncDataModel model, int value) 
    {
        UpdatePlayerScore();
    }

    private void PlayerMoveInputDidChange(PlayerSyncDataModel model, Vector2 value)
    {
        UpdatePlayerMoveInput();
    }

    private void PlayerMoveSpeedMultiplierDidChange(PlayerSyncDataModel model, float value)
    {
        UpdatePlayerMoveSpeedMultiplier();
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

     private void UpdatePlayerMoveInput() 
    {
        _playerMoveInput = model.playerMoveInput;

        Debug.Log("PlayerMoveInput = " + _playerMoveInput);
    }

    private void UpdatePlayerMoveSpeedMultiplier() 
    {
        _playerMoveSpeedMultiplier = model.playerMoveSpeedMultiplier;

        Debug.Log("PlayerMoveSpeedMultiplier = " + _playerMoveSpeedMultiplier);
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

    public void ChangedPlayerMoveInput(Vector2 playerInput)
    {
        model.playerMoveInput = playerInput;
    }

    public void ChangedPlayerMoveSpeedMultiplier(float playerSpeedMultiplier)
    {
        model.playerMoveSpeedMultiplier = playerSpeedMultiplier;
    }

    public void SetMoveAnimation(Vector2 moveInput)
    {
        animator.SetFloat("HorizontalMove", moveInput.x * _playerMoveSpeedMultiplier, 0.05f, Time.deltaTime);
        animator.SetFloat("VerticalMove", moveInput.y * _playerMoveSpeedMultiplier, 0.05f, Time.deltaTime);
    }
}
