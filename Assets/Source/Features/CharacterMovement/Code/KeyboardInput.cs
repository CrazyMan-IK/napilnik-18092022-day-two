using System;
using UnityEngine;
using CharacterMovement.Interfaces;

namespace CharacterMovement
{
    public class KeyboardInput : MonoBehaviour, IReadOnlyInput
    {
        public event Action InteractKeyPressed = null;

        [SerializeField] private float _sensitivity = 1;

        public Vector2 Direction { get; private set; } = Vector2.zero;
        public Vector2 MouseDelta { get; private set; } = Vector2.zero;
        public bool IsLocked => false;

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

            x = Input.GetAxisRaw("Mouse X");
            y = Input.GetAxisRaw("Mouse Y");

            MouseDelta = new Vector2(x, y) * _sensitivity;
        }
    }
}
