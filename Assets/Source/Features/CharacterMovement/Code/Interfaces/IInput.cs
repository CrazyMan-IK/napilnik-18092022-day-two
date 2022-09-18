using System;
using UnityEngine;

namespace CharacterMovement.Interfaces
{
    public interface IInput : IReadOnlyInput
    {
        void CursorLock();
        void CursorUnlock();
    }
}
