using System;
using System.Collections;
using AYellowpaper;
using CharacterMovement;
using UnityEngine;

namespace Screamer
{
    public class ScreamerCondition : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<IReadOnlyMovement> _movement;
        [SerializeField] private float _screamerDurationInSeconds = 10;
        [SerializeField] private float _rotationForLose = 85f;

        private Coroutine _playingCoroutine;

        public bool Playing => _playingCoroutine != null;

        public void StartCheck(Action<ScreamerResult> onEndCheck)
        {
            if (Playing)
                StopCoroutine(_playingCoroutine);
            
            _playingCoroutine = StartCoroutine(Checking(onEndCheck));
        }

        private IEnumerator Checking(Action<ScreamerResult> onEndPlaying)
        {
            var startRotationY = _movement.Value.RotationEuler.y;
            var movementRotationY = 0f;
            var elapsedTime = 0f;

            while (elapsedTime <= _screamerDurationInSeconds && movementRotationY < _rotationForLose)
            {
                elapsedTime += Time.deltaTime;
                movementRotationY = Math.Abs(startRotationY - _movement.Value.RotationEuler.y);

                yield return null;
            }
            
            onEndPlaying?.Invoke(movementRotationY >= _rotationForLose ? ScreamerResult.Scared : ScreamerResult.NotScared);
            _playingCoroutine = null;
        }
    }

    public enum ScreamerResult
    {
        NotScared,
        Scared,
    }
}