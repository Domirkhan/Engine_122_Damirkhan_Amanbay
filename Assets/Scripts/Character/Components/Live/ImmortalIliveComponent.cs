using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalIliveComponent : ILiveComponent
{
    private Character selfCharacter;

    public int MaxHealth => 1;
    public float Health => 1f;
    public bool IsAlive => true;

    public event Action OnDeath;
    public event Action<Character> OnCharacterDeath;
    public event Action<float, int> OnHealthChanged;

    public void GetDamage(float damage)
    {
        // ������ �� ������ � ����������
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        OnHealthChanged?.Invoke(Health, MaxHealth);
    }

    public void SetDamage(float damage)
    {
        Debug.Log("I am immortal");
    }
}