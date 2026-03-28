using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private CharacterType characterType;

    [SerializeField]
    protected CharacterData characterData;

    public virtual Character CharacterTarget { get; }
    public CharacterType CharacterType => characterType;
    public CharacterData Data => characterData;
    public IMovable MovableComponent { get; protected set; }
    public ILiveComponent LiveComponent { get; protected set; }
    public IAttackComponent AttackComponent { get; protected set; }

    public virtual void Initialize()
    {
        MovableComponent = new CharacterMovementComponent();
        // передаём 'this' (сам Character)
        MovableComponent.Initialize(this);
    }

    public abstract void Update();
}