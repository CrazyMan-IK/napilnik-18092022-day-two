using UnityEngine;

namespace RedEyes
{
    public class FaceToCamera : MonoBehaviour
    {
        [SerializeField] private bool _xRotate = true;

        private Transform _cameraTransform;
        private Quaternion _defaultRotation;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
            _defaultRotation = transform.localRotation;
        }

        private void Update()
        {
            transform.forward = _cameraTransform.forward;

            if (_xRotate == false)
                transform.localRotation = Quaternion.Euler(0, _defaultRotation.eulerAngles.y, _defaultRotation.eulerAngles.z);
        }
    }
}
