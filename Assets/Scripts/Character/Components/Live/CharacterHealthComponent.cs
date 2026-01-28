using System;
using UnityEngine;
using ZombieIo.Character.Skills;

public class CharacterHealthComponent : IHealthComponent
{
    private Character character;
    protected float currentHealth;
    protected int baseHealthMax;

    
    public event Action<Character> OnCharacterHealthChange;
    public event Action<Character> OnCharacterDeath;
    
    public float Health
    {
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, HealthMax);
            OnCharacterHealthChange?.Invoke(character);

            if (currentHealth > 0)
                return;
            
            currentHealth = 0;
            OnCharacterDeath?.Invoke(character);
            Debug.LogError("Character death");
        }
    }

    public virtual int HealthMax =>
        baseHealthMax;
    
    public bool IsAlive =>
        currentHealth > 0;
    

    public virtual void Initialize(Character character)
    {
        this.character = character;
        baseHealthMax = character.CharacterData.baseHealth;
        currentHealth = character.CharacterData.baseHealth;
    }
}
