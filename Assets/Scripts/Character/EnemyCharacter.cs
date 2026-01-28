using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyCharacter : Character
{
    [SerializeField] private float targetCheckingDistance = 0.5f;


    public override Character Target =>
        GameManager.Instance.CharacterFactory.PlayerCharacter;

    public override void Initialize()
    {
        base.Initialize();
        
        AttackComponent = new EnemyHandedAttackComponent();
        AttackComponent.Initialize(this);
        
        ControlComponent = new DefaultFighterAiControlComponent();
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