using UnityEngine;

public abstract class Character : MonoBehaviour
{
	[SerializeField] protected CharacterType characterType;
	[SerializeField] protected CharacterData characterData;

	public CharacterType CharacterType => characterType;
	public CharacterData CharacterData => characterData;
	
	public IMovementComponent MovementComponent { get; protected set; }
	public IHealthComponent HealthComponent { get; protected set; }
	public IAttackComponent AttackComponent { get; protected set; }
	public IAnimationComponent AnimationComponent { get; protected set; }
	public IControlComponent ControlComponent { get; protected set; }
	
	
	public abstract Character Target { get; }


	public virtual void Initialize()
	{
		MovementComponent = new CharacterMovementComponent();
		MovementComponent.Initialize(this);

		HealthComponent = new CharacterHealthComponent();
		HealthComponent.Initialize(this);

		AnimationComponent = new CharacterAnimationComponent();
		AnimationComponent.Initialize(this);
	}
	
	protected abstract void Update();
}