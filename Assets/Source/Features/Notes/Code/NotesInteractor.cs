using System;
using AYellowpaper;
using CharacterMovement.Interfaces;
using UnityEngine;

namespace Notes
{
    public class NotesInteractor : MonoBehaviour
    {
        public event Action<string> PickedUp = null;

        [SerializeField] private RectTransform _interactView = null;
        [SerializeField] private InterfaceReference<IReadOnlyInput> _input = null;
        [SerializeField] private float _interactDistance = 3;

        private Note _active = null;

        private void OnEnable()
        {
            _input.Value.InteractKeyPressed += OnInteracted;
        }

        private void OnDisable()
        {
            if (_input.Value == null)
            {
                return;
            }

            _input.Value.InteractKeyPressed -= OnInteracted;
        }

        private void Update()
        {
            if (!Physics.Raycast(transform.position, transform.forward, out var result, _interactDistance) || !result.transform.TryGetComponent(out _active))
            {
                _interactView.gameObject.SetActive(false);

                return;
            }

            _interactView.gameObject.SetActive(true);
        }

        private void OnInteracted()
        {
            if (_active == null)
            {
                return;
            }

            PickedUp?.Invoke(_active.Text);

            Destroy(_active.gameObject);
        }
    }
}
