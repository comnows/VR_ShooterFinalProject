using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class PlayerSyncDataModel 
{
    [RealtimeProperty(1, true, true)]
    private int _playerHP;
    [RealtimeProperty(2, true, true)]
    private int _playerScore;
    [RealtimeProperty(3, false, true)]
    private Vector2 _playerMoveInput;
    [RealtimeProperty(4, false, true)]
    private float _playerMoveSpeedMultiplier;
    [RealtimeProperty(5,true,true)]
    private string _playerName;
    [RealtimeProperty(6, true, true)]
    private bool _playerIsCanShootGunEffect;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class PlayerSyncDataModel : RealtimeModel {
    public UnityEngine.Vector2 playerMoveInput {
        get {
            return _playerMoveInputProperty.value;
        }
        set {
            if (_playerMoveInputProperty.value == value) return;
            _playerMoveInputProperty.value = value;
            InvalidateUnreliableLength();
            FirePlayerMoveInputDidChange(value);
        }
    }
    
    public float playerMoveSpeedMultiplier {
        get {
            return _playerMoveSpeedMultiplierProperty.value;
        }
        set {
            if (_playerMoveSpeedMultiplierProperty.value == value) return;
            _playerMoveSpeedMultiplierProperty.value = value;
            InvalidateUnreliableLength();
            FirePlayerMoveSpeedMultiplierDidChange(value);
        }
    }
    
    public int playerHP {
        get {
            return _playerHPProperty.value;
        }
        set {
            if (_playerHPProperty.value == value) return;
            _playerHPProperty.value = value;
            InvalidateReliableLength();
            FirePlayerHPDidChange(value);
        }
    }
    
    public int playerScore {
        get {
            return _playerScoreProperty.value;
        }
        set {
            if (_playerScoreProperty.value == value) return;
            _playerScoreProperty.value = value;
            InvalidateReliableLength();
            FirePlayerScoreDidChange(value);
        }
    }
    
    public string playerName {
        get {
            return _playerNameProperty.value;
        }
        set {
            if (_playerNameProperty.value == value) return;
            _playerNameProperty.value = value;
            InvalidateReliableLength();
            FirePlayerNameDidChange(value);
        }
    }
    
    public bool playerIsCanShootGunEffect {
        get {
            return _playerIsCanShootGunEffectProperty.value;
        }
        set {
            if (_playerIsCanShootGunEffectProperty.value == value) return;
            _playerIsCanShootGunEffectProperty.value = value;
            InvalidateReliableLength();
            FirePlayerIsCanShootGunEffectDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(PlayerSyncDataModel model, T value);
    public event PropertyChangedHandler<int> playerHPDidChange;
    public event PropertyChangedHandler<int> playerScoreDidChange;
    public event PropertyChangedHandler<UnityEngine.Vector2> playerMoveInputDidChange;
    public event PropertyChangedHandler<float> playerMoveSpeedMultiplierDidChange;
    public event PropertyChangedHandler<string> playerNameDidChange;
    public event PropertyChangedHandler<bool> playerIsCanShootGunEffectDidChange;
    
    public enum PropertyID : uint {
        PlayerHP = 1,
        PlayerScore = 2,
        PlayerMoveInput = 3,
        PlayerMoveSpeedMultiplier = 4,
        PlayerName = 5,
        PlayerIsCanShootGunEffect = 6,
    }
    
    #region Properties
    
    private ReliableProperty<int> _playerHPProperty;
    
    private ReliableProperty<int> _playerScoreProperty;
    
    private UnreliableProperty<UnityEngine.Vector2> _playerMoveInputProperty;
    
    private UnreliableProperty<float> _playerMoveSpeedMultiplierProperty;
    
    private ReliableProperty<string> _playerNameProperty;
    
    private ReliableProperty<bool> _playerIsCanShootGunEffectProperty;
    
    #endregion
    
    public PlayerSyncDataModel() : base(null) {
        _playerHPProperty = new ReliableProperty<int>(1, _playerHP);
        _playerScoreProperty = new ReliableProperty<int>(2, _playerScore);
        _playerMoveInputProperty = new UnreliableProperty<UnityEngine.Vector2>(3, _playerMoveInput);
        _playerMoveSpeedMultiplierProperty = new UnreliableProperty<float>(4, _playerMoveSpeedMultiplier);
        _playerNameProperty = new ReliableProperty<string>(5, _playerName);
        _playerIsCanShootGunEffectProperty = new ReliableProperty<bool>(6, _playerIsCanShootGunEffect);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _playerHPProperty.UnsubscribeCallback();
        _playerScoreProperty.UnsubscribeCallback();
        _playerNameProperty.UnsubscribeCallback();
        _playerIsCanShootGunEffectProperty.UnsubscribeCallback();
    }
    
    private void FirePlayerHPDidChange(int value) {
        try {
            playerHPDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FirePlayerScoreDidChange(int value) {
        try {
            playerScoreDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FirePlayerMoveInputDidChange(UnityEngine.Vector2 value) {
        try {
            playerMoveInputDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FirePlayerMoveSpeedMultiplierDidChange(float value) {
        try {
            playerMoveSpeedMultiplierDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FirePlayerNameDidChange(string value) {
        try {
            playerNameDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FirePlayerIsCanShootGunEffectDidChange(bool value) {
        try {
            playerIsCanShootGunEffectDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _playerHPProperty.WriteLength(context);
        length += _playerScoreProperty.WriteLength(context);
        length += _playerMoveInputProperty.WriteLength(context);
        length += _playerMoveSpeedMultiplierProperty.WriteLength(context);
        length += _playerNameProperty.WriteLength(context);
        length += _playerIsCanShootGunEffectProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _playerHPProperty.Write(stream, context);
        writes |= _playerScoreProperty.Write(stream, context);
        writes |= _playerMoveInputProperty.Write(stream, context);
        writes |= _playerMoveSpeedMultiplierProperty.Write(stream, context);
        writes |= _playerNameProperty.Write(stream, context);
        writes |= _playerIsCanShootGunEffectProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.PlayerHP: {
                    changed = _playerHPProperty.Read(stream, context);
                    if (changed) FirePlayerHPDidChange(playerHP);
                    break;
                }
                case (uint) PropertyID.PlayerScore: {
                    changed = _playerScoreProperty.Read(stream, context);
                    if (changed) FirePlayerScoreDidChange(playerScore);
                    break;
                }
                case (uint) PropertyID.PlayerMoveInput: {
                    changed = _playerMoveInputProperty.Read(stream, context);
                    if (changed) FirePlayerMoveInputDidChange(playerMoveInput);
                    break;
                }
                case (uint) PropertyID.PlayerMoveSpeedMultiplier: {
                    changed = _playerMoveSpeedMultiplierProperty.Read(stream, context);
                    if (changed) FirePlayerMoveSpeedMultiplierDidChange(playerMoveSpeedMultiplier);
                    break;
                }
                case (uint) PropertyID.PlayerName: {
                    changed = _playerNameProperty.Read(stream, context);
                    if (changed) FirePlayerNameDidChange(playerName);
                    break;
                }
                case (uint) PropertyID.PlayerIsCanShootGunEffect: {
                    changed = _playerIsCanShootGunEffectProperty.Read(stream, context);
                    if (changed) FirePlayerIsCanShootGunEffectDidChange(playerIsCanShootGunEffect);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _playerHP = playerHP;
        _playerScore = playerScore;
        _playerMoveInput = playerMoveInput;
        _playerMoveSpeedMultiplier = playerMoveSpeedMultiplier;
        _playerName = playerName;
        _playerIsCanShootGunEffect = playerIsCanShootGunEffect;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
