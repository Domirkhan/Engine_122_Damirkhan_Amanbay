using UnityEngine;

public class PlayerInputComponent : IInputComponent
{
    public Vector3 GetMovementVector()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ¬озвращаем нормализованный вектор движени€
        return new Vector3(moveHorizontal, 0, moveVertical).normalized;
    }
}