using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField]
    private Character targetCharacter;

    [SerializeField]
    private AiState aiState;

    private const float AttackDistance = 3f;

    public override Character CharacterTarget =>
        GameManager.Instance.CharacterFactory.Player;

    public override void Initialize()
    {
        base.Initialize();

        LiveComponent = new EnemyLiveComponent();
        // Инициализируем LiveComponent чтобы selfCharacter корректно передавался в события
        LiveComponent.Initialize(this);

        AttackComponent = new CharacterAttackComponent();
        // Инициализируем компонент атаки для самого врага (this)
        AttackComponent.Initialize(this);
    }

    public override void Update()
    {
        if (LiveComponent == null || !LiveComponent.IsAlive)
        {
            aiState = AiState.None;
            return;
        }

        if (CharacterTarget != null)
        {
            float distance = Vector3.Distance(transform.position, CharacterTarget.transform.position);

            if (distance <= AttackDistance)
            {
                aiState = AiState.Attack;
            }
            else if (aiState != AiState.None)
            {
                aiState = AiState.MoveToTarget;
            }
        }
        else
        {
            aiState = AiState.None;
        }

        switch (aiState)
        {
            case AiState.None:
                return;

            case AiState.MoveToTarget:
                Move();
                break;

            case AiState.Attack:
                AttackComponent.MakeDamage(CharacterTarget);
                break;
        }
    }
    private void Move()
    {
        if (CharacterTarget == null)
            return;

        Vector3 direction = CharacterTarget.transform.position - characterData.CharacterTransform.position;
        direction = direction.normalized;

        MovableComponent.Move(direction);
        MovableComponent.Rotation(direction);
    }
}