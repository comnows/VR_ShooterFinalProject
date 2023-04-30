using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    //public int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;

    public event Action<int> OnPlayerHealthUpdate;

    public UIPlayerHealthEffect uiPlayerHealthEffect;

    private void Start()
    {
        uiPlayerHealthEffect = GameObject.Find("DamageCanvas").GetComponent<UIPlayerHealthEffect>();
        OnPlayerHealthUpdate += uiPlayerHealthEffect.RefreshPlayerSplitterUI;
    }

    public void ReceiveDamage(int currentHealth)
    {
        OnPlayerHealthUpdate?.Invoke(currentHealth);
    }
}
