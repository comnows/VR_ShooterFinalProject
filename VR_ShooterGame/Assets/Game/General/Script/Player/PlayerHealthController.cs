using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;

    public event Action<int> OnPlayerHealthUpdate;

    public UIPlayerHealthEffect uiPlayerHealthEffect;

    private void Start()
    {
        OnPlayerHealthUpdate += uiPlayerHealthEffect.RefreshPlayerSplitterUI;
    }

    public void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        OnPlayerHealthUpdate?.Invoke(currentHealth);
    }
}
