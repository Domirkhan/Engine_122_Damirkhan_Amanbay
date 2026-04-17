public class HealthBoostDecorator : LiveComponentDecorator
{
    private int bonusHealth;

    public HealthBoostDecorator(ILiveComponent wrappee, int bonusHealth) : base(wrappee)
    {
        this.bonusHealth = bonusHealth;
    }

    public override int MaxHealth => wrappee.MaxHealth + bonusHealth;

    public override void Initialize(Character selfCharacter)
    {
        base.Initialize(selfCharacter);

        // После инициализации — "добавляем здоровье"
        float newHealth = wrappee.Health + bonusHealth;

        // костыльно, но работает — просто вызываем событие обновления
        wrappee.GetDamage(-bonusHealth); // отрицательный урон = хил
    }
}