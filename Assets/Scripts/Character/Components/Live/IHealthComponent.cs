using System;

public interface IHealthComponent : ICharacterComponent
{
    public event Action<Character> OnCharacterHealthChange; 
    public event Action<Character> OnCharacterDeath; 
    
    
    public float Health { get; set; }
    
    public int HealthMax { get; }
    
    public bool IsAlive { get; }
}
