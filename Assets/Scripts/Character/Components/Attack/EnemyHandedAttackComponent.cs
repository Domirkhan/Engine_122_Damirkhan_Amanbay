using UnityEngine;

public class EnemyHandedAttackComponent : IAttackComponent
{
    private const float ATTACK_DURATION_MAX = 1f;

    
    private Character thisCharacter;
    private CharacterData _characterData;
    private float _attackDuration;

    
    public float Damage => _characterData.baseDamage;
    public float AttackRange => 2.6f;
    

    public void MakeAttack()
    {
        if (thisCharacter.Target == null
            || !thisCharacter.Target.HealthComponent.IsAlive
            || _attackDuration > 0)
            return;
        
        float targetDistance = Vector3.Distance(
            thisCharacter.Target.MovementComponent.Position,
            _characterData.CharacterTransform.position);
        
        if (targetDistance > AttackRange)
            return;
        
        thisCharacter.Target.HealthComponent.Health -= Damage;
        _attackDuration = ATTACK_DURATION_MAX;
    }

    public void OnUpdate()
    {
        if (_attackDuration > 0)
        {
            _attackDuration -= Time.deltaTime;
        }
    }

    public void Initialize(Character character)
    {
        thisCharacter = character;
        _characterData = character.CharacterData;
    }
}
