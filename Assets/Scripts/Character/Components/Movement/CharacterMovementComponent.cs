using UnityEngine;

public class CharacterMovementComponent : IMovementComponent
{
    private Character character;
    private CharacterData characterData;
    // Переменная для хранения текущей скорости плавного поворота
    private float turnSmoothVelocity = 0.1f; 


    public float Speed
    { 
        get
        {
            return characterData.speed;
        }
        set
        {
            if (value >= 0)
                characterData.speed = value;
        } 
    }

    public Vector3 Position =>
        characterData.CharacterTransform.position;
    
    
    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            character.AnimationComponent.SetValue("Movement", 0);
            return;
        }

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        characterData.CharacterController.Move(move * Speed * Time.deltaTime);
        character.AnimationComponent.SetValue("Movement", direction.magnitude);
    }

    public void Rotation(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        float turnSmoothTime = 0.1f;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(characterData.CharacterTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        characterData.CharacterTransform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void Initialize(Character character)
    {
        this.character = character;
        characterData = character.CharacterData;
    }
}