using System;
using UnityEngine;

namespace CharacterMovement.Interfaces
{
    public interface IReadOnlyInput
    {
        event Action InteractKeyPressed;
        event Action JournalKeyPressed;

        Vector2 Direction { get; }
        Vector2 MouseDelta { get; }
        bool IsCursorLocked { get; }
    }
}
