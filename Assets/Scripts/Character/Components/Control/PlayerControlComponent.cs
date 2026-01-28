using UnityEngine;
using ZombieIo.Input;

public class PlayerControlComponent : IControlComponent
{
	private Character character;
	private IInputService inputService;
	
	
	private IMovementComponent MovementComponent =>
		character.MovementComponent;
	
	private IAttackComponent AttackComponent =>
		character.AttackComponent;
	
	
	public void Initialize(Character character)
	{
		this.character = character;
		inputService = GameManager.Instance.InputService;
	}
	
	public void OnUpdate()
	{
		float x = inputService.Direction.x;
		float z = inputService.Direction.y;
		
		Vector3 moveDirection = new Vector3(x, 0, z).normalized;
		MovementComponent.Move(moveDirection);
        
		if (character.Target == null || !character.Target.HealthComponent.IsAlive)
		{
			MovementComponent.Rotation(moveDirection);
		}
		else
		{
			Vector3 directionToTarget = character.Target.transform.position
				- character.CharacterData.CharacterTransform.position;
			directionToTarget.Normalize();
			MovementComponent.Rotation(directionToTarget);
        
			AttackComponent.OnUpdate();
			AttackComponent.MakeAttack();
		}
	}
}
