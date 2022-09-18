using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper;
using CharacterMovement.Interfaces;
using UnityEngine;

namespace Notes
{
    public class TreeInteractor : MonoBehaviour
    {
        [SerializeField] private RectTransform _interactView = null;
        [SerializeField] private Projector _projectorPrefab = null;
        [SerializeField] private InterfaceReference<IReadOnlyInput> _input = null;
        [SerializeField] private float _interactDistance = 3;

        private RaycastHit _lastResult = default;

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
            if (!Physics.Raycast(transform.position, transform.forward, out _lastResult, _interactDistance) || _lastResult.transform.TryGetComponent<Note>(out _))
            {
                _interactView.gameObject.SetActive(false);

                return;
            }

            _interactView.gameObject.SetActive(true);
        }

        private void OnInteracted()
        {
            if (_lastResult.collider == null)
            {
                return;
            }

            Instantiate(_projectorPrefab, transform.position + transform.forward * 0.1f, transform.rotation);
        }
    }
}
