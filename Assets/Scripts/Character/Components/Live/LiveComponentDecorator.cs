using System;

public class LiveComponentDecorator : ILiveComponent
{
    protected ILiveComponent wrappee;

    public event Action OnDeath
    {
        add => wrappee.OnDeath += value;
        remove => wrappee.OnDeath -= value;
    }

    public event Action<Character> OnCharacterDeath
    {
        add => wrappee.OnCharacterDeath += value;
        remove => wrappee.OnCharacterDeath -= value;
    }

    public event Action<float, int> OnHealthChanged
    {
        add => wrappee.OnHealthChanged += value;
        remove => wrappee.OnHealthChanged -= value;
    }

    public LiveComponentDecorator(ILiveComponent wrappee)
    {
        this.wrappee = wrappee;
    }

    public virtual bool IsAlive => wrappee.IsAlive;
    public virtual int MaxHealth => wrappee.MaxHealth;
    public virtual float Health => wrappee.Health;

    public virtual void GetDamage(float damage)
    {
        wrappee.GetDamage(damage);
    }

    public virtual void Initialize(Character selfCharacter)
    {
        wrappee.Initialize(selfCharacter);
    }
}