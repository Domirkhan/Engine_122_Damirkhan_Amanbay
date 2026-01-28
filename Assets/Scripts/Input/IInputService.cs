using UnityEngine;

namespace ZombieIo.Input
{
    public interface IInputService
    {
        Vector2 Direction { get; }
        
        bool Attack { get; }
        
        bool Skill { get; }
    }
}