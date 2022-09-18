using System;
using UnityEngine;
using CharacterMovement.Interfaces;

namespace CharacterMovement
{
    public class KeyboardInput : MonoBehaviour, IInput
    {
        public event Action InteractKeyPressed = null;
        public event Action JournalKeyPressed = null;

        [SerializeField] private float _sensitivity = 1;

        public Vector2 Direction { get; private set; } = Vector2.zero;
        public Vector2 MouseDelta { get; private set; } = Vector2.zero;
        public bool IsCursorLocked { get; private set; } = true;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");

            var newDirection = new Vector2(x, y);
            newDirection.Normalize();

            Direction = newDirection;

            MouseDelta = Vector2.zero;
            if (IsCursorLocked)
            {
                x = Input.GetAxisRaw("Mouse X");
                y = Input.GetAxisRaw("Mouse Y");

                MouseDelta = new Vector2(x, y) * _sensitivity;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractKeyPressed?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                JournalKeyPressed?.Invoke();
            }
        }

        public void CursorLock()
        {
            IsCursorLocked = true;

            Cursor.lockState = CursorLockMode.Locked;
        }

        public void CursorUnlock()
        {
            IsCursorLocked = false;

            Cursor.lockState = CursorLockMode.None;
        }
    }
}
