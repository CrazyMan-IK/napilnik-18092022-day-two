using UnityEngine;

namespace RedEyes
{
    public class RedEyes : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _destroyDelay = 10f;
        
        private Vector3 _moveDirection;

        public void Initialize(Transform target)
        {
            var horizontalDirection = target.right * (Random.Range(0, 101) > 50 ? 1 : -1);
            _moveDirection = (horizontalDirection + Vector3.up * Random.Range(0, 0.2f));
            Destroy(gameObject, _destroyDelay);
        }

        private void Update()
        {
            transform.position += _moveDirection * (_moveSpeed * Time.deltaTime);
        }
    }
}