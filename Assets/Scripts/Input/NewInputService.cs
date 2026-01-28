using UnityEngine;

namespace ZombieIo.Input
{
    public class NewInputService : IInputService
    {
        private PlayerInput playerInput;
        
        
        public Vector2 Direction =>
            playerInput.gameplay_map.Movement.ReadValue<Vector2>();

        public bool Attack =>
            playerInput.gameplay_map.Attack.ReadValue<bool>();
        
        public bool Skill =>
            playerInput.gameplay_map.Skill.ReadValue<bool>();


        public NewInputService()
        {
            playerInput = new PlayerInput();
            playerInput.gameplay_map.Enable();
        }
    }
}