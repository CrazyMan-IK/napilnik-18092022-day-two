using UnityEngine;

namespace CharacterMovement.Interfaces
{
    public interface IReadOnlyInput
    {
        Transform transform { get; }
        Vector2 Direction { get; }
        Vector2 MouseDelta { get; }
        bool IsLocked { get; }
    }
}
