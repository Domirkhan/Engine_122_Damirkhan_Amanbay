using System;

public interface ILiveComponent : ICharacterComponent
{
    public event Action OnDeath;
    public event Action <Character> OnCharacterDeath;
    /// <summary>Текущее HP и максимум (для UI).</summary>
    public event Action<float, int> OnHealthChanged;
    public bool IsAlive { get; }
    public int MaxHealth { get; }
    public float Health { get; }

    public void GetDamage(float damage);
}
