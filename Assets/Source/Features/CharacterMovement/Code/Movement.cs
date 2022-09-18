using UnityEngine;
using AYellowpaper;
using CharacterMovement.Extensions;
using CharacterMovement.Interfaces;

namespace CharacterMovement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour, IReadOnlyMovement
    {
        [SerializeField] private float _maxSpeed = 10;
        [SerializeField] private float _accelerationSpeedMultiplier = 10;
        [SerializeField] private Transform _cameraPivot = null;
        [SerializeField] private InterfaceReference<IReadOnlyInput> _input = null;

        private Rigidbody _rigidbody = null;
        private Vector3 _lastDirection = Vector3.forward;
        private Vector3 _euler = Vector3.zero;

        public Vector3 RotationEuler => _euler;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_input.Value == null)
            {
                return;
            }

            _euler.x = Mathf.Clamp(_euler.x - _input.Value.MouseDelta.y, -90, 90);
            _euler.y += _input.Value.MouseDelta.x;

            _cameraPivot.localRotation = Quaternion.Euler(_euler.x, 0, 0);
            _rigidbody.MoveRotation(Quaternion.Euler(0, _euler.y, 0));
        }

        private void FixedUpdate()
        {
            if (_input.Value == null)
            {
                return;
            }

            if (_input.Value.Direction == Vector2.zero)
            {
                _rigidbody.velocity = Vector3.up * _rigidbody.velocity.y;
                return;
            }

            var direction = _input.Value.Direction.AsXZ();
            var mag = direction.magnitude;

            if (mag <= 0.0001f)
            {
                direction = _lastDirection;
                mag = 0;
            }

            var newVelocity = Vector3.Lerp(_rigidbody.velocity, _maxSpeed * mag * (_rigidbody.rotation * direction), Time.deltaTime * _accelerationSpeedMultiplier);
            newVelocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = newVelocity;

            if (mag > 0)
            {
                _lastDirection = direction;
            }
        }
    }

    public interface IReadOnlyMovement
    {
        Vector3 RotationEuler {get;}
    }
}
