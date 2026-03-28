using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLiveComponent : ILiveComponent
{
    private Character selfCharacter;
    private float currentHealth;

    public event Action OnDeath;
    public event Action<Character> OnCharacterDeath;
    public event Action<float, int> OnHealthChanged;

    public int MaxHealth => 50;

    public float Health
    {
        get => currentHealth;
        protected set
        {
            currentHealth = value;
            if (currentHealth > MaxHealth)
                currentHealth = MaxHealth;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnHealthChanged?.Invoke(currentHealth, MaxHealth);
                SetDeath();
                return;
            }
            OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        }
    }

    public bool IsAlive => Health > 0;

    public CharacterLiveComponent()
    {
        Health = MaxHealth;
    }

    public void SetDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Get damage = " + damage);
    }

    protected void SetDeath()
    {
        OnDeath?.Invoke();
        OnCharacterDeath?.Invoke(selfCharacter);
        Debug.Log("Character is dead");
    }

    public void GetDamage(float damage)
    {
        SetDamage(damage);
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        OnHealthChanged?.Invoke(Health, MaxHealth);
    }
}