using UnityEngine;

public class PlayerCharacter : Character
{
    public override Character Target
    {
        get
        {
            Character target = null;
            float nearest = float.MaxValue;
            var activePool = GameManager.Instance.CharacterFactory.ActivePool;
            foreach (var activeCharacter in activePool)
            {
                if (activeCharacter.CharacterType == CharacterType.DefaultPlayer)
                    continue;
                
                if (!activeCharacter.HealthComponent.IsAlive)
                    continue;
                
                float distance = Vector3.Distance(activeCharacter.transform.position, transform.position);
                if (distance < nearest)
                {
                    nearest = distance;
                    target = activeCharacter;
                }
            }

            return target;
        }
    }
    

    public override void Initialize()
    {
        base.Initialize();

        AttackComponent = new WeaponAttackComponent();
        AttackComponent.Initialize(this);

        ControlComponent = new PlayerControlComponent();
        ControlComponent.Initialize(this);
    }

    protected override void Update()
    {
        if (!HealthComponent.IsAlive
            || !GameManager.Instance.IsGameActive)
            return;
        
        ControlComponent.OnUpdate();
    }
}