using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLiveComponent : ILiveComponent
{
    private Character selfCharacter;

    public event Action OnDeath;
    public event Action<Character> OnCharacterDeath;
    public event Action<float, int> OnHealthChanged;

    private float health = 50f;

    public bool IsAlive => health > 0;
    public int MaxHealth => 50;
    public float Health
    {
        get => health;
        private set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
                OnHealthChanged?.Invoke(health, MaxHealth);
                SetDeath();
                return;
            }
            OnHealthChanged?.Invoke(health, MaxHealth);
        }
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
    }

    public void SetDeath()
    {
        OnDeath?.Invoke();
        OnCharacterDeath?.Invoke(selfCharacter);
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        health = MaxHealth;
        OnHealthChanged?.Invoke(health, MaxHealth);
    }
}
