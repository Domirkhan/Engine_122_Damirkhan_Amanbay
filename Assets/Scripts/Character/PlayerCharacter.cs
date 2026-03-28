using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter : Character
{

    public override Character CharacterTarget
    {
        get
        {
            Character target = null;
            float minDistance = float.MaxValue;
            List<Character> list = GameManager.Instance.CharacterFactory.ActiveCharacters;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CharacterType == CharacterType.Player)
                    continue;

                float distanceBetween = Vector3.Distance(list[i].transform.position, transform.position);
                if (distanceBetween < minDistance)
                {
                    target = list[i];
                    minDistance = distanceBetween;
                }
            }
            return target;
        }
    }

    private IInputComponent InputComponent { get; set; }

    public override void Initialize()
    {
        base.Initialize();
        LiveComponent = new PlayerLiveComponent();
        LiveComponent.Initialize(this);

        InputComponent = new PlayerInputComponent();

        AttackComponent = new CharacterAttackComponent();
        AttackComponent.Initialize(this);
    }

    public override void Update()
    {
        if (!LiveComponent.IsAlive)
            return;
        Vector3 movementVector = InputComponent.GetMovementVector();

        if (CharacterTarget == null)
        {
            MovableComponent.Rotation(movementVector);
        }
        else
        {
            Vector3 rotationDirection = CharacterTarget.transform.position - transform.position;
            MovableComponent.Rotation(rotationDirection);

            // ??? ? ?????: ?????? ???? ? ??????? � ??????? ?????? MakeDamage (??????? ??? ??????????)
            if (AttackComponent != null && CharacterTarget.LiveComponent.IsAlive)
                AttackComponent.MakeDamage(CharacterTarget);
        }

        MovableComponent.Move(movementVector);
    }
}