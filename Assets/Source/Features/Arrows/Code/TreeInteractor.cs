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
        [SerializeField] private InterfaceReference<IReadOnlyInput> _input = null;
        [SerializeField] private float _interactDistance = 3;

        private Transform _active = null;

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
        }
    }
}
