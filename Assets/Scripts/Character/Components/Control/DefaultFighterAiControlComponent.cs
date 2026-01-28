using UnityEngine;

public class DefaultFighterAiControlComponent : IControlComponent
{
	private const float TARGET_DISTANCE = 0.5f;
	
	
	private Character character;
	private AiState aiState;	
	
	
	private IMovementComponent MovementComponent =>
		character.MovementComponent;
	
	private IAttackComponent AttackComponent =>
		character.AttackComponent;
	
	
	public void Initialize(Character character)
	{
		this.character = character;
		aiState = AiState.MovementToTarget;
	}

	public void OnUpdate()
	{
		if (character.Target == null || !character.Target.gameObject.activeSelf)
			return;
        
		Vector3 direction = character.Target.transform.position
			- character.CharacterData.CharacterTransform.position;
		
		switch (aiState)
		{
			case AiState.Idle:

				return;
            
			case AiState.MovementToTarget:
				direction = direction.normalized;
				MovementComponent.Move(direction);
				MovementComponent.Rotation(direction);
				if (Vector3.Distance(character.Target.transform.position,
						character.CharacterData.CharacterTransform.position)
					<= TARGET_DISTANCE)
				{
					aiState = AiState.Attack;
				}

				return;
            
			case AiState.Attack:
				MovementComponent.Move(Vector3.zero);
                
				direction = direction.normalized;
				MovementComponent.Rotation(direction);
                
				AttackComponent.MakeAttack();
                
				if (Vector3.Distance(character.Target.transform.position,
						character.CharacterData.CharacterTransform.position)
					> TARGET_DISTANCE)
					aiState = AiState.MovementToTarget;
				return;
		}
	}
}
